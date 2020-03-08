using FluentValidation;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts.Validations
{
    public class ReturnListOfProdutsValidator : AbstractValidator<ReturnListOfProdutsRequest>
    {
        public ReturnListOfProdutsValidator()
        {
            RuleFor(s => s.Id).GreaterThan(0);
        }
    }
}