using System.Threading.Tasks;

namespace TextAnalyzer.Client.Services.DialogService
{
    public interface IDialogService
    {
        Task ShowAlert(string tirle, string message, string cancel);
    }
}