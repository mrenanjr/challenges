using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TemplateS.Application.Dto.Request;
using TemplateS.Application.Interfaces;
using TemplateS.Application.ViewModels.Request;

namespace TemplateS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IValidator<CreateCityRequestDto> _createCityValidator;
        private readonly IValidator<UpdateCityRequestDto> _updateCityValidator;

        public CitiesController(ICityService cityService, IValidator<CreateCityRequestDto> createCityValidator)
        {
            _cityService = cityService;
            _createCityValidator = createCityValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _cityService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) => Ok(await _cityService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post(CreateCityRequestDto viewModel)
        {
            await _createCityValidator.ValidateAndThrowAsync(viewModel);

            return Ok(await _cityService.CreateAsync(viewModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdateCityRequestDto viewModel)
        {
            await _updateCityValidator.ValidateAndThrowAsync(viewModel);

            return Ok(await _cityService.UpdateAsync(id, viewModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Ok(await _cityService.DeleteAsync(id));
        
        [HttpDelete, Route("[action]")]
        public async Task<IActionResult> DeleteAllAsync() => Ok(await _cityService.DeleteAllAsync());
    }
}
