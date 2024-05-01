using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.DapperConexion
{
    public class ConexionConfiguracion
    {
        //contiene la cadena de conexion y viene desde web api en
        //services.Configure<ConexionConfiguracion>(Configuration.GetSection("GetConnectionString"));
        //A la vez que viene desde el json de configuraciones 
        public string DefaultConnection { get; set; }
    }
}
