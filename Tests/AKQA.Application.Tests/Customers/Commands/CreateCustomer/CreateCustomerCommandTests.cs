using AKQA.Application.Customers.Commands.CreateCustomer;
using AKQA.Application.Extensions;
using AKQA.Application.Tests.Infrastructure;
using AKQA.Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AKQA.Application.Tests.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidRequest_HandlerReturnsCorrectViewModel()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateCustomerCommand.Handler(_context, mediatorMock.Object);
            var newCustomer = new Customer { FirstName = "Leonardo", MiddleName = "Da", LastName = "Vinci", Amount = new Decimal(145.2) };
            var newCustomerFullName = $"{newCustomer.FirstName} {newCustomer.MiddleName} {newCustomer.LastName}";
            var amountInWordsExpected = newCustomer.Amount.ToWords(new System.Globalization.CultureInfo("en"));

            // Act
            var result = await sut.Handle(new CreateCustomerCommand { FirstName = newCustomer.FirstName, MiddleName = newCustomer.MiddleName, LastName = newCustomer.LastName, Amount = newCustomer.Amount.ToString() }, CancellationToken.None);


            // Assert
            mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result)
                .Verifiable();

            Assert.NotNull(result);
            Assert.Equal(result.FullName, newCustomerFullName);
            Assert.Equal(result.FormattedAmount, amountInWordsExpected);

        }
    }
}
