using FluentValidation;
using Immobilier.Host.Requests;

namespace Immobilier.Host.Validators
{
    public class EditPropertyValidator : AbstractValidator<EditPropertyRequest>
    {
        public EditPropertyValidator()
        {
        }
    }
}
