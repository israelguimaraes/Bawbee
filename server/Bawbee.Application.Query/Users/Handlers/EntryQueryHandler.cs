//using Bawbee.Application.Query.Users.Interfaces;
//using Bawbee.Application.Query.Users.Queries;
//using Bawbee.Application.Query.Users.Queries.Entries;
//using Bawbee.Application.Query.Users.ReadModels;
//using Bawbee.Application.Query.Users.ReadModels.Entries;
//using Bawbee.Domain.Core.Commands;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Bawbee.Application.Query.Users.Handlers
//{
//    public class EntryQueryHandler
//        : ICommandQueryHandler<GetAllEntriesByUser, IEnumerable<EntryReadModel>>
//    {
//        private readonly IUserReadRepository _userReadRepository;

//        public EntryQueryHandler(IUserReadRepository userReadRepository)
//        {
//            _userReadRepository = userReadRepository;
//        }

//        public Task<IEnumerable<EntryReadModel>> Handle(GetAllEntriesByUser query, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
