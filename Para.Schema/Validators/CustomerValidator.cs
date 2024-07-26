using FluentValidation;
using Para.Data.Context;
using Para.Data.UnitOfWork;
namespace Para.Schema.Validators
{

    public class CustomerValidator : AbstractValidator<CustomerRequest>
    {
        private readonly ParaDbContext _dbContext;


        public CustomerValidator(ParaDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(customer => customer.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(customer => customer.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(customer => customer.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address.").Must(BeUniqueEmail).WithMessage("Email address '{PropertyValue}' is already taken.");

            RuleFor(customer => customer.IdentityNumber)
                .NotEmpty().WithMessage("Identity number is required.")
                .Length(11).WithMessage("Identity number must be 11 characters.").Must(BeUniqueIdentityNumber).WithMessage("Identity number '{PropertyValue}' is already taken.");

            RuleFor(customer => customer.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.");
        }
        private bool BeUniqueEmail(string email)
        {
            return !_dbContext.Customers.Any(c => c.Email == email);
        }

        private bool BeUniqueIdentityNumber(string identityNumber)
        {
            return !_dbContext.Customers.Any(c => c.IdentityNumber == identityNumber);
        }
    }
}
