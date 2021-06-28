using Bawbee.Application.UseCases.Categories.GetAllCategoriesByUser;
using Bawbee.Core.Aggregates.Users;
using Bawbee.SharedKernel.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Bawbee.Application.UseCases.Shared
{
    public class CategoryMapper
    {
        public static IEnumerable<CategoryReadModel> Map(IEnumerable<Category> categories)
        {
            if (categories.IsEmpty())
                return Enumerable.Empty<CategoryReadModel>();

            return categories.Select(x => Map(x)).ToArray();
        }

        private static CategoryReadModel Map(Category category)
        {
            if (category == null) return null;

            return new CategoryReadModel(category.Id, category.Name);
        }
    }
}
