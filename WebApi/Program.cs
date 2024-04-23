using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Codigo que se usara para ejecutar el codigo de migracion
            var hostServer = CreateHostBuilder(args).Build();
            //creamos un contexto
            using (var ambiente = hostServer.Services.CreateScope())
            {
                var services = ambiente.ServiceProvider;
                //validamos que se ejecute correctamente este procedimiento
                try
                {
                    var context = services.GetRequiredService<CursosOnlineContext>();
                    //ejecutamos la migracion
                    context.Database.Migrate();
                    //context y userManager son necesario para la insercion de data al usuario
                    var userManager = services.GetRequiredService<UserManager<Usuario>>();
                    //.Wait() => usar await que te espere
                    DataPrueba.InsertarData(context, userManager).Wait();

                }
                catch (Exception ex)
                {
                    var logging = services.GetRequiredService<ILogger<Program>>();
                    logging.LogError(ex, "Ocurrio un error en la migracion");
                }

                
            }
            hostServer.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
