using Microsoft.AspNetCore.Mvc;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;

namespace TemplateS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _userService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) => Ok(await _userService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserRequestViewModel viewModel) => Ok(await _userService.CreateAsync(viewModel));

        [HttpPost, Route("[action]")]
        public IActionResult Authenticate(UserAuthRequestViewModel viewModel) => Ok(_userService.Authenticate(viewModel));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdateUserRequestViewModel viewModel) => Ok(await _userService.UpdateAsync(id, viewModel));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Ok(await _userService.DeleteAsync(id));
    }
}
