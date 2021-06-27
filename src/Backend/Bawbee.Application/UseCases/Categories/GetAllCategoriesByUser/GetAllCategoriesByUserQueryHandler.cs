using Bawbee.Application.Operations;
using Bawbee.Application.UseCases.Shared;
using Bawbee.Application.UseCases.Users;
using Bawbee.Core.Aggregates.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.UseCases.Categories.GetAllCategoriesByUser
{

    public class GetAllCategoriesByUserQueryHandler : BaseCommandQuery, 
        IRequestHandler<GetAllCategoriesByUserQuery, OperationResult>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetAllCategoriesByUserQueryHandler(IUserReadRepository repository)
        {
            _userReadRepository = repository;
        }

        public async Task<OperationResult> Handle(GetAllCategoriesByUserQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Category> categories = await _userReadRepository.GetCategories(query.UserId);

            if (!categories.Any())
            {
                return NotFound("There no categories for user");
            }

            var result = CategoryMapper.Map(categories);
            return Ok(result);
        }

        //public async Task<IEnumerable<EntryCategoryReadModel>> Handle(GetAllCategoriesByUserQuery query, CancellationToken cancellationToken)
        //{
        //    IEnumerable<User> users = await _userReadRepository.GetAllCategories(query.UserId);



        //    var result = users.Select(u => new UserReadModel
        //    {
        //        UserId = u.Id,
        //        Email = u.Email,
        //        Name = u.LastName,
        //        LastName = u.LastName
        //    });

        //    return result;
        //}

    }
}
