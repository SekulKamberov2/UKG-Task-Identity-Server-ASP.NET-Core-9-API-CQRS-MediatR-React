namespace IdentityServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MediatR;

    using IdentityServer.Application.Commands.CreateUser;
    using IdentityServer.Application.Commands.SignIn;
    using IdentityServer.Application.Commands.UpdateUser;
    using IdentityServer.Application.Commands.DeleteUser;

    public class UsersController : BaseApiController
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator) => _mediator = mediator;
         
        [HttpPost("signup")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command) =>
            AsActionResult(await _mediator.Send(command));

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command) =>
            AsActionResult(await _mediator.Send(command));

        [HttpPatch("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            return AsActionResult(await _mediator.Send(command));
        }


        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId) =>
            AsActionResult(await _mediator.Send(new DeleteUserCommand { UserId = userId }));



    }
}
