using BuildingBlocks.CQRS;
using Domain.Entities.Identity;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Identity.Command.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    private readonly UserManager<User> _userManager;

    public RegisterUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<RegisterUserCommandResponse>> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return Error.Failure(result.Errors.FirstOrDefault()?.Code, result.Errors.FirstOrDefault()?.Description);

        return new RegisterUserCommandResponse();
    }
}
