using Bawbee.Mobile.ViewModels.Entries;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bawbee.Mobile.Views.Entries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpensePage : ContentPage
    {
        public AddEntryViewModel AddEntryViewModel { get; set; }

        public AddExpensePage()
        {
            InitializeComponent();

            AddEntryViewModel = new AddEntryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<AddEntryViewModel>(this, AddEntryViewModel.MessageKey.EntryAdded, async (msg) => 
            {
                await Navigation.PopAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<AddEntryViewModel>(this, AddEntryViewModel.MessageKey.EntryAdded);
        }
    }
}