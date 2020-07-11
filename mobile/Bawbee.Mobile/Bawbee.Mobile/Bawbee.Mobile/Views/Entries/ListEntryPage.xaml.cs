using Bawbee.Mobile.ReadModels.Entries;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bawbee.Mobile.Views.Entries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListEntryPage : ContentPage
    {
        public ICollection<EntryReadModel> Entries { get; set; }

        public ListEntryPage()
        {
            InitializeComponent();

            Entries = new List<EntryReadModel>();

            GenerateFakeData();

            BindingContext = this;
        }

        private void GenerateFakeData()
        {
            for (int i = 1; i <= 30; i++)
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

                if (i == 30)
                {
                    entry.Value = 1_000_000.89m;
                }
                
                Entries.Add(entry);
            }
        }
    }
}