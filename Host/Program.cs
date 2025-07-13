using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitPublisher.Logic.Models.Cfg;
using System.Diagnostics;

namespace RabbitPublished
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((ctx, services) =>
                {
                    services
                        .Configure<List<RabbitHost>>(o => ctx.Configuration.GetSection(RabbitHost.Section).Bind(o))
                        .Configure<List<Contract>>(o => ctx.Configuration.GetSection(Contract.Section).Bind(o))
                        .AddMassTransit(regCfg =>
                        {
                            regCfg.UsingRabbitMq((context, cfg) =>
                            {
                                var hosts = context.GetRequiredService<IOptions<List<RabbitHost>>>().Value;
                                foreach (var host in hosts)
                                {
                                    cfg.Host(host.Url, host.VHost, hostCfg =>
                                    {
                                        hostCfg.Password(host.Password);
                                        hostCfg.Username(host.Login);
                                        hostCfg.ConnectionName(host.Name);
                                    });
                                }
                            });
                        })
                        .AddTransient<MainForm>();

                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                ApplicationConfiguration.Initialize();
                Application.ThreadException += (s, e) =>
                {
                    Debug.WriteLine(e.Exception.Message);
                };

                Application.Run(scope.ServiceProvider.GetRequiredService<MainForm>());
            }
        }
    }
}