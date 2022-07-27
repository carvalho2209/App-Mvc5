using FluentValidation;

namespace AAC.Business.Models.Providers.Validations
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("The field {PropertyName} needs to be provided.")
                .Length(2, 200)
                .WithMessage("The field {PropertyName} needs have between {MinLength} and {MaxLength} caracteres");

            When(p => p.TypeProvider == TypeProvider.LegalPerson, () =>
            {
                //RuleFor(p => p.Document.Length).Equal(ValidationDocs.CnpjValidacao.TamanhoCnpj)
                //    .WithMessage(" The field needs to have {ComparisonValue} caracteres and was informed {PropertyValue}");
                
                RuleFor(d => d.Document)
                    .NotEmpty()
                    .WithMessage("The field {PropertyName} needs to be provided.")
                    .Length(9)
                    .WithMessage("The field {PropertyName} needs have {MinLength} caracteres");
                
                //RuleFor(p => ValidationDocs.CnpjValidacao.Validar(p.Document)).Equal(true)
                //    .WithMessage(" The document is invalid.");
            });

            When(p => p.TypeProvider == TypeProvider.PhysicalPerson, () =>
            {
                RuleFor(d => d.Document)
                    .NotEmpty()
                    .WithMessage("The field {PropertyName} needs to be provided.")
                    .Length(9)
                    .WithMessage("The field {PropertyName} needs have {MinLength} caracteres");

                //RuleFor(p => p.Document.Length).Equal(ValidationDocs.CpfValidacao.TamanhoCpf)
                //    .WithMessage(
                //        " The field needs to have {ComparisonValue} caracteres and was informed {PropertyValue}");

                //RuleFor(p => ValidationDocs.CpfValidacao.Validar(p.Document)).Equal(true)
                //    .WithMessage(" The document is invalid.");
            });
        }
    }
}