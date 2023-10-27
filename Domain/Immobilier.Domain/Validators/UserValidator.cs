using FluentValidation;
using Imobbilier.Core;

namespace Immobilier.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(x => x.Name).MinimumLength(2);
            RuleFor(x => x.Email).Must(EmailUtils.IsEmailValid);
        }
    }
}
