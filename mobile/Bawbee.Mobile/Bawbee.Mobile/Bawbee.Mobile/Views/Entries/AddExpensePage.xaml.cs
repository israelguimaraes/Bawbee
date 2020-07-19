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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<AddExpenseViewModel>(this, AddExpenseViewModel.MessageKey.EntryAdded, async (msg) => 
            {
                await Navigation.PopAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<AddExpenseViewModel>(this, AddExpenseViewModel.MessageKey.EntryAdded);
        }
    }
}