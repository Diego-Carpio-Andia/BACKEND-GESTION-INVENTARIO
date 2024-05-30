using Aplicacion.Seguridad;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    //Para que todos puedan acceder al controller, ya que se implemento que todos los controllers tengan la cabecera de autenticacion
    [AllowAnonymous]
    public class UsuarioController : MiControllerBase
    {
        

        // http://localhost:5000/api/Usuario/login        
        [HttpPost("login")]
        //este metodo debe de recibir parrametros importando la clase Login
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros)
        {
            //invoca a la logica del manejador de login.manejador
            return await Mediator.Send(parametros);
        }
        // http://localhost:5000/api/Usuario/registrar 
        [HttpPost("registrar")]
        //este metodo debe de recibir parrametros importando la clase Login
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecutar parametros)
        {
            //invoca a la logica del manejador de login.manejador
            return await Mediator.Send(parametros);
        }
        [HttpGet]
        public async Task<ActionResult<UsuarioData>> DevolverUsuario()
        {
            //requieres de pasarle el token de autenticacion para que te devuelva el usuario actual
            //en la cabeza de la peticion
            return await Mediator.Send(new UsuarioActual.Ejecutar());
        }
        [HttpPut]
        public async Task<ActionResult<UsuarioData>> Actualizar(UsuarioActualizar.Ejecuta parametros) {
            return await Mediator.Send(parametros);
        }


    }
}
