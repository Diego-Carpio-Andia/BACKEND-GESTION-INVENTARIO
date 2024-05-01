using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Informes
{
    public class RepositorioInformes : IRepositorioInformes
    {
        public readonly IFactoryConnection _factoryConnection;
        public RepositorioInformes(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public async Task<IEnumerable<InformesCompraModel>> ObtenerInformesCompraPorCantidad(int cantidad)
        {
            var storeProcedure = "usp_obtener_cantidad_compra";
            IEnumerable<InformesCompraModel> informesCompraModel = null;
            try
            {
                //abrimos conexion
                var connection = _factoryConnection.GetConnection();
                //Ejecutamos el query
                informesCompraModel = await connection.QueryAsync<InformesCompraModel>(storeProcedure,
                    new { Cantidad = cantidad },
                    commandType: CommandType.StoredProcedure
                );

                _factoryConnection.CloseConnection();
                return informesCompraModel;
            }
            catch (Exception ex)
            {
                throw new Exception("no se pudo Obtener el Informe de Compras", ex);
            }
        }

        public async Task<IEnumerable<InformesTendenciaModel>> ObtenerInformesTendenciaPorCantidad(int cantidad)
        {
            var storeProcedure = "usp_obtener_cantidad_tendencia";
            IEnumerable<InformesTendenciaModel> informesTendenciaModel = null;
            try
            {
                //Abrimos conexion
                var connection = _factoryConnection.GetConnection();
                //Ejecutamos el query
                informesTendenciaModel = await connection.QueryAsync<InformesTendenciaModel>(storeProcedure,
                    new { Cantidad = cantidad},
                    commandType: CommandType.StoredProcedure
                    );
                _factoryConnection.CloseConnection();
                return informesTendenciaModel;
            }
            catch (Exception ex)
            {
                throw new Exception("no se pudo Obtener el Informe de Compras", ex);
            }
        }

        public async Task<IEnumerable<InformesVentaModel>> ObtenerInformesVentaPorCantidad(int cantidad)
        {
            var storeProcedure = "usp_obtener_cantidad_venta";
            IEnumerable<InformesVentaModel> informesVentaModel = null;
            try
            {
                //Abrimos conexion
                var connection = _factoryConnection.GetConnection();
                //Ejecutamos el query
                informesVentaModel = await connection.QueryAsync<InformesVentaModel>(storeProcedure,
                    new { Cantidad = cantidad },
                    commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();
                return informesVentaModel;
            }
            catch (Exception ex)
            {
                throw new Exception("no se pudo Obtener el Informe de Compras", ex);
            }
        }
    }
}
