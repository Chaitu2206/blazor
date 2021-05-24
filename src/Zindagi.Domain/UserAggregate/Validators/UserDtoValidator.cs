using FluentValidation;
using Zindagi.Domain.UserAggregate.ViewModels;

namespace Zindagi.Domain.UserAggregate.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.LastName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.AlternateId).NotEmpty();
        }
    }
}
