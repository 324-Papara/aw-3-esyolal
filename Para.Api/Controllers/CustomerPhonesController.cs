using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPhoneController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerPhoneController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> GetAll()
        {
            var query = new GetAllCustomerPhonesQuery();
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Get(long customerId)
        {
            var query = new GetCustomerPhonesByCustomerIdQuery(customerId);
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerPhoneResponse>> Post([FromBody] CustomerPhoneRequest request)
        {
            var command = new CreateCustomerPhoneCommand(request);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerPhoneRequest request)
        {
            var command = new UpdateCustomerPhoneCommand(customerId, request);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var command = new DeleteCustomerPhoneCommand(customerId);
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
