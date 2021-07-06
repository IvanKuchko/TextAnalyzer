using System.Threading.Tasks;
using System.Windows.Input;
using TextAnalyzer.Client.Models.Requests;
using TextAnalyzer.Client.Services.AuthorizationService;
using TextAnalyzer.Client.Services.DialogService;
using TextAnalyzer.Client.Services.NavigationService;
using TextAnalyzer.Client.Services.NetworkService;
using TextAnalyzer.Client.ViewModels.Commands;

namespace TextAnalyzer.Client.ViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {
        public string Login
        {
            get => login; 
            set => SetProperty(ref login, value); 
        }
        private string login; 
     
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private string password;

        public ICommand SignInCommand { get; private set; }
        public ICommand GoToRegistrationCommand { get; private set; }

        private readonly INavigationService _navigarionSercice;
        private readonly IDialogService _dialogSercice;
        private readonly INetworkService _networkService;
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationViewModel(
            INavigationService navigarionSercice,
            IDialogService dialogSercice,
            INetworkService networkService,
            IAuthorizationService authorizationService)
        {
            _navigarionSercice = navigarionSercice;
            _dialogSercice = dialogSercice;
            _networkService = networkService;
            _authorizationService = authorizationService;

            SignInCommand = new AsyncCommand(SignIn);
            GoToRegistrationCommand = new AsyncCommand(GoToRegistration);
        }

        private async Task GoToRegistration()
        {
            await _navigarionSercice.NavigateToAsync<RegistrationViewModel>();
        }

        private async Task SignIn()
        {
            bool isConnected = _networkService.IsConnected();

            if (isConnected)
            {
                var data = new SignInRequest()
                {
                    Login = Login,
                    Password = Password
                };

                var result = await _authorizationService.Request(AppConstants.SignInUrl, data, TokenSource.Token);

                if (result)
                {
                    _navigarionSercice.GoToMainAsync();
                }
                else
                {
                    await _dialogSercice.ShowAlert(AppConstants.ErrorTitle, AppConstants.AuthorizationErrorMessage, AppConstants.OkButton);
                }
            }
            else
            {
                await _dialogSercice.ShowAlert(AppConstants.WarningTitle, AppConstants.NoInternetConnectionMessage, AppConstants.OkButton);
            }
        }
    }
}
