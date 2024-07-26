using MediatR;
using Para.Base.Response;
using Para.Data.UnitOfWork;
using Para.Schema;
using AutoMapper;

public class CustomerDetailQueryHandler :
    IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponse>>,
    IRequestHandler<GetAllCustomerDetailsQuery, ApiResponse<List<CustomerDetailResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var detail = await unitOfWork.CustomerDetailRepository.GetById(request.CustomerId);
        var mapped = mapper.Map<CustomerDetailResponse>(detail);
        return new ApiResponse<CustomerDetailResponse>(mapped);
    }

    public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetAllCustomerDetailsQuery request, CancellationToken cancellationToken)
    {
        var details = await unitOfWork.CustomerDetailRepository.GetAll();
        var mappedList = mapper.Map<List<CustomerDetailResponse>>(details);
        return new ApiResponse<List<CustomerDetailResponse>>(mappedList);
    }
}
