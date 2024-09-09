using BuildingBlocks.CQRS;
using Domain.Abstractions;

namespace Application.Services.Product.Query.GetProducts;

public sealed class GetProductQueryRequest : IQuery<GetProductQueryResponse>
{
    public SortDirection SortDirection { get; set; }
    public int? UserId { get; set; }
    public int PageIndex { get; set; }
    private int _pageSize;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value <= 0 ? 10 : value;
    }

}

public sealed class GetProductQueryResponse
{
    public IReadOnlyList<ProductListDto> Items { get; set; }
}

public sealed class ProductListDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ProduceDate { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
}