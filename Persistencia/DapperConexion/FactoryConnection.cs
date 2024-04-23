using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistencia.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        //CADA CONEXION SE REQUIERE ABRIR Y CERRAR NO SE PUEDE DEJAR ABIERTO LA CONEXION
        //inyectamos la cadena de conexion a esta clase
        //Para un objeto dentro de otra clase usas un constructor
        private IDbConnection _connection;
        private readonly IOptions<ConexionConfiguracion> _config;
        public FactoryConnection(IOptions<ConexionConfiguracion> config) {
            //se convierte en un objeto
            _config = config;   
        }
        public void CloseConnection()
        {
            if(_connection != null && _connection.State == ConnectionState.Open)
            {
                //CERRAMOS LA CONEXION
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            //evaluamos si esta conexion existe o no
            if(_connection == null)
            {
                //con esto se crea la cadena de conexion
                _connection = new SqlConnection(_config.Value.DefaultConnection);
            }
            //si no esta abierto lo vamos a abrir
            if(_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            //objeto necsario para realizar l.as transacciones
            return _connection;
        }
    }
}
