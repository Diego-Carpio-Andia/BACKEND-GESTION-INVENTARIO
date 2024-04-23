using Aplicacion.ManejadorError;
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
    public class RolEliminar
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.Nombre);
                if(role == null)
                {
                    //no existe el rol
                    throw new ManejadorExepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el rol"});
                }
                var resultado = await _roleManager.DeleteAsync(role);
                if(resultado != null)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar el rol");
            }
        }
    }
}
