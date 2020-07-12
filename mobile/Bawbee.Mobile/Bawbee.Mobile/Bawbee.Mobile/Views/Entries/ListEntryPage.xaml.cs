
using Bawbee.Mobile.ViewModels.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bawbee.Mobile.Views.Entries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListEntryPage : ContentPage
    {
        public ListEntryPage()
        {
            InitializeComponent();

            BindingContext = new ListEntryViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<ListEntryViewModel>(this, nameof(ListEntryViewModel.OpenModalNewEntryCommand), async (msg) =>
            {
                await Navigation.PushAsync(new AddEntryPage());
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<ListEntryViewModel>(this, nameof(ListEntryViewModel.OpenModalNewEntryCommand));
        }
    }
}