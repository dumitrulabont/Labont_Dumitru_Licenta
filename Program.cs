using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Labont_Dumitru_Licenta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            //se creaza host-ul dar se ruleaza doar la finalul functiei dupa ce se asigurara existenta rolurilor
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                //obiectul de tip ServiceProvider este necesar pentru crearea unui RoleManager
                var serviceProvider = scope.ServiceProvider;
                //sir cu rolurile folosite
                string[] roles = { "furnizor", "consumator" };
                CreateRoles(serviceProvider,roles).Wait();
            }
            //pornirea host-ului
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
        private static async Task CreateRoles(IServiceProvider serviceProvider, string[] roles)
        {
            //un roleManager poate verifica existenta unui rol si poate creea roluri noi
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            //pentru fiecare rol din sir verifica daca exista, iar daca nu il creeaza
            foreach(string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }
    }
}
