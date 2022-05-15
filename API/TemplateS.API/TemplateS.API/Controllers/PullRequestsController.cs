using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels.Request;

namespace TemplateS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PullRequestsController : ControllerBase
    {
        private readonly IPullRequestService _pullRequestService;

        public PullRequestsController(IPullRequestService pullRequestService)
        {
            _pullRequestService = pullRequestService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Get() => Ok(await _pullRequestService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) => Ok(await _pullRequestService.GetByIdAsync(id));

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Post(CreatePullRequestRequestViewModel viewModel) => Ok(await _pullRequestService.CreateAsync(viewModel));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdatePullRequestRequestViewModel viewModel) => Ok(await _pullRequestService.UpdateAsync(id, viewModel));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Ok(await _pullRequestService.DeleteAsync(id));
        
        [HttpDelete, Route("[action]")]
        public async Task<IActionResult> DeleteAllAsync() => Ok(await _pullRequestService.DeleteAllAsync());
    }
}
