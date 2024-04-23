using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistencia.DapperConexion
{
    public interface IFactoryConnection
    {
        //metodo para cerrar conexiones existentes
        void CloseConnection();
        //otro que te devuelve el objeto de direccion
        IDbConnection GetConnection();
    }
}
