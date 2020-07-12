﻿using Bawbee.Mobile.Helpers;
using Bawbee.Mobile.Models.Menu;
using Bawbee.Mobile.ViewModels.Auth;
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
            if (false)
            {
                GoToMainPage();
            }
            else
            {
                GoToLoginPage();
            }
        }

        private void GoToMainPage()
        {
            MainPage = new MainPage();
        }

        private void GoToLoginPage()
        {
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            MessagingCenter.Subscribe<LoginViewModel>(this, nameof(LoginViewModel.LoginCommand), (msg) =>
            {
                MainPage = new MainPage();
            });

            MessagingCenter.Subscribe<MainPage>(this, nameof(MenuItemType.Logout), (msg) =>
            {
                Settings.UserAcessToken = null;
                GoToLoginPage();
            });
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
