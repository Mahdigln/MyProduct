using BuildingBlocks.CQRS;

namespace Application.Services.Product.Command.UpdateProduct;

public sealed class UpdateProductCommandRequest : ICommand<UpdateProductCommandResponse>
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public DateTime ProduceDate { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
    public int UserId { get; set; }
}

public sealed class UpdateProductCommandResponse
{
}