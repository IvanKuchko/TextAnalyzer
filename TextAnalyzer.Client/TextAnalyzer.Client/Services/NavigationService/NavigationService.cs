using System.Threading.Tasks;
using TextAnalyzer.Client.ViewModels;
using TextAnalyzer.Client.Views;
using Xamarin.Forms;

namespace TextAnalyzer.Client.Services.NavigationService
{
    public class NavigationService : INavigationService
    {
        private NavigationPage _rootPage => Application.Current.MainPage as NavigationPage;

        public NavigationService()
        {

        }

        public static void RegisterRoutes()
        {
            CustomRouting.RegisterRoute(typeof(AuthorizationViewModel), typeof(AuthorizationPage));
            CustomRouting.RegisterRoute(typeof(RegistrationViewModel), typeof(RegistrationPage));
            CustomRouting.RegisterRoute(typeof(AnalyzerViewModel), typeof(AnalyzerPage));
            CustomRouting.RegisterRoute(typeof(CompareViewModel), typeof(ComparePage));
        }

        public async Task GoBackAsync(bool animated = true)
        {
            await _rootPage.Navigation.PopAsync();
        }

        public async Task GoBackModalAsync(bool animated = true)
        {
            await _rootPage.Navigation.PopModalAsync();
        }

        public void GoToAuthorizationAsync()
        {
            Application.Current.MainPage = ViewResolver.Resolve<AuthorizationPage>();
        }

        public void GoToMainAsync()
        {
            Application.Current.MainPage = ViewResolver.Resolve<AnalyzerPage>();
        }

        public  async Task NavigateToAsync<TViewModel>(bool animated = true)
        {
            if (CustomRouting.GetPage(typeof(TViewModel)) is Page page)
            {
                await _rootPage.Navigation.PushAsync(page);
            }
        }

        public async Task NavigateToModalAsync<TViewModel>(bool animated = true)
        {
            if (CustomRouting.GetPage(typeof(TViewModel)) is Page page)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(page);
            }
        }
    }
}
