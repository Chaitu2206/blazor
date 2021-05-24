using FluentValidation;
using Zindagi.Domain.RequestsAggregate.Commands;

namespace Zindagi.Domain.RequestsAggregate.Validators
{
    public class CreateBloodRequestValidator : AbstractValidator<CreateBloodRequest>
    {
        public CreateBloodRequestValidator()
        {
            RuleFor(prop => prop.PatientName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(prop => prop.Reason)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(prop => prop.DonationType)
                .NotEmpty()
                .WithName("Blood Donation Type");

            RuleFor(prop => prop.BloodGroup)
                .NotEmpty();

            RuleFor(prop => prop.Priority)
                .NotEmpty();

            RuleFor(prop => prop.QuantityInUnits)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);
        }
    }
}
