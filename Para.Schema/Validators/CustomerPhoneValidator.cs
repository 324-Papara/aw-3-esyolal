using FluentValidation;

namespace Para.Schema.Validators
{
    public class CustomerPhoneValidator : AbstractValidator<CustomerPhoneRequest>
    {
        public CustomerPhoneValidator()
        {
            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required")
                .MaximumLength(3)
                .WithMessage("Country code must be up to 3 characters long");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .MaximumLength(10)
                .WithMessage("Phone number must be up to 10 characters long");
        }
    }
}
