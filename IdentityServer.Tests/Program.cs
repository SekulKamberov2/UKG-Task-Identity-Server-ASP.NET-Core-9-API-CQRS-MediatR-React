using FluentValidation;
using FluentValidation.AspNetCore;

using IdentityServer.Application.Commands.CreateUser;
using IdentityServer.Application.Commands.SignIn;
using IdentityServer.Application.Interfaces;
using IdentityServer.Controllers;
using IdentityServer.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>();
    cfg.RegisterServicesFromAssemblyContaining<SignInCommandHandler>();
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters(); 

//   Only register real services if NOT running in test mode
if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddScoped<IUserManager, UserManager>();
    builder.Services.AddScoped<IRoleManager, RoleManager>();
    builder.Services.AddScoped<ITokenService, TokenService>();
}

builder.Services.AddControllers()
    .AddApplicationPart(typeof(UsersController).Assembly);

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
