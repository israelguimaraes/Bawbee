using Bawbee.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bawbee.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await (BindingContext as DashboardViewModel).Init();

            //MessagingCenter.Subscribe<DashboardViewModel>(this, MessageKey.OpenModalNewEntry, async (msg) =>
            //{
            //    await Navigation.PushAsync(new EntryTabbedPage());
            //});
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //MessagingCenter.Unsubscribe<DashboardViewModel>(this, MessageKey.OpenModalNewEntry);
        }
    }
}
