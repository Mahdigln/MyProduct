using Domain.Constant;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model.Product;

public sealed class AddProductModel
{
	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	[MaxLength(100, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
	[MinLength(5, ErrorMessage = AttributesErrorMessages.MinLengthMessage)]
	public string Name { get; set; }

	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	public DateTime ProduceDate { get; set; }

	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	[RegularExpression("^0[0-9]{10}$", ErrorMessage = "شماره تلفن وارد شده معتبر نیست")]
	public string ManufacturePhone { get; set; }

	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	[EmailAddress(ErrorMessage = AttributesErrorMessages.InvalidEmailMessage)]
	[MaxLength(150, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
	public string ManufactureEmail { get; set; }

	[Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
	public bool IsAvailable { get; set; }
}