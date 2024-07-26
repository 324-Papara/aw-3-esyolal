using MediatR;
using Para.Base.Response;
using Para.Schema;

public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record UpdateCustomerPhoneCommand(long CustomerId, CustomerPhoneRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerPhoneCommand(long CustomerId) : IRequest<ApiResponse>;

public record GetCustomerPhonesByCustomerIdQuery(long CustomerId) : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
public record GetAllCustomerPhonesQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;