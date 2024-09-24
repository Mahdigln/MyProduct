using Application.Services.Identity.Command.RegisterUser;
using Application.Services.Identity.Query.Login;
using Application.Services.Identity.Query.UserProducts;
using Application.Services.Identity.Query.UserProducts2;
using Mapster;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("UserProducts")]
    [AllowAnonymous]
    public async Task<IActionResult> UserProducts([FromQuery] GetUserProductModel model, CancellationToken cancellationToken)
    {
        var command = model.Adapt<UserProductsQueryRequest>();
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsError)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }

    [HttpGet("UserProducts2")]
    [AllowAnonymous]
    public async Task<IActionResult> UserProducts2(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UserProductsQueryRequest2(), cancellationToken);
        if (result.IsError)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }

}
