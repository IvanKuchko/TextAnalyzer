namespace TextAnalyzer.Client.Services.SettingsService
{
    public interface ISettingsService
    {
        string GetAccessToken();
        string GetRefreshToken();
        void SetAccessToken(string accessToken);
        void SetRefreshToken(string refreshToken);
    }
}