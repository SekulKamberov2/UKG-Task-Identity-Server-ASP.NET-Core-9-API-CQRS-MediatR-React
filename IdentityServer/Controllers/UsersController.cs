namespace IdentityServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MediatR;

    using IdentityServer.Application.Commands.CreateUser;
    using IdentityServer.Application.Commands.SignIn;
    using IdentityServer.Application.Commands.UpdateUser;
    using IdentityServer.Application.Commands.DeleteUser;
    using IdentityServer.Application.Queries.GetUserInfo;

    public class UsersController : BaseApiController
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator) => _mediator = mediator;
         
        [HttpPost("signUp")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command) =>
            AsActionResult(await _mediator.Send(command));

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command) =>
            AsActionResult(await _mediator.Send(command));

        [HttpPatch("update-User/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            return AsActionResult(await _mediator.Send(command));
        } 

        [HttpDelete("delete-User/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId) =>
            AsActionResult(await _mediator.Send(new DeleteUserCommand { UserId = userId }));

        [HttpGet("me/info/{userId}")]
        public async Task<IActionResult> GetInfo(int userId) =>
            AsActionResult(await _mediator.Send(new GetUserInfoQuery { UserId = userId }));

    }
}
