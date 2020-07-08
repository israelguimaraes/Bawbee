using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Views;
using Bawbee.Mobile.Views.Auth;
using Xamarin.Forms;

namespace Bawbee.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        private void SetMainPage()
        {
            var userHasAccessToken = !string.IsNullOrWhiteSpace(Settings.UserAcessToken);

            if (userHasAccessToken)
            {
                //MainPage = new NavigationPage(new DashboardPage());
                MainPage = new MainPage();
            }
            else
            {
                //MainPage = new NavigationPage(new RegisterPage());
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
