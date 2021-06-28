using Bawbee.Application.Mediator;
using Bawbee.Application.Operations;
using Bawbee.Application.UseCases.Shared;
using Bawbee.Core.Aggregates.Users;
using Bawbee.SharedKernel.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.UseCases.Categories.GetAllCategoriesByUser
{
    public class GetAllCategoriesByUserQueryHandler : BaseCommandQuery,
        ICommandHandler<GetAllCategoriesByUserQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetAllCategoriesByUserQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<OperationResult> Handle(GetAllCategoriesByUserQuery query, CancellationToken cancellationToken)
        {
            var categories = await _userRepository.GetCategories(query.UserId);

            if (categories.IsEmpty())
            {
                return NotFound("You don't have categories yet.");
            }

            var result = CategoryMapper.Map(categories);
            return Ok(result);
        }
    }
}
