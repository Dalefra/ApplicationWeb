using ApplicationWeb.DTOs;
using FluentValidation;

namespace ApplicationWeb.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
        }
    }
}
