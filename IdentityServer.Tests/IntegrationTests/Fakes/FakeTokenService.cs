namespace IdentityServer.Tests.IntegrationTests.Fakes
{
    using IdentityServer.Application.Interfaces;
    using IdentityServer.Application.Results;
    using IdentityServer.Domain.Models;

    public class FakeTokenService : ITokenService
    {
        public IdentityResult<string> GenerateToken(string userId, User user, IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
