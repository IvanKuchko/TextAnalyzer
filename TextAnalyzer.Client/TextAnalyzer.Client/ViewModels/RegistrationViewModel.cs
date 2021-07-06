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
    public class RegistrationViewModel : BaseViewModel
    {
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }
        private string login;

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        private string email;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private string password; 
        
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value); 
        }
        private string firstName;

        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value); 
        }
        private string lastName;

        public ICommand SignUpCommand { get; private set; }

        private readonly INavigationService _navigarionSercice;
        private readonly IDialogService _dialogSercice;
        private readonly INetworkService _networkService;
        private readonly IAuthorizationService _authorizationService;

        public RegistrationViewModel(
            INavigationService navigarionSercice,
            IDialogService dialogSercice,
            INetworkService networkService,
            IAuthorizationService authorizationService)
        {
            _navigarionSercice = navigarionSercice;
            _dialogSercice = dialogSercice;
            _networkService = networkService;
            _authorizationService = authorizationService;

            SignUpCommand = new AsyncCommand(SignUp);
        }

        private async Task SignUp()
        {
            bool isConnected = _networkService.IsConnected();

            if (isConnected)
            {
                var data = new SignUpRequest()
                {
                    Login = Login,
                    Email = Email,
                    Password = Password,
                    FirstName = FirstName,
                    LastName = LastName
                };

                var result = await _authorizationService.Request(AppConstants.SignUpUrl, data, TokenSource.Token);

                if (result)
                {
                    _navigarionSercice.GoToMainAsync();
                }
                else
                {
                    await _dialogSercice.ShowAlert(AppConstants.ErrorTitle, AppConstants.RegistrationErrorMessage, AppConstants.OkButton);
                }
            }
            else
            {
                await _dialogSercice.ShowAlert(AppConstants.WarningTitle, AppConstants.NoInternetConnectionMessage, AppConstants.OkButton);
            }
        }
    }
}
