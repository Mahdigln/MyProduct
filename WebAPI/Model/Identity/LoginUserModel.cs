using Domain.Constant;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model.Identity;

public sealed class LoginUserModel
{
	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	[MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
	public string Username { get; set; }
	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	[MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
	public string Password { get; set; }
}