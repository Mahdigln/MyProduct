using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI;

namespace MyProduct.Test;

public class ProductApiFactory : WebApplicationFactory<IApiMarker>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		base.ConfigureWebHost(builder);
	}
}