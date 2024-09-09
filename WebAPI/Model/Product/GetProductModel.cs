using Domain.Abstractions;

namespace WebAPI.Model.Product;

public sealed class GetProductModel
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