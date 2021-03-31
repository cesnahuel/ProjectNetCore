using CatalogApi.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Validation
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator() 
        {
            RuleFor(model => model.ShortDescription)
                .NotEmpty().WithMessage("{PropertyName} no debe ser vacio.")
                .Length(0, 100).WithMessage("{PropertyName} no debe exceder los 100 caracteres.");
            RuleFor(model => model.UnitPrice)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor a cero");
            RuleFor(model => model.UnitStock)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor a cero");
            RuleFor(model => model.CategoryId)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor a cero");
        }
        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }

        private bool IsValidNumber(string name)
        {
            return name.All(Char.IsNumber);
        }
    }
}
