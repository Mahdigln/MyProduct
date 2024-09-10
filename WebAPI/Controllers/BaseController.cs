using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
	private IMediator _mediator;
	protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
public static class ClaimUtils
{
	public static long GetUserId(this ClaimsPrincipal principal)
	{
		if (principal is null)
			throw new ArgumentNullException(nameof(principal));
		return Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
	}
}
