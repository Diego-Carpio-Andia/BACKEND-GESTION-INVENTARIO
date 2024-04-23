using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Seguridad.TokenSeguridad
{
    public class UsuarioSesion : IUsuarioSesion
    {
        //Para tener acceso al usuario que esta en sesion aplicaremos un interface inyectandolo en el constructor
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public UsuarioSesion(IHttpContextAccessor httpcontextAccesor)
        {
            _httpcontextAccessor = httpcontextAccesor;
        }
        public string ObtenerUsuarioSesion()
        {
            //la data que se almacena en coreidentity User se llama claims, BUSCAMOS POR USERNAME
            var userName = _httpcontextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userName;
            //luego vamos a inyectar dentro de la clase starup y funcione 
        }
    }
}
