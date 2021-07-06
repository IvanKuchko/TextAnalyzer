using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TextAnalyzer.Client.Models;
using TextAnalyzer.Client.Models.Responses;
using TextAnalyzer.Client.Services.ClientService;

namespace TextAnalyzer.Client.Services.AnalyzerService
{
    public class AnalyzerService : IAnalyzerService
    {
        private readonly IClientService _clientService;

        public AnalyzerService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task AddValue(IEnumerable<Avatar> avatars, CancellationToken token)
        {
            await _clientService.PostWithAuth(AppConstants.AddValueUrl, avatars, token);
        }

<<<<<<< HEAD
        public async Task<CompareResponse> CompareWithRandomUser(IEnumerable<Avatar> avatars, CancellationToken token)
        {
            var source = await _clientService.PostWithAuth(AppConstants.CompareWithRandomUserUrl, avatars, token);
            return JsonConvert.DeserializeObject<CompareResponse>(source);
        }

=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
        public async Task<CompareResponse> CompareWithUser(object data, CancellationToken token)
        {
            var source = await _clientService.PostWithAuth(AppConstants.CompareWithUserUrl, data, token);
            return JsonConvert.DeserializeObject<CompareResponse>(source);
        }

        public async Task<CompareResponse> CompareWithUsers(CancellationToken token)
        {
            var source = await _clientService.GetWithAuth(AppConstants.CompareWithUsersUrl, token);
            return JsonConvert.DeserializeObject<CompareResponse>(source);
        }

        public async Task<UsersResponse> GetUsers(CancellationToken token)
        {
            var source = await _clientService.GetWithAuth(AppConstants.GetUsersUrl, token);
            return JsonConvert.DeserializeObject<UsersResponse>(source);
        }
    }
}
