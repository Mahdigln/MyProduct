namespace Domain.Models;

public sealed class UserProductsInfo
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