using System.Threading.Tasks;

namespace TextAnalyzer.Client.Services.NavigationService
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(bool animated = true);
        Task NavigateToModalAsync<TViewModel>(bool animated = true);
        Task GoBackAsync(bool animated = true);
        Task GoBackModalAsync(bool animated = true);
        void GoToAuthorizationAsync();
        void GoToMainAsync();
    }
}