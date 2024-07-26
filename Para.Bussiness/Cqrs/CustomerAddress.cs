/* CustomerAddress */

//Command
using MediatR;
using Para.Base.Response;
using Para.Schema;

public record CreateCustomerAddressCommand(long CustomerId, CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record UpdateCustomerAddressCommand(long CustomerId, CustomerAddressRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerAddressCommand(long CustomerId) : IRequest<ApiResponse>;
//Query
public record GetCustomerAddressByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record GetAllCustomerAddressesQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
