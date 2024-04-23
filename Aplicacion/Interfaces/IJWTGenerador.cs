using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces
{
    public interface IJWTGenerador
    {
        string CrearToken(Usuario usuario, List<string> roles);
    }
}
