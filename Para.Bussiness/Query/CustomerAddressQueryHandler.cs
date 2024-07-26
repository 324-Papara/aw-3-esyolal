using MediatR;
using Para.Base.Response;
using Para.Data.UnitOfWork;
using Para.Schema;
using AutoMapper;

public class CustomerAddressQueryHandler :
    IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>,
    IRequestHandler<GetAllCustomerAddressesQuery, ApiResponse<List<CustomerAddressResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerId);
        var mapped = mapper.Map<CustomerAddressResponse>(entity);
        return new ApiResponse<CustomerAddressResponse>(mapped);
    }

    public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAddressesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.CustomerAddressRepository.GetAll();
        var mappedList = mapper.Map<List<CustomerAddressResponse>>(entities);
        return new ApiResponse<List<CustomerAddressResponse>>(mappedList);
    }
}
