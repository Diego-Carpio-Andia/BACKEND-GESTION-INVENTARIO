using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class RolNuevo
    {
        public class Ejecuta : IRequest
        {
            //este valor no puede ser vacio
            public string Nombre { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            //es parte del identity core, parte del framework
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(RoleManager<IdentityRole> roleManager) { 
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //validamos que el rol no exista previamente
                //validamos por el nombre del rol
                var role = await _roleManager.FindByNameAsync(request.Nombre);
                if(role != null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.BadRequest, new { mesaje = "Ya existe el rol" });
                }

                //le pasamos el nombre en un objeto identity role
                var resultado = await _roleManager.CreateAsync(new IdentityRole(request.Nombre));

                if(resultado.Succeeded) {
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar el rol");

            }
        }
    }
}
