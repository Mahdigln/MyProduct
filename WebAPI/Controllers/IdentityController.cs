using Application.Services.Identity.Command.RegisterUser;
using Application.Services.Identity.Query.Login;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model.Identity;

namespace WebAPI.Controllers;


public sealed class IdentityController : BaseController
{

    [HttpPost("Register")]
    public async Task<IActionResult> UserRegister([FromBody] RegisterUserModel model, CancellationToken cancellationToken)
    {
        var command = model.Adapt<RegisterUserCommandRequest>();
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsError)
            return BadRequest(result.Errors);

        return NoContent();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> UserLogin(LoginUserModel model, CancellationToken cancellationToken)
    {
        var command = model.Adapt<LoginUserQueryRequest>();
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsError)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}
