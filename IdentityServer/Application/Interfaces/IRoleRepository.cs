namespace IdentityServer.Application.Interfaces
{
    using IdentityServer.Domain.Models;
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<bool> AddUserToRoleAsync(int userId, int role);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
        Task<bool> CreateUserRoleAsync(string roleName, string description);
        Task<Role> FindRoleByIdAsync(int roleId);
        Task<bool> UpdateUserRoleAsync(int id, string? roleName, string? description);
        Task<bool> DeleteUserRoleAsync(int role);
    }
}
