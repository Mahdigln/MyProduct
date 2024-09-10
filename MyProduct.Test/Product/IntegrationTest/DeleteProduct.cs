using FluentAssertions;
using MyProduct.Test.Product.AddProduct;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MyProduct.Test.Product.IntegrationTest;

public sealed class DeleteProductIntegrationTests : IClassFixture<ProductApiFactory>
{
    private readonly HttpClient _factory;

    public DeleteProductIntegrationTests(ProductApiFactory factory)
    {
        _factory = factory.CreateClient();
    }

    [Fact]
    public async Task DeleteProduct_ShouldReturnNoContent_WhenProductIsDeletedSuccessfully()
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

        // Assume productId is the ID of a product that belongs to this user and exists in the system
        int productIdToDelete = 10;

        // Act
        var response = await client.DeleteAsync($"http://localhost:5053/DeleteProduct?productId={productIdToDelete}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    [Fact]
    public async Task DeleteProduct_ShouldReturnBadRequest_WhenProductDoesNotExistOrBelongsToAnotherUser()
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

        // Assume productId does not exist or belongs to another user
        int nonExistentProductId = 999;

        // Act
        var response = await client.DeleteAsync($"http://localhost:5053/DeleteProduct?productId={nonExistentProductId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        // Check error message
        var errorResponse = await response.Content.ReadAsStringAsync();
        errorResponse.Should().Contain("محصول وجود ندارد یا متعلق به شما نیست");
    }
}
