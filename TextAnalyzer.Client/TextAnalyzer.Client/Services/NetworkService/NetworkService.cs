using Xamarin.Essentials;

namespace TextAnalyzer.Client.Services.NetworkService
{
    public class NetworkService : INetworkService
    {
        public bool IsConnected() => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
