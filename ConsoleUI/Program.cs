
using System.Net;
using System.Security.Cryptography.X509Certificates;
using ConsoleUI.Context;
using ConsoleUI.Data.Handlers;
using ConsoleUI.Data.Services;
using ConsoleUI.Handlers;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleUI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = BuildHost();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DataSeed.Initialize(services);
            }

            var handler = host.Services.GetRequiredService<IAppHandler>();
            handler.Run();

           
        }

        private static IHost BuildHost()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services = ConfigureServices(services);
                })
                .Build();
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LeaveApplicationContext>(x =>
                x.UseSqlServer(
                    "Data Source=.\\SQLEXPRESS;Initial Catalog=leave-application-db;Integrated Security=True"));

            services.AddTransient<IAppHandler, AppHandler>();
            services.AddTransient<ILeaveApplicationServices, LeaveApplicationServices>();

            return services;
        }
    }
}

