using Bawbee.Mobile.ViewModels.Auth;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bawbee.Mobile.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<RegisterViewModel>(this, RegisterViewModel.MessageKey.UserRegistered, async (msg) =>
            {
                await Navigation.PushAsync(new LoginPage());
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<RegisterViewModel>(this, RegisterViewModel.MessageKey.UserRegistered);
        }
    }
}