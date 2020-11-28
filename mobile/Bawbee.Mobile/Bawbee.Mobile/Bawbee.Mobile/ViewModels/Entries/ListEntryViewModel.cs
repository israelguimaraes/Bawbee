using Bawbee.Mobile.ReadModels.Entries;
using Bawbee.Mobile.Services.Entries;
using Bawbee.Mobile.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Entries
{
    public class ListEntryViewModel : BaseViewModel
    {
        private readonly ExpenseService _entryService;

        public ListEntryViewModel()
        {
            _entryService = new ExpenseService();
            _entries = new ObservableCollection<EntryReadModel>();
        }

        public decimal TotalIncomes
        {
            get
            {
                return Entries.Where(e => e.Value > 0).Sum(e => e.Value);
            }
        }

        public decimal TotalExpenses
        {
            get
            {
                return Entries.Where(e => e.Value < 0).Sum(e => e.Value);
            }
        }

        private ObservableCollection<EntryReadModel> _entries;
        public ObservableCollection<EntryReadModel> Entries 
        {
            get => _entries;
            set
            {
                _entries = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalIncomes));
                OnPropertyChanged(nameof(TotalExpenses));
            }
        }

        public async Task LoadCurrenthMonthEntries()
        {
            IsBusy = true;

            Entries = await _entryService.GetCurrenthMonthEntries();

            IsBusy = false;
        }

        public ICommand OpenModalNewEntryCommand
        {
            get
            {
                return new Command(() =>
                {
                    MessagingCenter.Send(this, MessageKey.OpenModalNewEntry);
                });
            }
        }

        public class MessageKey
        {
            public const string OpenModalNewEntry = nameof(OpenModalNewEntryCommand);
        }
    }
}
