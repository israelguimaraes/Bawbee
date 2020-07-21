using Bawbee.Application.Query.Users.Interfaces;
using Bawbee.Application.Query.Users.Queries;
using Bawbee.Application.Query.Users.Queries.Entries;
using Bawbee.Application.Query.Users.ReadModels;
using Bawbee.Application.Query.Users.ReadModels.Entries;
using Bawbee.Domain.Core.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.Query.Users.Handlers
{
    public class EntryQueryHandler
        : ICommandQueryHandler<GetAllEntriesByUser, IEnumerable<EntryReadModel>>
    {
        private readonly IEntryReadRepository _entryReadRepository;

        public EntryQueryHandler(IEntryReadRepository entryReadRepository)
        {
            _entryReadRepository = entryReadRepository;
        }

        public async Task<IEnumerable<EntryReadModel>> Handle(GetAllEntriesByUser query, CancellationToken cancellationToken)
        {
            var entries = await _entryReadRepository.GetAllByUser(query.UserId);

            return entries.Select(e => new EntryReadModel
            {
                Id = e.EntryId,
                Description = e.Description,
                Value = e.Value,
                CategoryName = e.EntryCategoryName,
                BankAccountName = e.BankAccountName,
                IsPaid = e.IsPaid,
                CreatedAt = e.CreatedAt
            });
        }
    }
}
