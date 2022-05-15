using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Application.ViewModels;
using TemplateS.Application.ViewModels.Request;
using TemplateS.Application.ViewModels.Response;
using TemplateS.Domain.Entities;

namespace TemplateS.Application.Interfaces
{
    public interface ICityService
    {
        Task<GetAllResponse<CityViewModel>> GetAllAsync();
        Task<GetResponse<CityViewModel>> GetByIdAsync(string id);
        Task<CreateResponse<CityViewModel>> CreateAsync(CreateCityRequestViewModel viewModel);
        Task<UpdateResponse> UpdateAsync(string id, UpdateCityRequestViewModel viewModel);
        Task<DeleteResponse> DeleteAsync(string id);
        Task<DeleteResponse> DeleteAllAsync();
    }
}
