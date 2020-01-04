using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIVegetarianoFurio.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EIVegetarianoFurio
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host= CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = host.Services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Fehler beim Initialisieren der Datenbank");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
