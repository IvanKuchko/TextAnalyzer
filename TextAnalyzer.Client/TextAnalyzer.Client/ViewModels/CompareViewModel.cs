using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TextAnalyzer.Client.Models;
using TextAnalyzer.Client.Models.Requests;
using TextAnalyzer.Client.Services.AnalyzerService;
using TextAnalyzer.Client.Services.DialogService;
using TextAnalyzer.Client.Services.NavigationService;
using TextAnalyzer.Client.Services.NetworkService;
using TextAnalyzer.Client.ViewModels.Commands;

namespace TextAnalyzer.Client.ViewModels
{
    public class CompareViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users
        {
            get => users;
            set => SetProperty(ref users, value);
        }
        private ObservableCollection<User> users;

        public User SelectedUser
        {
            get => selectedUser;
            set => SetProperty(ref selectedUser, value);
        }
        private User selectedUser;

        public string Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }
        private string result;

        public ICommand GetUsersCommand { get; private set; }
        public ICommand CompareWithUserCommand { get; private set; }
        public ICommand CompareWithUsersCommand { get; private set; }

        private readonly INavigationService _navigarionSercice;
        private readonly IDialogService _dialogSercice;
        private readonly INetworkService _networkService;
        private readonly IAnalyzerService _analyzerService;

        public CompareViewModel(
            INavigationService navigarionSercice,
            IDialogService dialogSercice,
            INetworkService networkService,
            IAnalyzerService analyzerService)
        {
            _navigarionSercice = navigarionSercice;
            _dialogSercice = dialogSercice;
            _networkService = networkService;
            _analyzerService = analyzerService;

            GetUsersCommand = new AsyncCommand(GetUsers);
            CompareWithUserCommand = new AsyncCommand(CompareWithUser);
            CompareWithUsersCommand = new AsyncCommand(CompareWithUsers);
        }

<<<<<<< HEAD

=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
        private async Task CompareWithUsers()
        {
            bool isConnected = _networkService.IsConnected();

            if (isConnected)
            {

                var response = await _analyzerService.CompareWithUsers(TokenSource.Token);
                Result = response.Message;
            }
            else
            {
                await _dialogSercice.ShowAlert(AppConstants.WarningTitle, AppConstants.NoInternetConnectionMessage, AppConstants.OkButton);
            }
        }

        private async Task CompareWithUser()
        {
            bool isConnected = _networkService.IsConnected();

            if (isConnected)
            {
                var request = new CompareWithUserRequest()
                {
                    UserId = SelectedUser.Id
                };
                var response = await _analyzerService.CompareWithUser(request, TokenSource.Token);
                Result = response.Message;
            }
            else
            {
                await _dialogSercice.ShowAlert(AppConstants.WarningTitle, AppConstants.NoInternetConnectionMessage, AppConstants.OkButton);
            }
        }

        private async Task GetUsers()
        {
            bool isConnected = _networkService.IsConnected();

            if (isConnected)
            {
                var response = await _analyzerService.GetUsers(TokenSource.Token);
                Users = new ObservableCollection<User>(response.Users);
            }
            else
            {
                await _dialogSercice.ShowAlert(AppConstants.WarningTitle, AppConstants.NoInternetConnectionMessage, AppConstants.OkButton);
            }
        }
    }
}
