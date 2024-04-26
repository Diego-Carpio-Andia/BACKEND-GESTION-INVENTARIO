using Aplicacion.Interfaces;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {
        //clase para devolver datos al usuario actual
        public class Ejecutar : IRequest<UsuarioData> { }
        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            //creamos las variables privadas readonly
            UserManager<Usuario> _userManager;
            IJWTGenerador _jwtGenerador;
            IUsuarioSesion _usuarioSesion;
            private readonly EntityContext _cursosOnlineContext;

            //IusuarioSesion => devuelve el username
            public Manejador(UserManager<Usuario> userManager, IJWTGenerador jwtGenerador, IUsuarioSesion usuarioSesion, EntityContext context) { 
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
                _userManager = userManager;
                _cursosOnlineContext = context;
            }
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                //buscamos un usuario en la base de datos con ese username
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion()) ?? throw new Exception("El usuario no se encontró en la base de datos.");

                //variable de la lista de roles
                var listaRoles = await _userManager.GetRolesAsync(usuario);

                //Obtenemos la imagen tipo byte array de bites - si no le pones el default or async el buscador se queda atascado ejecutandose el objeto next
                var imagenPerfil = await _cursosOnlineContext.Documento.Where(x => x.ObjetoReferencia == new Guid(usuario.Id)).FirstOrDefaultAsync();

                if(imagenPerfil != null)
                {
                    var imagenCliente = new ImagenGeneral
                    {
                        Data = Convert.ToBase64String(imagenPerfil.Contenido),
                        Extension = imagenPerfil.Extension,
                        Nombre = imagenPerfil.Nombre,
                    };

                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Username = usuario.UserName,
                        Token = _jwtGenerador.CrearToken(usuario, listaRoles.ToList()),
                        Email = usuario.Email,
                        general = imagenCliente,

                    };
                }
                else
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Username = usuario.UserName,
                        Token = _jwtGenerador.CrearToken(usuario, listaRoles.ToList()),
                        Email = usuario.Email,
                    };
                }

                
            }
        }



    }
}
