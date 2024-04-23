using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore.Scaffolding;
using static Dapper.SqlMapper;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionRepository : IPaginacion
    {
        private readonly IFactoryConnection _factoryConnection;

        public PaginacionRepository(IFactoryConnection factoryConnection) {
            _factoryConnection = factoryConnection;
        }
        //IDictionary<string, object> parametrosFiltro [{nombre:"diego"},{nombre:"aaron"}]
        public async Task<PaginacionModel> devolverPaginacion(string storeProcedure, int numeroPagina, int cantidadElementos, IDictionary<string, object> parametrosFiltro, string ordenamientoColumna)
        {
            PaginacionModel model = new PaginacionModel();
            List<IDictionary<string, object>> listaReporte = null;
            int totalPaginas = 0;
            int totalRecords = 0;

            try
            {
                var connection = _factoryConnection.GetConnection();
                DynamicParameters dynamicParameters = new DynamicParameters();
                //ingresa a la base de datos - entrada input a la DB
                dynamicParameters.Add("@NumeroPagina", numeroPagina);
                dynamicParameters.Add("@CantidadElementos", cantidadElementos);
                dynamicParameters.Add("@Ordenamiento", ordenamientoColumna);
                //bucle para el parametro filtro este parametro sera es un array,
                //verificaremos si tiene data y luego recorreremos los parametros
                foreach(var param in parametrosFiltro)
                {
                    //clave - valor IDictionary<string (clave), objecto(valor)>
                    dynamicParameters.Add("@" + param.Key, param.Value);
                }



                //Los parametros que salen de la DB
                dynamicParameters.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);
                dynamicParameters.Add("@TotalPaginas", totalPaginas, DbType.Int32, ParameterDirection.Output);




                //por defecto en la db viene un task enumerable
                var result  = await connection.QueryAsync(storeProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
                //result debe de ser de IDictionary
                listaReporte = result.Select(x=> (IDictionary<string, object>)x).ToList();
                model.ListaRecords = listaReporte;
                model.NumeroPaginas = dynamicParameters.Get<int>("@TotalPaginas");
                model.TotalRecords = dynamicParameters.Get<int>("@TotalRecords");


            }
            catch(Exception ex)
            {
                throw new Exception("No se pudo ejecutar el procedimiento almacenado", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return model;
        }
    }
}
