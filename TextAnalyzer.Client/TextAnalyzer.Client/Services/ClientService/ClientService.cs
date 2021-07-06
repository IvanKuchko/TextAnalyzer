using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextAnalyzer.Client.Services.NetworkService;
using TextAnalyzer.Client.Services.SettingsService;

namespace TextAnalyzer.Client.Services.ClientService
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _client;
        private readonly ISettingsService _settingsService;
        private readonly INetworkService _networkService;

        public ClientService(
            ISettingsService settingsService,
            INetworkService networkService)
        {
            _client = new HttpClient(CreateHandlerHandler())
            {
                BaseAddress = new Uri(AppConstants.BaseAddressUrl),
<<<<<<< HEAD
                Timeout = TimeSpan.FromSeconds(6000)
=======
                Timeout = TimeSpan.FromSeconds(60)
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
            };
            _settingsService = settingsService;
            _networkService = networkService;

        }

        public HttpClientHandler CreateHandlerHandler() => new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true
        };

        public void AddAuthHeader(string accessToken)
        {
            if (accessToken != null) _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public void RemoveAuthHeader()
        {
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> GetWithAuth(string url, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Get;
                var accessToken = _settingsService.GetAccessToken();
                if (accessToken != null)
                    AddAuthHeader(accessToken);
                HttpResponseMessage response = await _client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> PostWithAuth(string url, object data, CancellationToken token)
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
                var accessToken = _settingsService.GetAccessToken();
                if (accessToken != null)
                    AddAuthHeader(accessToken);
                HttpResponseMessage response = await _client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
