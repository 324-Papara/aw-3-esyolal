using FluentValidation;

namespace Para.Schema.Validators
{
    public class CustomerDetailValidator : AbstractValidator<CustomerDetailRequest>
    {
        public CustomerDetailValidator()
        {
            RuleFor(x => x.FatherName)
                .NotEmpty()
                .WithMessage("Father's name is required")
                .MaximumLength(15)
                .WithMessage("Father name can be up to 15 characters long!");

            RuleFor(x => x.MotherName)
                .NotEmpty()
                .WithMessage("Mother's name is required")
                .MaximumLength(15)
                .WithMessage("Mother name can be up to 15 characters long!");

            RuleFor(x => x.MontlyIncome)
                .NotEmpty()
                .WithMessage("Monthly income is required")
                .MaximumLength(20)
                .WithMessage("Monthly income must be maximum 20 characters long!");
                
            RuleFor(x => x.Occupation)
                .NotEmpty()
                .WithMessage("Occupation is required")
                .MaximumLength(30)
                .WithMessage("Occupation must be maximum 30 characters long!");
        }
    }
}
