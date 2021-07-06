using System.Threading.Tasks;
using Xamarin.Forms;

namespace TextAnalyzer.Client.Services.DialogService
{
    public class DialogService : IDialogService
    {
        public async Task ShowAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
