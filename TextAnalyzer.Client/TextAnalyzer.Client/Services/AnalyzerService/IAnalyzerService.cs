using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TextAnalyzer.Client.Models;
using TextAnalyzer.Client.Models.Responses;

namespace TextAnalyzer.Client.Services.AnalyzerService
{
    public interface IAnalyzerService
    {
        Task AddValue(IEnumerable<Avatar> avatars, CancellationToken token);
        Task<UsersResponse> GetUsers(CancellationToken token);
        Task<CompareResponse> CompareWithUsers(CancellationToken token);
        Task<CompareResponse> CompareWithUser(object data, CancellationToken token);
<<<<<<< HEAD
        Task<CompareResponse> CompareWithRandomUser(IEnumerable<Avatar> avatars, CancellationToken token);
=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
    }
}