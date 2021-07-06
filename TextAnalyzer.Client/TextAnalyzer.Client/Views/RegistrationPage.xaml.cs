using TextAnalyzer.Client.Services.NavigationService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextAnalyzer.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent(); 
            var context = CustomRouting.GetOrCreateViewModel(GetType());
            BindingContext = context;
        }
    }
}