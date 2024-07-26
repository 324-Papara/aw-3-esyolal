using MediatR;
using Para.Base.Response;
using Para.Schema;

public record CreateCustomerDetailCommand(long CustomerId, CustomerDetailRequest Request) : IRequest<ApiResponse<CustomerDetailResponse>>;
public record UpdateCustomerDetailCommand(long CustomerId, CustomerDetailRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerDetailCommand(long CustomerId) : IRequest<ApiResponse>;

public record GetAllCustomerDetailsQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
public record GetCustomerDetailByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerDetailResponse>>;