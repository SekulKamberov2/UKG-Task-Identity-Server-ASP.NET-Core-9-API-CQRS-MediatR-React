namespace IdentityServer.Infrastructure.Repositories
{
    using Dapper;
    using IdentityServer.Application.Interfaces;
    using IdentityServer.Domain.Exceptions;
    using IdentityServer.Domain.Models;
    using Microsoft.Data.SqlClient;
    using System.Data;

    public class RoleRepository : IRoleRepository
    {   
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(IConfiguration configuration, ILogger<RoleRepository> logger)
        {
            _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _logger = logger;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            const string query = @"SELECT * FROM Roles";
            try
            {
                var roles = await _dbConnection.QueryAsync<Role>(query);
                if (roles == null || !roles.Any()) throw new RepositoryException("No roles found.");
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching roles.");
                throw new RepositoryException("Error occurred while fetching roles.", ex);
            }
        }

        public async Task<bool> AddUserToRoleAsync(int userId, int roleId)
        {
            const string deleteQuery = "DELETE FROM UserRoles WHERE UserId = @UserId";
            const string insertQuery = "INSERT INTO UserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)";
            var parameters = new { UserId = userId, RoleId = roleId };

            using var connection = _dbConnection;
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                // Delete previous roles
                await connection.ExecuteAsync(deleteQuery, new { UserId = userId }, transaction);

                // Insert the new role
                var rowsInserted = await connection.ExecuteAsync(insertQuery, parameters, transaction);

                transaction.Commit();
                return rowsInserted > 0;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Error occurred while adding user ID {UserId} to role ID {RoleId}.", userId, roleId);
                throw new RepositoryException("Error occurred while updating user role.", ex);
            }
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        {
            const string query = @"SELECT r.Name FROM Roles r LEFT JOIN UserRoles ur ON r.Id = ur.RoleId WHERE ur.UserId = @UserId";
            var parameters = new { UserId = userId };
            try
            {
                var roles = await _dbConnection.QueryAsync<string>(query, parameters);
                if (roles == null || !roles.Any()) throw new RepositoryException("No roles found for the given user.");
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching roles for user ID {UserId}.", userId);
                throw new RepositoryException("Error occurred while fetching roles.", ex);
            }
        }
    }
}