using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Aplicacion.Interfaces;
using System.Linq;
using Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Seguridad
{
    public class Login
    {
       
        //parametros a pasar
        public class Ejecuta : IRequest<UsuarioData>
        {
            //validamos que los campos no sean vacios
            [Required(ErrorMessage = "Por favor ingrese el Email")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Por favor ingrese la contraseña")]
            public string Password { get; set; }

        }
             
        //logica
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            //variable que represente la interfaz del token
            private readonly IJWTGenerador _jwtgenerador;

            private readonly CursosOnlineContext _context;


            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> singInManager, IJWTGenerador jwtgenerador, CursosOnlineContext context) {                       
                _userManager = userManager;
                _signInManager = singInManager;
                _jwtgenerador = jwtgenerador;
                _context = context;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //evaluamos con el login los emails y contraseña
                var usuario_data = await _userManager.FindByEmailAsync(request.Email);
                if(usuario_data == null)
                {
                    //error de autenticacion
                    throw new ManejadorExepcion(HttpStatusCode.Unauthorized);
                }

                //si pasa hacemos la siguiente logica, request.Password => viene desde el cliente
                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario_data, request.Password, false);
                //variable de la lista de roles
                var listaRoles = await _userManager.GetRolesAsync(usuario_data);

                //imagen - si no le pones el default or async el buscador se queda atascado ejecutandose el objeto next
                var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(usuario_data.Id)).FirstOrDefaultAsync();
                
                //si es exitoso que me devuelva la data del usuario
                if (resultado.Succeeded)
                {



                    //evaluamos
                    if (imagenPerfil != null)
                    {
                        var ImagenCliente = new ImagenGeneral
                        {
                            Data = Convert.ToBase64String(imagenPerfil.Contenido),
                            Extension = imagenPerfil.Extension,
                            Nombre = imagenPerfil.Nombre,
                        };

                        return new UsuarioData
                        {
                            NombreCompleto = usuario_data.NombreCompleto,
                            //vamos a reemplazar este token
                            //Token = "Esta sera la data del token",
                            Token = _jwtgenerador.CrearToken(usuario_data, listaRoles.ToList()),
                            Username = usuario_data.UserName,
                            Email = usuario_data.Email,
                            general = ImagenCliente
                        };

                    }
                    else
                    {
                        return new UsuarioData
                        {
                            NombreCompleto = usuario_data.NombreCompleto,
                            //vamos a reemplazar este token
                            //Token = "Esta sera la data del token",
                            Token = _jwtgenerador.CrearToken(usuario_data, listaRoles.ToList()),
                            Username = usuario_data.UserName,
                            Email = usuario_data.Email,
                        };
                    }

                }

                throw new ManejadorExepcion(HttpStatusCode.Unauthorized);

            }
        }
    }
}
