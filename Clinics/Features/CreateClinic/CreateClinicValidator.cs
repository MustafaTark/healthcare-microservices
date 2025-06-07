using FluentValidation;

namespace Clinics.Features.CreateClinic
{
    public class CreateClinicValidator : AbstractValidator<CreateClinicCommand>
    {
        public CreateClinicValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be in E.164 format");
        }
    }
}