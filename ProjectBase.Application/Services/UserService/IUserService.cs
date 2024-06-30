using Microsoft.AspNetCore.Http;
using ProjectBase.Domain.Entities;
using ProjectBase.Domain.Pagination;

namespace ProjectBase.Application.Services.UserService
{
    public interface IUserService
    {
        Task<PageList<User>> GetPagedList(int pageIndex, int pageSize);
        Task<List<User>> GetUserByRole(int pageIndex, int pageSize, int roleID);
        Task UploadProfileImage(IFormFile file, string mail);
    }
}