using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema.Validators;
using Para.Schema;
using Para.Data.Context;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;


        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerResponse>>> Get()
        {
            var operation = new GetAllCustomerQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerResponse>> Get([FromRoute] long customerId)
        {
            var operation = new GetCustomerByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest value)
        {

            var validationOperation = new ValidateCustomerCommand(value);
            var validationResult = await mediator.Send(validationOperation);

            if (!validationResult.IsSuccess)
            {
                return new ApiResponse<CustomerResponse>(false)
                {
                    Message = validationResult.Message
                };
            }

            var operation = new CreateCustomerCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerRequest value)
        {
            var operation = new UpdateCustomerCommand(customerId, value);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerCommand(customerId);
            var result = await mediator.Send(operation);
            return result;
        }
        //Include kullanarak modelleri birbirine ekledik.
        [HttpGet("with-addresses-and-phones")]
        public async Task<ApiResponse<List<CustomerResponse>>> Include()
        {
            var result = await mediator.Send(new GetCustomersWithIncludesQuery());
            return result;
        }
        //Where kullanım örneği
        [HttpGet("where-name")]
        public async Task<ApiResponse<List<CustomerResponse>>> Where(string name)
        {
            var result = await mediator.Send(new GetCustomersByNameQuery(name));
            return result;
        }
    }
}