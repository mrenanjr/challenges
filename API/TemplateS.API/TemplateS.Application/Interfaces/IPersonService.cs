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
    public interface IPersonService
    {
        Task<GetAllResponse<PersonViewModel>> GetAllAsync();
        Task<GetResponse<PersonViewModel>> GetByIdAsync(string id);
        Task<CreateResponse<PersonViewModel>> CreateAsync(CreatePersonRequestViewModel viewModel);
        Task<UpdateResponse> UpdateAsync(string id, UpdatePersonRequestViewModel viewModel);
        Task<DeleteResponse> DeleteAsync(string id);
    }
}
