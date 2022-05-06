using Microsoft.AspNetCore.Mvc;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;

namespace TemplateS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _personService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) => Ok(await _personService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post(CreatePersonRequestViewModel viewModel) => Ok(await _personService.CreateAsync(viewModel));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdatePersonRequestViewModel viewModel) => Ok(await _personService.UpdateAsync(id, viewModel));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Ok(await _personService.DeleteAsync(id));
    }
}
