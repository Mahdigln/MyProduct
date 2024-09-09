using Application.Services.Product.Command.AddProduct;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model.Product;

namespace WebAPI.Controllers;
[Authorize]
public sealed class ProductController : BaseController
{

    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct([FromForm] AddProductModel model, CancellationToken cancellationToken)
    {
        var command = model.Adapt<AddProductCommandRequest>();
        command.UserId = GetUserId();
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsError)
            return BadRequest(result.Errors);

        return NoContent();
    }

    //[HttpPost("Login")]
    //public async Task<IActionResult> UserLogin(LoginUserModel model, CancellationToken cancellationToken)
    //{
    //    var command = model.Adapt<LoginUserQueryRequest>();
    //    var result = await Mediator.Send(command, cancellationToken);
    //    if (result.IsError)
    //        return BadRequest(result.Errors);

    //    return Ok(result.Value);
    //}
}
