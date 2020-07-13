using Bawbee.Mobile.ReadModels.Entries;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Entries
{
    public class ListEntryViewModel
    {
        public ICollection<EntryReadModel> Entries { get; set; }

        public ListEntryViewModel()
        {
            GenerateFakeData();
        }

        private void GenerateFakeData()
        {
            Entries = new List<EntryReadModel>();

            int countItems = 5;
            for (int i = 1; i <= countItems; i++)
            {
                var entry = new EntryReadModel
                {
                    Id = i,
                    Description = $"lorem ipsum {i}",
                    Value = (i + 2) * 8.33m,
                    //Value = 123456789.98m,
                    CategoryName = $"category {i}",
                    BankAccountName = "ActivoBank",
                    IsPaid = i % 2 == 0,
                    CreatedAt = DateTime.Now.AddDays(i - 1)
                };

                if (i == countItems)
                {
                    entry.Value = 1_000_000.89m;
                }

                Entries.Add(entry);
            }
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
            public const string OpenModalNewEntry = nameof(OpenModalNewEntry);
        }
    }
}
