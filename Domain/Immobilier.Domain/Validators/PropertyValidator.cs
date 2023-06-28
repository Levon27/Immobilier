using FluentValidation;

namespace Immobilier.Domain.Validators
{
    public class PropertyValidator : AbstractValidator<Property>
    {
        public PropertyValidator() 
        { 
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Address).NotEmpty();
        }
    }
}
