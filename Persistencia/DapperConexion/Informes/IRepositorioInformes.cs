using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Informes
{
    public interface IRepositorioInformes
    {
        Task<IEnumerable<InformesCompraModel>> ObtenerInformesCompraPorCantidad(int cantidad);
        Task<IEnumerable<InformesVentaModel>> ObtenerInformesVentaPorCantidad(int cantidad);
        Task<IEnumerable<InformesTendenciaModel>> ObtenerInformesTendenciaPorCantidad(int cantidad);
    }
}
