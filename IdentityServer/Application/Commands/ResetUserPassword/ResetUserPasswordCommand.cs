using IdentityServer.Application.Results;
using MediatR;

namespace IdentityServer.Application.Commands.ResetUserPassword
{
    public class ResetUserPasswordCommand : IRequest<IdentityResult<bool>>
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
    }
}
