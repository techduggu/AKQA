using AKQA.Application.Customers.Queries;
using AKQA.Application.Extensions;
using AKQA.Application.Interfaces;
using AKQA.Domain.Entities;
using MediatR;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace AKQA.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CustomerViewModel>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Amount { get; set; }

        public class Handler : IRequestHandler<CreateCustomerCommand, CustomerViewModel>
        {
            private readonly IAppDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<CustomerViewModel> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    Amount = decimal.Parse(request.Amount)
                };

                _context.Customers.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return new CustomerViewModel
                {
                    FullName = $"{entity.FirstName} {entity.MiddleName} {entity.LastName}",
                    FormattedAmount = entity.Amount.ToWords(new CultureInfo("en"))
                };
            }
        }
    }
}
