using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TextAnalyzer.Client.Services.AnalyzerService;
using TextAnalyzer.Client.Services.DialogService;
using TextAnalyzer.Client.Services.NavigationService;
using TextAnalyzer.Client.Services.NetworkService;
using TextAnalyzer.Client.Services.SettingsService;
using TextAnalyzer.Client.ViewModels.Commands;
using System.Timers;
using TextAnalyzer.Client.Models;

namespace TextAnalyzer.Client.ViewModels
{
    public class AnalyzerViewModel : BaseViewModel
    {
        public string Text
        {
            get => text;
            set
            {
                TextChanged(value); 
                SetProperty(ref text, value); 
            }
        }
        private string text;

        public string Output
        {
            get => output; 
            set => SetProperty(ref output, value); 
        }
        private string output;

        public ICommand AddValueCommand { get; private set; }
        public ICommand GoToCompareCommand { get; private set; }
<<<<<<< HEAD
        public ICommand CompareWithRandomUserCommand { get; private set; }
=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb


        private readonly INavigationService _navigarionSercice;
        private readonly IDialogService _dialogSercice;
        private readonly INetworkService _networkService;
        private readonly IAnalyzerService _analyzerService;

        public AnalyzerViewModel(
            INavigationService navigarionSercice,
            IDialogService dialogSercice,
            INetworkService networkService,
            IAnalyzerService analyzerService)
        {
            _navigarionSercice = navigarionSercice;
            _dialogSercice = dialogSercice;
            _networkService = networkService;
            _analyzerService = analyzerService;

            AddValueCommand = new AsyncCommand(AddValue);
            GoToCompareCommand = new AsyncCommand(GoToCompare);
<<<<<<< HEAD
            CompareWithRandomUserCommand = new AsyncCommand(CompareWithRandomUser);
=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb

            SetTimer();
        }

        private async Task AddValue()
        {
            bool isConnected = _networkService.IsConnected();

            if (isConnected)
            {
                await _analyzerService.AddValue(TemporaryAvatar, TokenSource.Token);
<<<<<<< HEAD
                TemporaryAvatar.Clear();
                Text = string.Empty;
=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
            }
            else
            {
                await _dialogSercice.ShowAlert(AppConstants.WarningTitle, AppConstants.NoInternetConnectionMessage, AppConstants.OkButton);
            }
<<<<<<< HEAD

=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
        }

        private async Task GoToCompare()
        {
            await _navigarionSercice.NavigateToModalAsync<CompareViewModel>();
        }

        private static Timer _timer;
        private List<Avatar> TemporaryAvatar = new List<Avatar>();
        private string _textnow = string.Empty;
        private string _textbefore = string.Empty;
        private int _beforelength = 0;
        private static int _timerTick = 0;

        private static void SetTimer()
        {
            _timer = new Timer(1);
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _timerTick++;
        }

        private void TextChanged(string value)
        {
            _textnow  =  value;

            if (_textnow.Length < _beforelength && _textnow.Length != 0)
            {
                if (_timerTick > 35) _timerTick = 0;
                else
                {
                    TemporaryAvatar.Add(new Avatar()
                    {
                        CharPair = "back" + Convert.ToString(_textbefore[_textbefore.Length - 1]),
                        Delay = _timerTick,
                        Identical = 0
                    });
                }
                _textbefore = value;
                _timerTick = 0;
            }
            else
            {
                if (_beforelength == 0)
                {
                    if (_textnow.Length == 0)
                    {; }
                    else
                    {
                        if (_timerTick > 35) _timerTick = 0;
                        else
                        {
                            TemporaryAvatar.Add(new Avatar()
                            {
                                CharPair = Convert.ToString(_textbefore[_textbefore.Length - 1]),
                                Delay = _timerTick,
                                Identical = 0
                            });
                        }
                        _textbefore = value;
                        _timerTick = 0;
                    }
                }
                else
                {
                    if (_timerTick > 35)
                    {
                        _timerTick = 0;
                        _textbefore = value;
                    }
                    else
                    {
                        _textbefore = value;
                        if (_textbefore.Length != 0)
                        {
                            TemporaryAvatar.Add(new Avatar()
                            {
                                CharPair = Convert.ToString(_textbefore[_textbefore.Length - 2]) + Convert.ToString(_textbefore[_textbefore.Length - 1]),
                                Delay = _timerTick,
                                Identical = 0 
                            });
                        }
                    }
                    _timerTick = 0;
                }
                _beforelength = _textbefore.Length;
            }
        }
<<<<<<< HEAD
        private async Task CompareWithRandomUser()
        {
            bool isConnected = _networkService.IsConnected();
            if (isConnected)
            {

                var response = await _analyzerService.CompareWithRandomUser(TemporaryAvatar, TokenSource.Token);
                Output = response.Message;
                TemporaryAvatar.Clear();
                Text = string.Empty;
            }
            else
            {
                await _dialogSercice.ShowAlert(AppConstants.WarningTitle, AppConstants.NoInternetConnectionMessage, AppConstants.OkButton);
            }

        }
=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
    }
}
