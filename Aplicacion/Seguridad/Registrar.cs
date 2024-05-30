using Aplicacion.Interfaces;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecutar : IRequest<UsuarioData>
        {
            [Required(ErrorMessage = "Por favor ingrese el nombre")]
            public string Nombre { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el Apellido")]
            public string Apellidos { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el Email")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el Email")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el Username")]
            public string Username { get; set; }
                  
        }
                
        //Aparte del ejecutar, va a devolver un tipo UsuarioDATA
        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            private readonly EntityContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJWTGenerador _jwtGenerador;

            //se requiere registra la data del usuario, seguridad y token
            public Manejador(EntityContext context, UserManager<Usuario> userManager, IJWTGenerador jwtGenerador) {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;

            }
            //devuelve al usuariData este task
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                //LOGICA
                //verificamos si el mail no existe en la base de datos
                //tabla aspNetUsers pero a nivel identidad se llama Users
                //LENGUAJE LINQ x => registros
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync(); //devuelve un valor boolea
                var existe_username = await _context.Users.Where(x=>x.UserName == request.Username).AnyAsync(); 
                if(existe || existe_username)
                {
                    throw new ManejadorExepcion(HttpStatusCode.BadRequest, new {mensaje = "El email o username ingresado ya existe"});
                }

                var usuario = new Usuario
                {
                    NombreCompleto = request.Nombre.ToString() + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.Username,    
                };

                
                //metodo para insertar este valor el usuario y el password
                var resultado = await _userManager.CreateAsync(usuario, request.Password);
                if (resultado.Succeeded)
                {
                    return new UsuarioData { 
                        NombreCompleto = usuario.NombreCompleto,
                        //usuario que recien se crea por ende no tiene roles
                        Token = _jwtGenerador.CrearToken(usuario, null),
                        Email = usuario.Email,
                        Username = usuario.UserName                        
                    };

                }

                throw new Exception("No se pudo agregar al nuevo usuario");
            }
        }

    }
}
