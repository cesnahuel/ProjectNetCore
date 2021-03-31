using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Domain;

namespace UserApi.Validation
{
    public class AuthValidator : AbstractValidator<AuthDto> 
    {
        public AuthValidator()
        {
            RuleFor(model => model.username)
                .NotEmpty().WithMessage("{PropertyName} no debe ser vacio.")
                .Length(0, 10).WithMessage("{PropertyName} no debe exceder los 10 caracteres.");
            RuleFor(model => model.password)
                .NotEmpty().WithMessage("{PropertyName} no debe ser vacio.");
        }
    }
}
