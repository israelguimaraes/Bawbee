using Bawbee.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bawbee.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardViewModel Dashboard { get; set; }

        public DashboardPage()
        {
            InitializeComponent();

            Dashboard = new DashboardViewModel();
        }
    }
}
