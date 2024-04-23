using Aplicacion.Interfaces;
using Dominio;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Seguridad.TokenSeguridad
{
    public class JWTGenerador : IJWTGenerador
    {
        public string CrearToken(Usuario usuario, List<string> roles)
        {
            var claims = new List<Claim>
            {
                //el claim name id enviara al userName
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };

            //si no es nulo los roles
            if(roles != null)
            {
                foreach(var role in roles)
                {
                    //agregamos los claims de todos los roles
                    claims.Add(new Claim(ClaimTypes.Role, role));
                } 
            }

            //creamos las credenciales de acceso osea la palabra secreta que va a decodificar el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta de la web api de este master en .Net core 3.1.201"));
            //encriptacion
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            //descripcion de token
            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //vida del token
                Expires = DateTime.Now.AddDays(30),
                //tipo de acceso sha 512 que se puso en credenciales
                SigningCredentials = credenciales,
            };

            //crear el token
            var TokenManejador = new JwtSecurityTokenHandler();
            //se basa en tokenDescripcion para ser creado 
            var token = TokenManejador.CreateToken(tokenDescripcion);
            //devolvemos un string
            return TokenManejador.WriteToken(token);    
        }
    }
}
