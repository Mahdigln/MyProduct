using BuildingBlocks.Extension.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace WebAPI.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected int GetUserId()
    {
        _ = int.TryParse(HttpContext.User.Identity?.GetUserId(), CultureInfo.InvariantCulture, out var userId);
        return userId;
    }
}
