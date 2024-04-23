using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ManejadorError
{
    //AGREGAREMOS UN MIDDLEWARE
    public class ManejadorExepcion : Exception
    {
        public HttpStatusCode Codigo { get; }
        public object Error { get; }
        public ManejadorExepcion(HttpStatusCode codigo, object error = null) { 
            Codigo = codigo;
            Error = error;
        }
    }
}
