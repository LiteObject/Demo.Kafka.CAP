using System;
using DotNetCore.CAP.Dashboard.NodeDiscovery;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Demo.Kafka.CAP
{
    /// <summary>
    /// ORIGINAL SOURCE: https://github.com/dotnetcore/CAP
    /// </summary>
    class Program
    {
        /// <summary>
        /// The services.
        /// </summary>
        private static readonly ServiceProvider Provider;

        /// <summary>
        /// The Program logger.
        /// </summary>
        private static readonly ILogger<Program> Logger;

        /// <summary>
        /// The configuration.
        /// </summary>
        private static IConfiguration configuration;

        static Program()
        {
            var serviceCollection = new ServiceCollection();
            string[] arguments = Environment.GetCommandLineArgs();
            ConfigureServices(serviceCollection);

            Provider = serviceCollection.BuildServiceProvider();
            Logger = Provider.GetRequiredService<ILogger<Program>>();
            Logger.LogInformation($"{nameof(Program)} class has been instantiated.");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(); //Options, If you are using EF as the ORM

            //Note: The injection of services needs before of `services.AddCap()`
            services.AddTransient<ISubscriberService, SubscriberService>();

            services.AddCap(x =>
            {
                // If you are using EF, you need to add the configuration：
                x.UseEntityFramework<AppDbContext>(); //Options, Notice: You don't need to config x.UseSqlServer(""") again! CAP can autodiscovery.

                // If you are using ADO.NET, choose to add configuration you needed：
                x.UseSqlServer("Your ConnectionStrings");
                
                // CAP support RabbitMQ,Kafka,AzureService as the MQ, choose to add configuration you needed：
                x.UseRabbitMQ("ConnectionString");
                x.UseKafka("ConnectionString");

                // Register Dashboard
                x.UseDashboard();

                // Register to Console
                x.UseDiscovery(d =>
                {
                    d.DiscoveryServerHostName = "localhost";
                    d.DiscoveryServerPort = 8500;
                    d.CurrentNodeHostName = "localhost";
                    d.CurrentNodePort = 5800;
                    d.NodeId = "1";
                    d.NodeName = "CAP No.1 Node";
                });
            });
        }
    }
}
