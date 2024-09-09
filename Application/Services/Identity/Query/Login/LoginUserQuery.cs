using BuildingBlocks.CQRS;

namespace Application.Services.Identity.Query.Login;

public sealed class LoginUserQueryRequest : IQuery<LoginUserQueryResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public sealed class LoginUserQueryResponse
{
    public AccessToken AccessToken { get; set; }

}
public sealed class AccessToken
{
    public string Token { get; set; }
    public string TokenType { get; set; }
}