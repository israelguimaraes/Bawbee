using Bawbee.Mobile.Models.Menu;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bawbee.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeftMenuPage : ContentPage
    {
        private MainPage RootPage => Application.Current.MainPage as MainPage;

        private List<HomeMenuItem> _menuItems;

        public LeftMenuPage()
        {
            InitializeComponent();

            _menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem { Title = "Dashboard", Type = MenuItemType.Dashboard },
                new HomeMenuItem { Title = "Entries", Type = MenuItemType.Entries },
                new HomeMenuItem { Title = "Reports", Type = MenuItemType.Reports },
                new HomeMenuItem { Title = "Credit Cards", Type = MenuItemType.CreditCards },
                new HomeMenuItem { Title = "Categories", Type = MenuItemType.Category },
                new HomeMenuItem { Title = "Bank Accounts", Type = MenuItemType.BankAccounts },
                new HomeMenuItem { Title = "Profile", Type = MenuItemType.Profile },
                new HomeMenuItem { Title = "Logout", Type = MenuItemType.Logout }
            };

            ListViewMenu.ItemsSource = _menuItems;
            ListViewMenu.SelectedItem = _menuItems.First();

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null) return;

                var menuSelected = (HomeMenuItem)e.SelectedItem;
                var pageId = (int)menuSelected.Type;

                await RootPage.NavigateFromMenu(pageId);
            };
        }
    }
}