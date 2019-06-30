using AKQA.Application.Customers.Commands.CreateCustomer;
using AKQA.Application.Customers.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AKQA.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CustomerController : BaseController
    {
        /// <summary>
        /// Process the customer request.
        /// </summary>
        /// <param name="resource">Customer data.</param>
        /// <returns>Customer Name and Amount as words.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerViewModel>> Process([FromBody]CreateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}