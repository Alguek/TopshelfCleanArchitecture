using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts.Validations
{
    public class ReturnListOfProdutsValidator : AbstractValidator<ReturnListOfProdutsCommand>
    {
        public ReturnListOfProdutsValidator()
        {
            RuleFor(s => s.Id).GreaterThan(0);
        }
    }
}
