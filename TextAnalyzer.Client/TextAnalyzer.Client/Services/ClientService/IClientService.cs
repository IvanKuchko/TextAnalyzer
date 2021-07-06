using System.Threading;
using System.Threading.Tasks;

namespace TextAnalyzer.Client.Services.ClientService
{
    public interface IClientService
    {
        Task<string> GetWithAuth(string url, CancellationToken token);
        Task<string> PostWithAuth(string url, object data, CancellationToken token);
    }
}