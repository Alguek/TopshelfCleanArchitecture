using Autofac;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.UseCase.Base;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.IoC
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TResponse : ResponseBase
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidatorBehavior(IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .Select(s => s.ErrorMessage)
                .ToList();

            if (failures.Any())
            {
                var responseType = typeof(TResponse);
                var resultType = responseType.GetGenericArguments()[0];
                var genericTypeClass = typeof(ResponseBase<>).MakeGenericType(resultType);
                var errors = Activator.CreateInstance(genericTypeClass, null, failures) as TResponse;
                return errors;
            }

            var response = await next();
            return response;
        }
    }
}