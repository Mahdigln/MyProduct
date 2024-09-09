using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;
public class User : IdentityUser<int>, IEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
