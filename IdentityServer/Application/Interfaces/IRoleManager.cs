namespace IdentityServer.Application.Interfaces
{
    using IdentityServer.Application.Results;
    using IdentityServer.Domain.Models;
    public interface IRoleManager
    {
        Task<IdentityResult<IEnumerable<Role>>> GetAllRolesAsync();
        Task<IdentityResult<bool>> AddToRoleAsync(int userId, int roleId);
        Task<IdentityResult<IEnumerable<string>>> GetRolesAsync(int userId);


    }
}
