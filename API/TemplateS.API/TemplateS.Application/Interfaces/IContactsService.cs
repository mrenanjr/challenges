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
    public interface IContactService
    {
        Task<GetAllResponse<ContactViewModel>> GetAllAsync();
        Task<GetResponse<ContactViewModel>> GetByIdAsync(string id);
        Task<CreateResponse<ContactViewModel>> CreateAsync(CreateContactRequestViewModel viewModel);
        Task<UpdateResponse> UpdateAsync(string id, UpdateContactRequestViewModel viewModel);
        Task<DeleteResponse> DeleteAsync(string id);
        Task<DeleteResponse> DeleteAllAsync();
    }
}
