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
    }
}