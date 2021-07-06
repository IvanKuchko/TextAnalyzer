using SimpleInjector;
using TextAnalyzer.Client.Services.AnalyzerService;
using TextAnalyzer.Client.Services.AuthorizationService;
using TextAnalyzer.Client.Services.ClientService;
using TextAnalyzer.Client.Services.DialogService;
using TextAnalyzer.Client.Services.NavigationService;
using TextAnalyzer.Client.Services.NetworkService;
using TextAnalyzer.Client.Services.SettingsService;
using TextAnalyzer.Client.ViewModels;
using TextAnalyzer.Client.Views;

namespace TextAnalyzer.Client
{
    public class ViewResolver
    {
        public static Container Container { get; protected set; }

        public virtual void CreateContainer()
        {
            Container = new Container();

            Container.Register<ISettingsService, SettingsService>(Lifestyle.Singleton);
            Container.Register<INetworkService, NetworkService>(Lifestyle.Singleton);
            Container.Register<IAuthorizationService, AuthorizationService>(Lifestyle.Singleton);
            Container.Register<IClientService, ClientService>(Lifestyle.Singleton);
            Container.Register<INavigationService, NavigationService>(Lifestyle.Singleton);
            Container.Register<IDialogService, DialogService>(Lifestyle.Singleton);
            Container.Register<IAnalyzerService, AnalyzerService>(Lifestyle.Singleton);

            Container.Register<AuthorizationViewModel>();
            Container.Register<RegistrationViewModel>();
            Container.Register<AnalyzerViewModel>();
            Container.Register<CompareViewModel>();

            Container.Register<AuthorizationPage>();
            Container.Register<RegistrationPage>();
            Container.Register<AnalyzerPage>();
            Container.Register<ComparePage>();

            NavigationService.RegisterRoutes();
        }

        public static T Resolve<T>() where T : class => Container.GetInstance<T>();

        public static void DeleteContainer()
        {
            Container.Dispose();
            Container = null;
            CustomRouting.UnregisterRoutes();
        }
    }
}
