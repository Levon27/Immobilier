using FluentValidation;
using Immobilier.Host.Requests;
using Imobbilier.Core;

namespace Immobilier.Host.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Age).GreaterThanOrEqualTo(18);
            RuleFor(x => x.Name).MinimumLength(2);
            RuleFor(x => x.Email).Must(EmailUtils.IsEmailValid);
        }
    }
}
