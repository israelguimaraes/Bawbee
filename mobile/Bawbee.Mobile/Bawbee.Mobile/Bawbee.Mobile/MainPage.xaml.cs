using Bawbee.Mobile.Models.Menu;
using Bawbee.Mobile.Views;
using Bawbee.Mobile.Views.Entries;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bawbee.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private Dictionary<int, NavigationPage> _menuPages = new Dictionary<int, NavigationPage>();

        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            this.Master = new LeftMenuPage();
            this.Detail = new NavigationPage(new DashboardPage());

            _menuPages.Add((int)MenuItemType.Dashboard, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!_menuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Dashboard:
                        _menuPages.Add(id, new NavigationPage(new DashboardPage()));
                        break;
                    case (int)MenuItemType.Entries:
                        _menuPages.Add(id, new NavigationPage(new ListEntryPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        MessagingCenter.Send(this, MessageKey.Logout);
                        return;
                }
            }

            var nextPage = _menuPages[id];

            if (nextPage != null && Detail != nextPage)
            {
                this.Detail = nextPage;

                if (App.IsAndroid)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        public class MessageKey
        {
            public const string Logout = nameof(Logout);
        }
    }
}
