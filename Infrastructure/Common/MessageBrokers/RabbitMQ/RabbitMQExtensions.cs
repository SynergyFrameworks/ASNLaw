using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Common.MessageBrokers.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services,  IConfiguration Configuration, Action<IRegistrationConfigurator> options = null)
        {
              
            RabbitMQOptions brokerOptions = new RabbitMQOptions();
            RabbitMQBusOptions brokerLocationOptions = new RabbitMQBusOptions();
            Configuration.GetSection(nameof(MessageBrokersOptions)).Bind(options);
                services.Configure<RabbitMQOptions>(Configuration.GetSection(nameof(MessageBrokersOptions)));

            services.AddMassTransit(x =>
            {
                if (options != null) options(x);

                x.UsingRabbitMq((context, cfg) =>
                {
                    string host = brokerOptions.Host ?? "rabbitmq";
                    ushort port = brokerOptions.Port ?? 5672;
                    string virtualHost = brokerOptions.VirtualHost ?? @"/";

                    cfg.Host(host, port, virtualHost, (hc) =>
                    {
                        hc.Username(brokerOptions.User ?? "user");
                        hc.Password(brokerOptions.Password ?? "password");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            // services.AddMassTransitHostedService(true);
         



            services.AddSingleton<IEventListener, RabbitMQListener>();

            return services;
        }
    }
}
