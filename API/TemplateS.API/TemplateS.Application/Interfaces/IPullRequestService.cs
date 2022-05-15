using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Application.ViewModels.Response;

namespace TemplateS.Application.Interfaces
{
    public interface IPullRequestService
    {
        Task<GetAllResponse<PullRequestViewModel>> GetAllAsync();
        Task<GetResponse<PullRequestViewModel>> GetByIdAsync(string id);
        Task<CreateResponse<PullRequestViewModel>> CreateAsync(CreatePullRequestRequestViewModel viewModel);
        Task<UpdateResponse> UpdateAsync(string id, UpdatePullRequestRequestViewModel viewModel);
        Task<DeleteResponse> DeleteAsync(string id);
        Task<DeleteResponse> DeleteAllAsync();
    }
}
