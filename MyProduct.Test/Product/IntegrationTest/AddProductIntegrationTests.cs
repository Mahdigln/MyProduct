using FluentAssertions;
using MyProduct.Test.Product.AddProduct;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MyProduct.Test.Product.IntegrationTest;

public sealed class AddProductIntegrationTests : IClassFixture<ProductApiFactory>
{
    private readonly HttpClient _factory;

    public AddProductIntegrationTests(ProductApiFactory factory)
    {
        _factory = factory.CreateClient();
    }

    [Fact]
    public async Task AddProduct_ShouldReturnNoContent_WhenProductIsAddedSuccessfully()
    {
        // Arrange
        var client = _factory;
        var loginRequest = new LoginModels()
        {
            UserName = "mahdi1",
            Password = "Mahdi123456"
        };
        var loginResponse = await client.PostAsJsonAsync(new Uri("http://localhost:5053/Login"), loginRequest);
        var result = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        var token = result.accessToken.token;

        // Set the Authorization header with the Bearer token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        var addProductModelRequest = new MultipartFormDataContent
        {
            { new StringContent("ProductName"), "Name" },
            { new StringContent("2023-09-10"), "ProduceDate" },
            { new StringContent("09034988741"), "ManufacturePhone" },
            { new StringContent("true"), "IsAvailable" },
            { new StringContent("Mahdi@gmail.com"), "ManufactureEmail" }
        };

        // Act
        var response = await client.PostAsync("http://localhost:5053/AddProduct", addProductModelRequest);

        // Assert

        var res = response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task AddProduct_ShouldReturnBadRequest_WhenProductIsDuplicate()
    {
        // Arrange

        //var client = _factory;
        var client = _factory;
        var loginRequest = new LoginModels()
        {
            UserName = "mahdi1",
            Password = "Mahdi123456"
        };
        var loginResponse = await client.PostAsJsonAsync(new Uri("http://localhost:5053/Login"), loginRequest);
        var result = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        var token = result.accessToken.token;

        // Set the Authorization header with the Bearer token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var formData = new MultipartFormDataContent
        {
            { new StringContent("DuplicateProduct"), "Name" },
            { new StringContent("2023-09-10"), "ProduceDate" },
            { new StringContent("09034988741"), "ManufacturePhone" },
            { new StringContent("Mahdi@gmail.comm"), "ManufactureEmail" },
            { new StringContent("true"), "IsAvailable" }
        };

        // Act
        var response = await client.PostAsync("http://localhost:5053/AddProduct", formData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("محصول با این ایمیل تولیدکننده و تاریخ تولید قبلا ثبت شده است");
    }
}