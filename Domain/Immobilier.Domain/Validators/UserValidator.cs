using FluentValidation;
using Imobbilier.Core;

namespace Immobilier.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(x => x.Age).GreaterThanOrEqualTo(18);
            RuleFor(x => x.Name).MinimumLength(7);
            RuleFor(x => x.Email).Must(EmailUtils.IsEmailValid);
        }
    }
}
