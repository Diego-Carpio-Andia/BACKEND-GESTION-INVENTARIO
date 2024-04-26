using Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class DataPrueba
    {
        //insertamos data a la base de datos con la instancia del entityFramework con un userManager del tipo Usuario
        //WEb api inserta el nuevo usuario que quiero
        public static async Task InsertarData(EntityContext context, UserManager<Usuario> usuarioManger)
        {
            //insertara si es que no existe ningun usuario en la base de datos
            //validacion de que exista un usuario en la base de datos, en este caso el if menciona si no existen
            if (!usuarioManger.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "DiegoC", UserName = "lucky", Email = "diegobefore@hotmail.com" };
                //creamos el usuario
                await usuarioManger.CreateAsync(usuario, "Password123$");
            }
        }
    }
}
