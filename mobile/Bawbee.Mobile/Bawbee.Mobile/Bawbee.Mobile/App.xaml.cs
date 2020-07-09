using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Views;
using Bawbee.Mobile.Views.Auth;
using Xamarin.Forms;

namespace Bawbee.Mobile
{
    public partial class App : Application
    {
        public static bool IsAndroid = Device.RuntimePlatform == Device.Android;

        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        private void SetMainPage()
        {
            var userHasAccessToken = !string.IsNullOrWhiteSpace(Settings.UserAcessToken);

            // TODO: implement token
            if (true)
            {
                MainPage = new MainPage();
            }
            else
            {
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
