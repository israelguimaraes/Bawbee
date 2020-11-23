using Bawbee.Domain.AggregatesModel.Entries;
using Bawbee.Domain.AggregatesModel.Users;
using Bawbee.Infra.Data.ReadInterfaces;
using Raven.Client.Documents.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.RavenDB.Repositories
{
    public class AdminRavenDBRepository : IAdminRavenDBRepository
    {
        private readonly IDocumentStoreHolder _documentStore;
        private readonly IEntryRepository _entryRepository;
        private readonly IUserRepository _userRepository;

        public AdminRavenDBRepository(
            IDocumentStoreHolder documentStore, 
            IEntryRepository entryRepository,
            IUserRepository userRepository)
        {
            _documentStore = documentStore;
            _entryRepository = entryRepository;
            _userRepository = userRepository;
        }

        public async Task CreateInitialData()
        {
            var user = User.UserFactory.CreateNewPlataformUser("Israel", "Guimarães", "israel@gmail.com", "123456");

            await _userRepository.Add(user);

            var bankAccount = user.BankAccounts.First();
            var categories = user.Categories.ToList();

            for (int i = 0; i < 9; i++)
            {
                var expense = new Expense(
                    $"expense {i}", 
                    (i * 1.45m), 
                    true,
                    null, 
                    DateTime.Now.AddDays(-i), 
                    user.Id, 
                    bankAccount.Id,
                    categories[i].Id);

                await _entryRepository.Add(expense);
            }
        }

        public Task DeleteAllDocuments()
        {
            var query = @"from @all_docs";
            return _documentStore.Store.Operations.SendAsync(new DeleteByQueryOperation(query));
        }
    }
}
