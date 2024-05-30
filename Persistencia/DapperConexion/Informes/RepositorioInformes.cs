using Dapper;
using Microsoft.AspNetCore.Mvc.Formatters;
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

        public async Task<IEnumerable<InformesCompraModel>> ObtenerInformesCompraPorCantidad(string usuarioId)
        {
            var storeProcedure = "usp_obtener_cantidad_compra";
            IEnumerable<InformesCompraModel> informesCompraModel = null;
            try
            {
                //abrimos conexion
                var connection = _factoryConnection.GetConnection();
                //Ejecutamos el query
                informesCompraModel = await connection.QueryAsync<InformesCompraModel>(storeProcedure,
                    new { UsuarioId = usuarioId },
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

        public async Task<IEnumerable<InformesTendenciaModel>> ObtenerInformesTendenciaPorCantidad(string usuarioId)
        {
            var storeProcedure = "usp_obtener_cantidad_tendencia";
            IEnumerable<InformesTendenciaModel> informesTendenciaModel = null;
            try
            {
                //Abrimos conexion
                var connection = _factoryConnection.GetConnection();
                //Ejecutamos el query
                informesTendenciaModel = await connection.QueryAsync<InformesTendenciaModel>(storeProcedure,
                    new { UsuarioId = usuarioId },
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

        public async Task<IEnumerable<InformesTotales>> ObtenerInformesTotalesCantidad(string usuarioId)
        {
            var storeProcedure = "usp_obtener_totales";
            IEnumerable<InformesTotales> informesTotales = null;
            try
            {
                //Abrimos conexion
                var connection = _factoryConnection.GetConnection();
                //Ejecutamos el query
                informesTotales = await connection.QueryAsync<InformesTotales>(storeProcedure,
                    new { UsuarioId = usuarioId },
                    commandType: CommandType.StoredProcedure
                    );
                _factoryConnection.CloseConnection();
                return informesTotales;
            }
            catch (Exception ex)
            {
                throw new Exception("no se pudo Obtener el Informe Total", ex);
            }
        }

        public async Task<IEnumerable<InformesVentaModel>> ObtenerInformesVentaPorCantidad(string usuarioId)
        {
            var storeProcedure = "usp_obtener_cantidad_venta";
            IEnumerable<InformesVentaModel> informesVentaModel = null;
            try
            {
                //Abrimos conexion
                var connection = _factoryConnection.GetConnection();
                //Ejecutamos el query
                informesVentaModel = await connection.QueryAsync<InformesVentaModel>(storeProcedure,
                    new { UsuarioId = usuarioId },
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
