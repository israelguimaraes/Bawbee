using Bawbee.Application.Operations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bawbee.Application.UseCases.Categories.GetAllCategoriesByUser
{
    public class GetAllCategoriesByUserQuery : IRequest<OperationResult>
    {
        public int UserId { get; }

        public GetAllCategoriesByUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}
