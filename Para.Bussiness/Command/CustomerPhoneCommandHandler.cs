using MediatR;
using Para.Base.Response;
using Para.Data.UnitOfWork;
using Para.Schema;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Para.Data.Domain;

public class CustomerPhoneCommandHandler :
    IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
    IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        var phone = mapper.Map<CustomerPhone>(request.Request);
        await unitOfWork.CustomerPhoneRepository.Insert(phone);
        await unitOfWork.Complete();

        var response = mapper.Map<CustomerPhoneResponse>(phone);
        return new ApiResponse<CustomerPhoneResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        var phone = mapper.Map<CustomerPhone>(request.Request);
        phone.CustomerId = request.CustomerId;
        unitOfWork.CustomerPhoneRepository.Update(phone);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        var phone = await unitOfWork.CustomerPhoneRepository.GetById(request.CustomerId);
        if (phone != null)
        {
            await unitOfWork.CustomerPhoneRepository.Delete(phone.Id);
            await unitOfWork.Complete();
        }
        return new ApiResponse();
    }
}
