using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.Common;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Application.ViewModels.Response;
using TemplateS.Domain.Entities;
using TemplateS.Domain.Interfaces;

namespace TemplateS.Application.Services
{
    public class PullRequestService : BaseService<PullRequestService>, IPullRequestService
    {
        private readonly IPullRequestRepository _pullRequestRepository;
        private readonly IMapper _mapper;

        public PullRequestService(ILogger<PullRequestService> logger, IPullRequestRepository pullRequestRepository, IMapper mapper) : base(logger)
        {
            _pullRequestRepository = pullRequestRepository;
            _mapper = mapper;
        }

        public async Task<GetAllResponse<PullRequestViewModel>> GetAllAsync()
        {
            var prs = await _pullRequestRepository.GetAllAsync();

            return new GetAllResponse<PullRequestViewModel>()
            {
                Datas = prs != null ? prs.Select(a => _mapper.Map<PullRequestViewModel>(a)).ToList() : new List<PullRequestViewModel>()
            };
        }

        public async Task<GetResponse<PullRequestViewModel>> GetByIdAsync(string id)
        {
            var response = new GetResponse<PullRequestViewModel>();
            var guid = ValidationService.ValidGuid<PullRequest>(id);
            var pr = await _pullRequestRepository.GetAsync(x => x.Id == guid);

            if (pr != null) response.Data = _mapper.Map<PullRequestViewModel>(pr);

            return response;
        }

        public async Task<CreateResponse<PullRequestViewModel>> CreateAsync(CreatePullRequestRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var pr = _mapper.Map<PullRequest>(viewModel);
            var prUser = await _pullRequestRepository.CreateAsync(pr);

            return new CreateResponse<PullRequestViewModel>() { Data = _mapper.Map<PullRequestViewModel>(prUser) };
        }

        public async Task<UpdateResponse> UpdateAsync(string id, UpdatePullRequestRequestViewModel viewModel)
        {
            Validator.ValidateObject(viewModel, new ValidationContext(viewModel), true);

            var guid = ValidationService.ValidGuid<PullRequest>(id);
            var pr = _pullRequestRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(pr);

            _mapper.Map(viewModel, pr);

            await _pullRequestRepository.UpdateAsync(pr);

            return new UpdateResponse();
        }

        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var guid = ValidationService.ValidGuid<PullRequest>(id);
            var pr = _pullRequestRepository.Find(x => x.Id == guid);

            ValidationService.ValidExists(pr);

            await _pullRequestRepository.DeleteAsync(pr);

            return new DeleteResponse();
        }

        public async Task<DeleteResponse> DeleteAllAsync()
        {
            await _pullRequestRepository.DeleteAllAsync();

            return new DeleteResponse();
        }
    }
}
