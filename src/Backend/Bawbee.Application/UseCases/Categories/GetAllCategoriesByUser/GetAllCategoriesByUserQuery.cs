using Bawbee.Application.Bus;

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
