using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;


//Command
public record CreateCustomerCommand(CustomerRequest Request) : IRequest<ApiResponse<CustomerResponse>>;
public record UpdateCustomerCommand(long CustomerId, CustomerRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerCommand(long CustomerId) : IRequest<ApiResponse>;
public record ValidateCustomerCommand(CustomerRequest CustomerRequest) : IRequest<ApiResponse>;
//Query
public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomerByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerResponse>>;
public record GetCustomerByParametersQuery(long CustomerId, string Name, string IdentityNumber) : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomersWithIncludesQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomersByNameQuery(string Name) : IRequest<ApiResponse<List<CustomerResponse>>>;