using Bawbee.Mobile.ViewModels.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bawbee.Mobile.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<LoginViewModel>(this, LoginViewModel.MessageKey.OpenRegisterPage, async (msg) =>
            {
                await Navigation.PushAsync(new RegisterPage());
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<LoginViewModel>(this, LoginViewModel.MessageKey.OpenLoginPage);
            MessagingCenter.Unsubscribe<LoginViewModel>(this, LoginViewModel.MessageKey.OpenRegisterPage);
        }
    }
}