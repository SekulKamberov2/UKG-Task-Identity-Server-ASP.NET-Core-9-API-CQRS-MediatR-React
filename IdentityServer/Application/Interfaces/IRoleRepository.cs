namespace IdentityServer.Application.Interfaces
{
    using IdentityServer.Domain.Models;
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<bool> AddUserToRoleAsync(int userId, int role);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
    }
}
