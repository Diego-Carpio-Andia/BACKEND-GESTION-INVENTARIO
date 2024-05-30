using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Informes
{
    public interface IRepositorioInformes
    {
        Task<IEnumerable<InformesCompraModel>> ObtenerInformesCompraPorCantidad(string usuarioId);
        Task<IEnumerable<InformesVentaModel>> ObtenerInformesVentaPorCantidad(string usuarioId);
        Task<IEnumerable<InformesTendenciaModel>> ObtenerInformesTendenciaPorCantidad(string usuarioId);
        Task<IEnumerable<InformesTotales>> ObtenerInformesTotalesCantidad(string usuarioId);
    }
}
