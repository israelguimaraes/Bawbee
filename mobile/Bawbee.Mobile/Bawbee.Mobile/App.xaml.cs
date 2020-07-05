using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bawbee.Mobile.Services;
using Bawbee.Mobile.Views;
using Bawbee.Mobile.Views.Auth;

namespace Bawbee.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();

            MainPage = new NavigationPage(new RegisterPage());
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
