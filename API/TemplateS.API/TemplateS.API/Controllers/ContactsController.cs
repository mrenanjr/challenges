using Microsoft.AspNetCore.Mvc;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels.Request;

namespace TemplateS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactsService;

        public ContactsController(IContactService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _contactsService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) => Ok(await _contactsService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post(CreateContactRequestViewModel viewModel) => Ok(await _contactsService.CreateAsync(viewModel));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdateContactRequestViewModel viewModel) => Ok(await _contactsService.UpdateAsync(id, viewModel));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Ok(await _contactsService.DeleteAsync(id));

        [HttpDelete, Route("[action]")]
        public async Task<IActionResult> DeleteAllAsync() => Ok(await _contactsService.DeleteAllAsync());
    }
}
