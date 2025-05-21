namespace IdentityServer.Application.Commands.DeleteRole
{
    using IdentityServer.Application.Results;
    using MediatR;
    public class DeleteRoleCommand : IRequest<IdentityResult<bool>>
    {
        public int RoleId { get; set; }
    }
}
