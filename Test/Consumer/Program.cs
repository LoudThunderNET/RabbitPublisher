// See https://aka.ms/new-console-template for more information
using Consumer;
using MassTransit;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder()
    .ConfigureServices((ctx, services) =>
    {
        services.AddMassTransit(busCfg =>
        {
            busCfg.AddConsumer<SampleMessageConsumer>();
            busCfg.UsingRabbitMq((ctx, rabbitMqBusCfg) =>
            {
                rabbitMqBusCfg.Host("localhost", "/", cfg =>
                {
                    cfg.Password("guest");
                    cfg.Username("guest");
                });
                rabbitMqBusCfg.ConfigureEndpoints(ctx);
            });

        });
    }).Build().Run();
