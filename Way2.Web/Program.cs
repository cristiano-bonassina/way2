using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Way2.Presentation.WebApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseLamar()
                .UseSerilog((context, configuration) =>
                {

                    if (context.HostingEnvironment.IsProduction())
                    {
                        configuration
                            .MinimumLevel.Error()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                            .MinimumLevel.Override("System", LogEventLevel.Error)
                            .WriteTo.Console()
                            .WriteTo.File("./app.log");
                    }
                    else
                    {
                        configuration
                            .MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                            .MinimumLevel.Override("System", LogEventLevel.Debug)
                            .WriteTo.Console();
                    }

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
