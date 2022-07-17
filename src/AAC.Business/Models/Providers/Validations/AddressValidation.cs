using FluentValidation;

namespace AAC.Business.Models.Providers.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(c => c.HouseNumber)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
                .Length(2, 200)
                .WithMessage("The field {PropertyName} needs be between {MinLength} and {MaxLength} caracteres");

            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
                .Length(2, 100)
                .WithMessage("The field {PropertyName} needs be between {MinLength} and {MaxLength} caracteres");

            RuleFor(c => c.AptNumber)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
                .Length(8).WithMessage("The field {PropertyName} needs be {MaxLength} caracteres");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
                .Length(2, 100)
                .WithMessage("The field {PropertyName} needs be between {MinLength} and {MaxLength} caracteres");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
                .Length(2, 50)
                .WithMessage("The field {PropertyName} needs be between {MinLength} and {MaxLength} caracteres");

            RuleFor(c => c.PostalCode)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
                .Length(2, 50)
                .WithMessage("The field {PropertyName} needs be between {MinLength} and {MaxLength} caracteres");
        }
    }
}
