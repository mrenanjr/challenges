using Microsoft.AspNetCore.Mvc;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels.Request;

namespace TemplateS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _cityService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) => Ok(await _cityService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post(CreateCityRequestViewModel viewModel) => Ok(await _cityService.CreateAsync(viewModel));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdateCityRequestViewModel viewModel) => Ok(await _cityService.UpdateAsync(id, viewModel));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Ok(await _cityService.DeleteAsync(id));
        
        [HttpDelete, Route("[action]")]
        public async Task<IActionResult> DeleteAllAsync() => Ok(await _cityService.DeleteAllAsync());
    }
}
