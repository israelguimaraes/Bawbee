using Bawbee.Mobile.ReadModels.Entries;
using Bawbee.Mobile.Services;
using Bawbee.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Entries
{
    public class ListEntryViewModel : BaseViewModel
    {
        public ObservableCollection<EntryReadModel> Entries { get; set; }

        private readonly EntryService _entryService;

        public ListEntryViewModel()
        {
            _entryService = new EntryService();
        }

        public async Task LoadEntries()
        {
            Entries = await _entryService.GetEntries();
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
