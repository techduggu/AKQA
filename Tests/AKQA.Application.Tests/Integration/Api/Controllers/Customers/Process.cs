using AKQA.Api;
using AKQA.Application.Customers.Commands.CreateCustomer;
using AKQA.Application.Customers.Queries;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AKQA.Application.Tests.Integration.Api.Controllers.Customers
{
    public class Process : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public Process(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenValidCreateCustomerCommand_ReturnsSuccessStatusCode()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "Leonardo",
                MiddleName = "Da",
                LastName = "Vinci",
                Amount = "104.58"
            };
            
            var content = Utilities.GetRequestContent(command);

            // Act
            var response = await _client.PostAsync($"{Utilities.AKQAApiURL}/api/customer/process", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidRequest_ReturnsBadRequestStatusCode()
        {
            // Arrange
            //Removed mandatory field - FirstName
            var command = new CreateCustomerCommand
            {
                MiddleName = "Da",
                LastName = "Vinci",
                Amount = "104.58"
            };

            var content = Utilities.GetRequestContent(command);
            
            // Act
            var response = await _client.PostAsync($"{Utilities.AKQAApiURL}/api/customer/process", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidCreateCustomerCommand_ReturnsValidCustomerViewModel()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "Leonardo",
                MiddleName = "Da",
                LastName = "Vinci",
                Amount = "104.58"
            };

            var content = Utilities.GetRequestContent(command);

            // Act
            var response = await _client.PostAsync($"{Utilities.AKQAApiURL}/api/customer/process", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<CustomerViewModel>(response);

            Assert.NotNull(vm);
            Assert.IsType<CustomerViewModel>(vm);
            Assert.NotEmpty(vm.FullName);
            Assert.NotEmpty(vm.FormattedAmount);
        }
    }
}
