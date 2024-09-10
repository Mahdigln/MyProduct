namespace MyProduct.Test.Product.AddProduct;

internal sealed class LoginModels
{
	public string UserName { get; set; }
	public string Password { get; set; }
}

public sealed class LoginResponse
{
	public Accesstoken accessToken { get; set; }
}

public class Accesstoken
{
	public string token { get; set; }
	public string tokenType { get; set; }
}
