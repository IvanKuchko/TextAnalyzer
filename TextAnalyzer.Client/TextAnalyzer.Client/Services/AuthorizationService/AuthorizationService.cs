using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextAnalyzer.Client.Models.Responses;
using TextAnalyzer.Client.Services.SettingsService;

namespace TextAnalyzer.Client.Services.AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _client;
        private readonly ISettingsService _settingsService;

        public AuthorizationService(
            ISettingsService settingsService)
        {
            _client = new HttpClient(CreateHandlerHandler())
            {
                BaseAddress = new Uri(AppConstants.BaseAddressUrl),
                Timeout = TimeSpan.FromSeconds(60),
            };
            _settingsService = settingsService;
        }

        public HttpClientHandler CreateHandlerHandler() => new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true
        };

        public async Task<bool> Request(string url, object data, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            try
            {
                string json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;
                request.Content = content;
                HttpResponseMessage response = await _client.SendAsync(request);
                var source = await response.Content.ReadAsStringAsync();
                var tokens = JsonConvert.DeserializeObject<TokenResponse>(source);
                _settingsService.SetAccessToken(tokens.Tokens.AccessToken);
                _settingsService.SetRefreshToken(tokens.Tokens.RefreshToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
