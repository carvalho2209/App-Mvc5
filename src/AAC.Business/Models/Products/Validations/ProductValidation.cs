using FluentValidation;

namespace AAC.Business.Models.Products.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
                .Length(2, 200)
                .WithMessage("The field {PropertyName} needs to be between {MinLength} and {MaxLength} caracteres");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
                .Length(2, 1000)
                .WithMessage("The field {PropertyName} needs to be between {MinLength} and {MaxLength} caracteres");

            RuleFor(c => c.Amount)
                .GreaterThan(0)
                .WithMessage("The field {PropertyName} needs to be bigger than {ComparisonValue}");
        }
    }
}