using FluentValidation;
using System.Text.RegularExpressions;

namespace AKQA.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(60).NotEmpty();
            RuleFor(x => x.MiddleName).MaximumLength(60);
            RuleFor(x => x.LastName).MaximumLength(60);
            RuleFor(x => x.Amount).MaximumLength(15).NotEmpty().Matches(new Regex(@"^([0-9]+(?:[\.][0-9]*)?|\.[0-9]+)$"));
        }
    }
}
