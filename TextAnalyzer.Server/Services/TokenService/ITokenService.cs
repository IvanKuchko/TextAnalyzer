using TextAnalyzer.Server.Models;

namespace TextAnalyzer.Server.Services.TokenService
{
    public interface ITokenService
    {
        bool AccessVerefy(string accessToken);
        Tokens GenerateTokens(User user);
        Tokens RefreshTokens(string refreshToken);
    }
}