using AutoMapper;
using FluentValidation;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

public class CustomerCommandHandler :
    IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
    IRequestHandler<UpdateCustomerCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerCommand, ApiResponse>,
    IRequestHandler<ValidateCustomerCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IValidator<CustomerRequest> validator;

    public CustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CustomerRequest> validator)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.validator = validator;
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var mapped = mapper.Map<CustomerRequest, Customer>(request.Request);
            mapped.CustomerNumber = new Random().Next(1000000, 9999999);
            await unitOfWork.CustomerRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerResponse>(mapped);
            return new ApiResponse<CustomerResponse>(response);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CustomerResponse>(false)
            {
                Message = ex.Message
            };
        }
    }

    public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        
        var existingCustomer = await unitOfWork.CustomerRepository.GetById(request.CustomerId);
        if (existingCustomer == null)
        {
            return new ApiResponse { Message = "Customer not found." };
        }
        mapper.Map(request.Request, existingCustomer);

        existingCustomer.InsertUser = existingCustomer.InsertUser ?? "Server";
        existingCustomer.InsertDate = existingCustomer.InsertDate == default ? DateTime.UtcNow : existingCustomer.InsertDate;
        existingCustomer.IsActive = existingCustomer.IsActive;

        unitOfWork.CustomerRepository.Update(existingCustomer);
        await unitOfWork.Complete();

        return new ApiResponse();
    }



    public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.CustomerRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            return new ApiResponse()
            {
                Message = ex.Message
            };
        }
    }

    public async Task<ApiResponse> Handle(ValidateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request.CustomerRequest, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiResponse()
                {
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                };
                return errorResponse;
            }

            return new ApiResponse();
        }
        catch (Exception ex)
        {
            return new ApiResponse("false")
            {
                Message = ex.Message
            };
        }
    }
}
