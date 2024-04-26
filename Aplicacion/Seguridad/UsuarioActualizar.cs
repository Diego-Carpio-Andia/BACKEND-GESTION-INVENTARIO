using Aplicacion.Interfaces;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioActualizar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellidos {  get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            //no se va a cambiar esto pero es necesario que pase el username para cambiar los datos
            public string Username {  get; set; }
            //Data de la imagen
            public ImagenGeneral imagenPerfil { get; set; }
        
        }
        //lo que devolvera es un objeto de tipo UsuarioData
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            //representacion de la persistencia
            private readonly EntityContext _context;
            //representacion del objeto userManager
            private readonly UserManager<Usuario> _userManager;
            //representacion del Token creacion denuevo del token
            private readonly IJWTGenerador _jwtGenerador;
            //objeto para hashear el password
            public readonly IPasswordHasher<Usuario> _passwordHasher;
            public Manejador(EntityContext context, UserManager<Usuario> userManager, IJWTGenerador jwtGenerador, IPasswordHasher<Usuario> passwordHasher)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _passwordHasher = passwordHasher;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //si existe el usuario
                var usuarioIden = await _userManager.FindByNameAsync(request.Username);
                if (usuarioIden == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con este username" });
                }
                //validamos que el email no exista y que sea diferente al usuario que quieres cambiar
                var resultado = await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.Username).AnyAsync();
                if (resultado)
                {
                    throw new ManejadorExepcion(HttpStatusCode.InternalServerError, new { mensaje = "Este email pertenece a otro usuario" });
                }

                if (request.imagenPerfil != null)
                {
                    //buscar por Id usuario
                    var resultadoImagen = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(usuarioIden.Id)).FirstAsync();
                    //si no existe una imagen
                    if (resultadoImagen == null)
                    {
                        var imagen = new Documento
                        {
                            //byte          - convertimos de base64 a byte[]
                            Contenido = Convert.FromBase64String(request.imagenPerfil.Data),
                            Nombre = request.imagenPerfil.Nombre,
                            Extension = request.imagenPerfil.Extension,
                            ObjetoReferencia = new Guid(usuarioIden.Id),
                            DocumentoId = Guid.NewGuid(),
                            FechaCreacion =   DateTime.UtcNow
                        };
                        //agregamos al contexto
                        _context.Documento.Add(imagen);
                    }//si existe una imagen pero quieres actualizarla
                    else
                    {
                        resultadoImagen.Contenido = Convert.FromBase64String(request.imagenPerfil.Data);
                        resultadoImagen.Nombre = request.imagenPerfil.Nombre;
                        resultadoImagen.Extension = request.imagenPerfil.Extension;
                    }

                }
                //AspNetUsers
                usuarioIden.NombreCompleto = request.Nombre + " " + request.Apellidos;
                //password hasheado a la DB 
                usuarioIden.PasswordHash = _passwordHasher.HashPassword(usuarioIden, request.Password);
                usuarioIden.Email = request.Email;
                //Actualizamos el usuario   
                var resultadoUpdate = await _userManager.UpdateAsync(usuarioIden);

                //vamos a obtener la lista de roles del usuario
                var resultadoRoles = await _userManager.GetRolesAsync(usuarioIden);
                var listaRoles = new List<string>(resultadoRoles);

                //devolvemos el perfil - si no le pones el default or async el buscador se queda atascado
                var imagenPerfil = await _context.Documento.Where(x=>x.ObjetoReferencia == new Guid(usuarioIden.Id)).FirstOrDefaultAsync();
                ImagenGeneral general = null;

                //si fue exitoso
                if (resultadoUpdate.Succeeded)
                {
                    if (imagenPerfil != null)
                    {
                        general = new ImagenGeneral
                        {
                            Data = Convert.ToBase64String(imagenPerfil.Contenido),
                            Nombre = imagenPerfil.Nombre,
                            Extension = imagenPerfil.Extension
                        };
                        return new UsuarioData
                        {
                            NombreCompleto = usuarioIden.NombreCompleto,
                            Username = usuarioIden.UserName,
                            Email = usuarioIden.Email,
                            //nuevo TOKEN del nuevo usuario actualizando los datos del token
                            Token = _jwtGenerador.CrearToken(usuarioIden, listaRoles),
                            general = general

                        };
                    }
                    else
                    {
                        return new UsuarioData
                        {
                            NombreCompleto = usuarioIden.NombreCompleto,
                            Username = usuarioIden.UserName,
                            Email = usuarioIden.Email,
                            //nuevo TOKEN del nuevo usuario actualizando los datos del token
                            Token = _jwtGenerador.CrearToken(usuarioIden, listaRoles),

                        };
                    }                    
                }

                throw new Exception("No se pudo actualizar el usuario");
            }

        
        }
    }
}
