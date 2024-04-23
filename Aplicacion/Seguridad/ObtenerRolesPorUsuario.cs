using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class ObtenerRolesPorUsuario
    {
        //Quiero que me devuelva una lista de strings
        public class Ejecuta : IRequest<List<string>>
        {
            public string Username {  get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, List<string>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly UserManager<Usuario> _userManager;

            public Manejador(RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager)
            {
                _roleManager = roleManager;
                _userManager = userManager;
            }
            public async Task<List<string>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                
                var usuarioIden = await _userManager.FindByNameAsync(request.Username);
                if (usuarioIden == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "El usuario no existe" });
                }
                var resultados = await _userManager.GetRolesAsync(usuarioIden);
                //convertimos a una lista de strings
                return new List<string>(resultados);
            }
        }
    }
}
