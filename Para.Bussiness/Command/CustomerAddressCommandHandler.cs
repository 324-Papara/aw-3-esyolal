using MediatR;
using Para.Base.Response;
using Para.Data.UnitOfWork;
using Para.Schema;
using AutoMapper;
using Para.Data.Domain;

public class CustomerAddressCommandHandler :
    IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
    IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var detail = mapper.Map<CustomerAddress>(request.Request);
        detail.CustomerId = request.CustomerId; // Ensure CustomerId is set correctly

        var existingDetail = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerId);
        if (existingDetail != null)
        {
            return new ApiResponse<CustomerAddressResponse>(false)
            {
                Message = "Customer details already exist."
            };
        }

        await unitOfWork.CustomerAddressRepository.Insert(detail);
        await unitOfWork.Complete();

        var response = mapper.Map<CustomerAddressResponse>(detail);
        return new ApiResponse<CustomerAddressResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var detail = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerId);
        if (detail == null)
        {
            return new ApiResponse()
            {
                Message = "Customer address not found."
            };
        }

        mapper.Map(request.Request, detail);
        unitOfWork.CustomerAddressRepository.Update(detail);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var detail = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerId);
        if (detail != null)
        {
            await unitOfWork.CustomerAddressRepository.Delete(detail.CustomerId);
            await unitOfWork.Complete();
        }
        return new ApiResponse();
    }
}
