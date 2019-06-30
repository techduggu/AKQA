using AKQA.Application.Exceptions;
using AKQA.Application.Extensions;
using AKQA.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AKQA.Application.Customers.Queries
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerViewModel>
    {
        private readonly IAppDbContext _context;

        public GetCustomerQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerViewModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .Where(e =>
                e.CustomerId == new Guid(request.CustomerId))
                .FirstOrDefaultAsync(cancellationToken);

            if (entity.Equals(null))
            {
                throw new NotFoundException(nameof(entity), request.CustomerId);
            }

            return new CustomerViewModel
            {
                FullName = $"{entity.FirstName} {entity.MiddleName} {entity.LastName}",
                FormattedAmount = entity.Amount.ToWords(new CultureInfo("en"))
            };
        }
    }
}
