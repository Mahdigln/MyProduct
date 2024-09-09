using BuildingBlocks.CQRS;

namespace Application.Services.Product.Command.AddProduct;

public sealed class AddProductCommandRequest : ICommand<AddProductCommandResponse>
{
    public string Name { get; set; }
    public DateTime ProduceDate { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
    public int UserId { get; set; }
}

public sealed class AddProductCommandResponse
{

}