using Domain.Abstractions;

namespace WebAPI.Model.Identity;

public sealed class GetUserProductModel
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