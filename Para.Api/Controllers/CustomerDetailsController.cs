using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Schema;


namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> GetAll()
        {
            var query = new GetAllCustomerDetailsQuery();
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> GetByCustomerId(long customerId)
        {
            var query = new GetCustomerDetailByIdQuery(customerId);
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost("{customerId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> Post(long customerId,[FromBody] CustomerDetailRequest request)
        {
            var command = new CreateCustomerDetailCommand(customerId,request);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerDetailRequest request)
        {
            var command = new UpdateCustomerDetailCommand(customerId, request);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var command = new DeleteCustomerDetailCommand(customerId);
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
