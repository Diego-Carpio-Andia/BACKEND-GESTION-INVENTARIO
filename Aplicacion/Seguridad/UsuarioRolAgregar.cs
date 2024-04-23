using Aplicacion.ManejadorError;
using Dominio;
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
    public class UsuarioRolAgregar
    {
        public class Ejecuta : IRequest
        {
            //usuario en la operacion
            public string Username { get; set; }
            //Rol del usuario
            public string RolNombre {  get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolNombre);
                if(role == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "El rol no existe" });
                }
                var usuarioIden = await _userManager.FindByNameAsync(request.Username);
                if (usuarioIden == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "El usuario no existe" });
                }
                //agregamos el rol al usuario usuario - nombre del rol
                var resultado = await _userManager.AddToRoleAsync(usuarioIden, request.RolNombre);
                if (resultado.Succeeded)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo agregar el rol al usuario");
            }
        }
    }
}
