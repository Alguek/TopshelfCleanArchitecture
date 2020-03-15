using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;

namespace TopshelfCleanArchitecture.Infra.Resiliency
{
    public static class HttpResiliency
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (a, b, c, d) =>
                {
                    Console.WriteLine("teste");
                });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));
        }
    }
}