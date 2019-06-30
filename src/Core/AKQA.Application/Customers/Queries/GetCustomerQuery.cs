using MediatR;

namespace AKQA.Application.Customers.Queries
{
    public class GetCustomerQuery : IRequest<CustomerViewModel>
    {
        public string CustomerId { get; set; }
    }
}
