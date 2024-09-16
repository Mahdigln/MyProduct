using BuildingBlocks.CQRS;
using Domain.Abstractions;

namespace Application.Services.Identity.Query.UserProducts;

public sealed class UserProductsQueryRequest : IQuery<UserProductsQueryResponse>
{
    public SortDirection SortDirection { get; set; }
    public int PageIndex { get; set; }
    private int _pageSize;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value <= 0 ? 10 : value;
    }
}

public sealed class UserProductsQueryResponse
{
    public List<UserDto> User { get; set; }
}

public sealed class UserDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public IReadOnlyList<UserProductDto> UserProduct { get; set; }

}

public sealed class UserProductDto
{
    public string Name { get; set; }
    public DateTime ProduceDate { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
    public int UserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedDateTime { get; set; }
}