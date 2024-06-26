using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistencia;

using MediatR;
using WebApi.Middleware;
using Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using Aplicacion.Interfaces;
using Seguridad.TokenSeguridad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;
using Persistencia.DapperConexion;

using System.Reflection;
using Microsoft.OpenApi.Models;
using Persistencia.DapperConexion.Paginacion;
using Aplicacion.Actividad;
using Persistencia.DapperConexion.Informes;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //Esta funcion tiene acceso al appsettings.json
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            //CORS para que cualquier cliente pueda consumir nuestra api
            services.AddCors(options => options.AddPolicy("CorsApp", builder =>{
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));

            //Vamos a agregar el nuevo servicio para que me abstraiga data de la base de datos
            //opt => tipo de conexion que va atener
            //CursosonlineContext=> representacion de mi base de datos con entity framework
            services.AddDbContext<EntityContext>(opt =>
            {
                //Cadena de conexion el string lo vamos a poner en appsettings.json
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            });
            //para trabajar con dapper se requiere de una cadena de conexion nueva
            services.AddOptions();
            //GetConnectionString => toma toda la seccion 
            //ConexionConfiguracion => propiedad con el mismo nombre de la propiedad  DefaultConnection
            services.Configure<ConexionConfiguracion>(Configuration.GetSection("ConnectionStrings"));


            //añadimos el mediador para los controllers
            //es responsable de registrar los manejadores de MediatR en tu aplicación.
            //Dado que Ejecutar implementa la interfaz IRequest, se puede usar como tipo genérico en MediatR para indicar qué tipo de solicitud se manejará con MediatR. Esto permite que MediatR construya y maneje la solicitud adecuadamente a través de su infraestructura de manejo de solicitudes
            /*
             Cuando agregas services.AddMediatR(typeof(AgregarActividad.Ejecutar).Assembly), 
            estás configurando el contenedor de servicios de ASP.NET Core para registrar 
            todos los manejadores de solicitudes (requests) MediatR que se encuentran 
            en el ensamblado que contiene la clase AgregarActividad.Ejecutar.
            Aquí está cómo funciona:

            typeof(AgregarActividad.Ejecutar).Assembly: typeof(AgregarActividad.Ejecutar) 
            devuelve el tipo de la clase Ejecutar dentro del espacio de nombres AgregarActividad.
            Luego, Assembly obtiene el ensamblado (assembly) al que pertenece ese tipo, es decir,
            el ensamblado que contiene la clase Ejecutar. Esto es importante porque MediatR 
            necesita saber en qué ensamblado buscar los manejadores.
            services.AddMediatR(...): Luego, al llamar a AddMediatR, 
            estás configurando el contenedor de servicios para que MediatR 
            escanee ese ensamblado y registre automáticamente todos los 
            manejadores de solicitudes que encuentre. Esto significa que 
            no necesitas registrar manualmente cada manejador en el contenedor 
            de servicios; MediatR se encarga de eso por ti.
            Entonces, al llamar a services.AddMediatR(typeof(AgregarActividad.Ejecutar).Assembly),
            estás permitiendo que MediatR descubra y registre todos los manejadores de 
            solicitudes definidos en el mismo ensamblado que AgregarActividad.Ejecutar. 
            Esto simplifica mucho la configuración y el mantenimiento, especialmente en 
            aplicaciones grandes donde hay muchos manejadores dispersos en varios ensamblados.
             */

            //Simplemente el tipo de clases que ejecutan : IRequest
            services.AddMediatR(typeof(AgregarActividad.Ejecutar).Assembly);

            
            


            //vamos a agregar fluent validation .AddFluentValidation() ademas de indicar la clase en la cual clase se requiere validar
            services.AddControllers(opt => {
                //para que nuestros controlers tenga la autizacion en cada peticion
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));

            });//.AddFluentValidation( cfg => cfg.RegisterValidatorsFromAssemblyContaining<AgregarCurso>());

            //configuracion del core identity dentro del API para que esta pueda acceder a la base de datos mediante 
            //la instancia de entityFramwork que viene a ser cursosOnlineContext
            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            //agregamos el objeto de tipo rolmanager instanceandolo para asi crear la clase y no nos de error de creacion de clase
            //identidad que represtara los roles por defecto es identityRole
            identityBuilder.AddRoles<IdentityRole>();
            //agregamos claims de la data de los roles a los tokens de seguridad
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();

            //vamos a agregar la clase que instancia del entitydframwork que viene a ser cursosOnlineContext
            identityBuilder.AddEntityFrameworkStores<EntityContext>();
            //configuracion para el Sign para el usuario
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            //lo que falta para el usuario
            services.TryAddSingleton<ISystemClock, SystemClock>();
            //vamos a inyectar a la interface con la clase
            //De esta forma sera posible acceder a los tokens que nos da el proyecto Seguridad desde WEBAPI
            services.AddScoped<IJWTGenerador,JWTGenerador>();

            //creamos las credenciales de acceso osea la palabra secreta que va a decodificar el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta de la web api de este master en .Net core 3.1.201"));


            //Autorizacion para que no me permita consumir los webtokens sin previa autorizacion
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                //verificamos quien nos envia el token ips etc
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    //cualquier tipo de request del cliente debe ser validado por la logica que se ha puesto en el token
                    ValidateIssuerSigningKey = true,
                    //credenciales decodificacion
                    IssuerSigningKey = key,
                    //quien va apoder crear esos tokens, cualquier persona en el mundo lo puede hacer valido para todas las ips
                    ValidateAudience = false,
                    //quien va a poder enviar esos tokens, any people can send the tokens
                    ValidateIssuer = false,

                };
            });

            //agregamos el servicio para la interfaz de IUsuarioSesion, para que funcione la clase
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();
            //inyectamos la interfaz automapper para el correcto funcionanmiento del mapeado de identidades
            //OJO QUE SOLO ES EN CURSO
            // services.AddAutoMapper(typeof(Consulta.Manejador));


            //vamos a arrancar el IFactoryConnection para instancear la conexion a la base de datos 
            services.AddTransient<IFactoryConnection, FactoryConnection>();            
            
            //instanceamos la paginacion
            services.AddScoped<IPaginacion, PaginacionRepository>();
            //Instanceamos las interfaces
            services.AddScoped<IRepositorioInformes, RepositorioInformes>();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "services para el mantenimiento de inventario y ventas",
                    Version = "v1",
                });
                c.CustomSchemaIds(c => c.FullName);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //incluiremos nuestro middelware como un tipo manejador error middelware
            app.UseMiddleware<ManejadorErrorMiddleware>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

            }
            //vamos a indicarle que la app utilizara la autenticacion
            app.UseAuthentication();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsApp");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventario v1");
            });
        }
    }
}
