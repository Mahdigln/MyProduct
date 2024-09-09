using BuildingBlocks.CQRS;

namespace Application.Services.Identity.Command.RegisterUser;

public sealed class RegisterUserCommandRequest : ICommand<RegisterUserCommandResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public sealed class RegisterUserCommandResponse
{

}