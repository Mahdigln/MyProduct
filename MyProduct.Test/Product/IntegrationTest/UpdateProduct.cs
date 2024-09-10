using FluentAssertions;
using MyProduct.Test.Product.AddProduct;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MyProduct.Test.Product.IntegrationTest;

public class UpdateProductIntegrationTests : IClassFixture<ProductApiFactory>
{
    private readonly HttpClient _factory;

    public UpdateProductIntegrationTests(ProductApiFactory factory)
    {
        _factory = factory.CreateClient();
    }

    [Fact]
    public async Task UpdateProduct_ShouldReturnNoContent_WhenProductIsUpdatedSuccessfully()
    {
        // Arrange
        var client = _factory;

        // Login and get the token
        var loginRequest = new LoginModels()
        {
            UserName = "mahdi1",
            Password = "Mahdi123456"
        };
        var loginResponse = await client.PostAsJsonAsync(new Uri("http://localhost:5053/Login"), loginRequest);
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        var token = loginResult.accessToken.token;

        // Set the Authorization header with the Bearer token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Update product model
        var updateProductModelRequest = new MultipartFormDataContent
        {
            { new StringContent("UpdatedProductName"), "Name" },
            { new StringContent("2023-09-10"), "ProduceDate" },
            { new StringContent("09034988852"), "ManufacturePhone" },
            { new StringContent("true"), "IsAvailable" },
            { new StringContent("MahdiUpdated@gmail.com"), "ManufactureEmail" }
        };

        // Act
        int productId = 7;  // Assume product with ID 7 exists and belongs to the user
        var response = await client.PutAsync($"http://localhost:5053/UpdateProduct/{productId}", updateProductModelRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    [Fact]
    public async Task UpdateProduct_ShouldReturnBadRequest_WhenProductDoesNotExistOrBelongsToAnotherUser()
    {
        // Arrange
        var client = _factory;

        // Login and get the token
        var loginRequest = new LoginModels()
        {
            UserName = "mahdi1",
            Password = "Mahdi123456"
        };
        var loginResponse = await client.PostAsJsonAsync(new Uri("http://localhost:5053/Login"), loginRequest);
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        var token = loginResult.accessToken.token;

        // Set the Authorization header with the Bearer token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Update product model
        var updateProductModelRequest = new MultipartFormDataContent
        {
            { new StringContent("UpdatedProductName"), "Name" },
            { new StringContent("2023-09-10"), "ProduceDate" },
            { new StringContent("09034988741"), "ManufacturePhone" },
            { new StringContent("true"), "IsAvailable" },
            { new StringContent("MahdiUpdated@gmail.com"), "ManufactureEmail" }
        };

        // Act
        int nonExistentProductId = 999;  // Assume product with ID 999 does not exist
        var response = await client.PutAsync($"http://localhost:5053/UpdateProduct/{nonExistentProductId}", updateProductModelRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        // Check error message
        var errorResponse = await response.Content.ReadAsStringAsync();
        errorResponse.Should().Contain("محصول وجود ندارد یا متعلق به شما نیست");
    }
}
