using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerAddressController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
        {
            var query = new GetAllCustomerAddressesQuery();
            var result = await mediator.Send(query);
            return result;
        }

        [HttpGet("{addressId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> Get(long addressId)
        {
            var query = new GetCustomerAddressByIdQuery(addressId);
            var result = await mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerAddressResponse>> Post([FromBody] CustomerAddressRequest request)
        {
            var command = new CreateCustomerAddressCommand(request);
            var result = await mediator.Send(command);
            return result;
        }

        [HttpPut("{addressId}")]
        public async Task<ApiResponse> Put(long addressId, [FromBody] CustomerAddressRequest request)
        {
            var command = new UpdateCustomerAddressCommand(addressId, request);
            var result = await mediator.Send(command);
            return result;
        }

        [HttpDelete("{addressId}")]
        public async Task<ApiResponse> Delete(long addressId)
        {
            var command = new DeleteCustomerAddressCommand(addressId);
            var result = await mediator.Send(command);
            return result;
        }
    }
}
