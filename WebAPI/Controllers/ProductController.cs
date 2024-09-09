using Application.Services.Product.Command.AddProduct;
using Application.Services.Product.Command.UpdateProduct;
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

    [HttpPut("UpdateProduct/{productId:int}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromForm] UpdateProductModel model, CancellationToken cancellationToken)
    {
        var command = model.Adapt<UpdateProductCommandRequest>();
        command.ProductId = productId;
        command.UserId = GetUserId();
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsError)
            return BadRequest(result.Errors);

        return NoContent();
    }
}
