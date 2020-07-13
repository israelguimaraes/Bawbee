using Bawbee.Mobile.ViewModels.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Bawbee.Mobile.ViewModels.Entries.ListEntryViewModel;

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

            MessagingCenter.Subscribe<ListEntryViewModel>(this, MessageKey.OpenModalNewEntry, async (msg) =>
            {
                //var action = await DisplayActionSheet(null, "cancel", null, "Add Expense", "Add Income");

                //if (action == "Add Expense")
                //    await Navigation.PushAsync(new AddEntryPage());

                await Navigation.PushAsync(new EntryTabbedPage());
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<ListEntryViewModel>(this, MessageKey.OpenModalNewEntry);
        }
    }
}