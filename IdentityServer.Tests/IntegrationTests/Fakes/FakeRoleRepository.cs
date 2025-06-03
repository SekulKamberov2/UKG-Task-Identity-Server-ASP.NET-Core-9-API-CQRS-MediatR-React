namespace IdentityServer.Tests.IntegrationTests.Fakes
{
    using IdentityServer.Application.Interfaces;
    using IdentityServer.Domain.Models;

    public class FakeRoleRepository : IRoleRepository
    {
        public Task<bool> AddUserToRoleAsync(int userId, int role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateUserRoleAsync(string roleName, string description)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserRoleAsync(int role)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindRoleByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserRoleAsync(int id, string? roleName, string? description)
        {
            throw new NotImplementedException();
        }
    }
}
