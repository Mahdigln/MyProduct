using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected int GetUserId()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userIdString, out int userId))
        {
            return userId;
        }

        throw new Exception("User ID is not valid.");
    }
}
