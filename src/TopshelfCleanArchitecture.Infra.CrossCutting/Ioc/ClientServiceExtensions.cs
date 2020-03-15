using Microsoft.Extensions.DependencyInjection;
using System;
using TopshelfCleanArchitecture.Client.Clients.ClientTest;
using TopshelfCleanArchitecture.Infra.Resiliency;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.IoC
{
    public static class ClientServiceExtensions
    {
        public static void RegisterHttpClient(this IServiceCollection services, string configurationUrl)
        {
            services.AddHttpClient<ClientTest, IClientTest>(configurationUrl);
        }

        public static IHttpClientBuilder AddHttpClient<TClient, TImplementation>(
            this IServiceCollection services,
            string configurationUrl)
            where TClient : class, TImplementation
            where TImplementation : class
        {
            return services.AddHttpClient<TImplementation, TClient>(client =>
            {
                client.BaseAddress = new Uri(configurationUrl);
            })
            .AddPolicyHandler(HttpResiliency.GetRetryPolicy())
            .AddPolicyHandler(HttpResiliency.GetCircuitBreakerPolicy());
        }
    }


}
