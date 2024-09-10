using Domain.Constant;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model.Identity;

public sealed class RegisterUserModel
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	[MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
	[MinLength(5, ErrorMessage = AttributesErrorMessages.MinLengthMessage)]
	public string Username { get; set; }
	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	[MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
	[MinLength(5, ErrorMessage = AttributesErrorMessages.MinLengthMessage)]
	public string Password { get; set; }

	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	[EmailAddress(ErrorMessage = AttributesErrorMessages.InvalidEmailMessage)]
	[MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
	public string Email { get; set; }
}