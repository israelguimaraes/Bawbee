using Bawbee.Application.Mediator;

namespace Bawbee.Application.UseCases.Categories.GetAllCategoriesByUser
{
    public class GetAllCategoriesByUserQuery : BaseQuery
    {
        public int UserId { get; }

        public GetAllCategoriesByUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}
