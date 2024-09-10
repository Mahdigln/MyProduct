using Application.Services.Product.Command.AddProduct;
using Application.Services.Product.Command.DeleteProduct;
using Application.Services.Product.Command.UpdateProduct;
using Application.Services.Product.Query.GetProducts;
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
		command.UserId = (int)User.GetUserId();

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
		command.UserId = (int)User.GetUserId();
		var result = await Mediator.Send(command, cancellationToken);
		if (result.IsError)
			return BadRequest(result.Errors);

		return NoContent();
	}

	[HttpGet("GetProducts")]
	[AllowAnonymous]
	public async Task<IActionResult> GetProducts([FromQuery] GetProductModel model, CancellationToken cancellationToken)
	{
		var command = model.Adapt<GetProductQueryRequest>();
		var result = await Mediator.Send(command, cancellationToken);
		if (result.IsError)
			return BadRequest(result.Errors);

		return Ok(result.Value);
	}

	[HttpDelete("DeleteProduct")]
	public async Task<IActionResult> DeleteProduct(int productId, CancellationToken cancellationToken)
	{
		var command = new DeleteProductCommandRequest
		{
			UserId = (int)User.GetUserId(),
			ProductId = productId
		};
		var result = await Mediator.Send(command, cancellationToken);
		if (result.IsError)
			return BadRequest(result.Errors);

		return NoContent();
	}

}


