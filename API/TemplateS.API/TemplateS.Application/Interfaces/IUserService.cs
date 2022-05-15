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
    public interface IUserService
    {
        Task<GetAllResponse<UserViewModel>> GetAllAsync();
        Task<GetResponse<UserViewModel>> GetByIdAsync(string id);
        Task<CreateResponse<UserViewModel>> CreateAsync(CreateUserRequestViewModel viewModel);
        Task<UpdateResponse> UpdateAsync(string id, UpdateUserRequestViewModel viewModel);
        Task<DeleteResponse> DeleteAsync(string id);
        UserAuthResponseViewModel Authenticate(UserAuthRequestViewModel viewModel);
    }
}
