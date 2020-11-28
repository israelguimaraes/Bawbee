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
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as ListEntryViewModel).LoadCurrenthMonthEntries();

            MessagingCenter.Subscribe<ListEntryViewModel>(this, ListEntryViewModel.MessageKey.OpenModalNewEntry, async (msg) =>
            {
                await Navigation.PushAsync(new EntryTabbedPage());
            });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<ListEntryViewModel>(this, ListEntryViewModel.MessageKey.OpenModalNewEntry);
        }
    }
}