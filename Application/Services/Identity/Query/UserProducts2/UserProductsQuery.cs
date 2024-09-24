using BuildingBlocks.CQRS;

namespace Application.Services.Identity.Query.UserProducts2;

public sealed class UserProductsQueryRequest2 : IQuery<UserProductsQueryResponse2>
{
}

public sealed class UserProductsQueryResponse2
{
    public List<UserDto2> User { get; set; }
}

public sealed class UserDto2
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<UserProductDto2> UserProduct { get; set; }

}

public sealed class UserProductDto2
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