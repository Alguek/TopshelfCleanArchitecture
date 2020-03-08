﻿using MediatR;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.MediatR
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.Information($"Handling {typeof(TRequest).Name}");
            var response = await next();
            _logger.Information($"Handled {typeof(TResponse).Name}");
            return response;
        }
    }
}
