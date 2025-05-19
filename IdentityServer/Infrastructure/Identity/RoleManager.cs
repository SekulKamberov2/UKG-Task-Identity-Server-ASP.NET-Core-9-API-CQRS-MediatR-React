namespace IdentityServer.Infrastructure.Identity
{
    using System.Collections.Generic;

    using IdentityServer.Application.Interfaces;
    using IdentityServer.Application.Results;
    using IdentityServer.Domain.Exceptions;
    using IdentityServer.Domain.Models; 

    public class RoleManager : IRoleManager
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleManager> _logger;

        public RoleManager(IRoleRepository roleRepository, ILogger<RoleManager> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        private async Task<IdentityResult<T>> ExecuteWithLogging<T>(
            Func<Task<T>> action,
            string successMessage,
            string errorMessage,
            params object[] args)
        {
            try
            {
                var result = await action();
                if (EqualityComparer<T>.Default.Equals(result, default)) 
                    return IdentityResult<T>.Failure(errorMessage); 

                _logger.LogInformation(successMessage, args);
                return IdentityResult<T>.Success(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex, errorMessage, args);
                return IdentityResult<T>.Failure(errorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while executing action.");
                throw;  
            }
        }

        public async Task<IdentityResult<bool>> AddToRoleAsync(int userId, int roleId)
        {
            return await ExecuteWithLogging(
                () => _roleRepository.AddUserToRoleAsync(userId, roleId),
                "User added to role successfully.",
                $"Error occurred while adding user ID {userId} to role ID {roleId}."
            );
        }

        public async Task<IdentityResult<IEnumerable<string>>> GetRolesAsync(int userId)
        {
            return await ExecuteWithLogging(
                () => _roleRepository.GetUserRolesAsync(userId),
                "Fetched user roles successfully.",
                $"Error occurred while fetching roles for user ID {userId}."
            );
        }

        public Task<IdentityResult<IEnumerable<Role>>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
