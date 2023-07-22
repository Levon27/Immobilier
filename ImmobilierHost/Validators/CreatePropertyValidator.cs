using FluentValidation;
using Immobilier.Host.Requests;

namespace Immobilier.Host.Validators
{
    public class CreatePropertyValidator : AbstractValidator<CreatePropertyRequest>
    {
        public CreatePropertyValidator()
        {
        }
    }
}
