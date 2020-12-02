using Bawbee.Mobile.ViewModels.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bawbee.Mobile.Views.Entries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpensePage : ContentPage
    {
        public AddExpensePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await (BindingContext as AddExpenseViewModel).Init();

            MessagingCenter.Subscribe<AddExpenseViewModel>(this, AddExpenseViewModel.MessageKey.EntryAdded, async (msg) =>
            {
                await Navigation.PopAsync();
            });

            MessagingCenter.Subscribe<AddExpenseViewModel>(this, AddExpenseViewModel.MessageKey.EntryFormInvalid, async (msg) =>
            {
                await DisplayAlert("Alert", "Check the required fields", "OK");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<AddExpenseViewModel>(this, AddExpenseViewModel.MessageKey.EntryAdded);
            MessagingCenter.Unsubscribe<AddExpenseViewModel>(this, AddExpenseViewModel.MessageKey.EntryFormInvalid);
        }
    }
}