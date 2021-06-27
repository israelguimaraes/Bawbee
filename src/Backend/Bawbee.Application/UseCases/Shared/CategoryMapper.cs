using Bawbee.Core.Aggregates.Users;
using Bawbee.SharedKernel.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Bawbee.Application.UseCases.Shared
{
    public class CategoryMapper
    {
        public static IEnumerable<Category> Map(IEnumerable<Category> categories)
        {
            if (categories.IsEmpty())
                return Enumerable.Empty<Category>();

            return categories;
        }
    }
}
