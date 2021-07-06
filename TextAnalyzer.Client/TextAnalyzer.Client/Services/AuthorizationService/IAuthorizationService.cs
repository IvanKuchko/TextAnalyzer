using System.Threading;
using System.Threading.Tasks;

namespace TextAnalyzer.Client.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        Task<bool> Request(string url, object data, CancellationToken token);
    }
}