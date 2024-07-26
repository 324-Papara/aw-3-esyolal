/* CustomerAddress */

//Command
using MediatR;
using Para.Base.Response;
using Para.Schema;

public record CreateCustomerAddressCommand(CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record UpdateCustomerAddressCommand(long AddressId, CustomerAddressRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerAddressCommand(long AddressId) : IRequest<ApiResponse>;
//Query
public record GetCustomerAddressByIdQuery(long AddressId) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record GetAllCustomerAddressesQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
