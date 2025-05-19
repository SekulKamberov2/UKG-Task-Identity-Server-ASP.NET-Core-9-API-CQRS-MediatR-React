using IdentityServer.Application.Results;
using MediatR;

namespace IdentityServer.Application.Commands.CreateUser
{
    public record CreateUserCommand(string UserName, string Email, string Password, string PhoneNumber) : IRequest<IdentityResult<CreateUserResponse>>;
}
