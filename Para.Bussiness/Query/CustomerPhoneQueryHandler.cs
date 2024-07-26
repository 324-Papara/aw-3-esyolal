using MediatR;
using Para.Base.Response;
using Para.Data.UnitOfWork;
using Para.Schema;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class CustomerPhoneQueryHandler :
    IRequestHandler<GetCustomerPhonesByCustomerIdQuery, ApiResponse<List<CustomerPhoneResponse>>>,
    IRequestHandler<GetAllCustomerPhonesQuery, ApiResponse<List<CustomerPhoneResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerPhoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetCustomerPhonesByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var phones = await unitOfWork.CustomerPhoneRepository.GetById(request.CustomerId);
        var mappedList = mapper.Map<List<CustomerPhoneResponse>>(phones);
        return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
    }

    public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhonesQuery request, CancellationToken cancellationToken)
    {
        var phones = await unitOfWork.CustomerPhoneRepository.GetAll();
        var mappedList = mapper.Map<List<CustomerPhoneResponse>>(phones);
        return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
    }
}
