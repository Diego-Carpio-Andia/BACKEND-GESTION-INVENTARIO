using Aplicacion.Informes;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Informes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class InformesController : MiControllerBase
    {
        [HttpPost("InformeCompra")]
        public async Task<ActionResult<List<InformesCompraModel>>> ObtenerInformeCompra(InformeCompra.Ejecutar data) { 
            return await Mediator.Send(data);
        }
        [HttpPost("InformeVenta")]
        public async Task<ActionResult<List<InformesVentaModel>>> ObtenerInformeVenta(InformeVenta.Ejecutar data)
        {
            return await Mediator.Send(data);
        }
        [HttpPost("InformeTendencia")]
        public async Task<ActionResult<List<InformesTendenciaModel>>> ObtenerInformeTendencia(InformeTendencia.Ejecutar data)
        {
            return await Mediator.Send(data);
        }
    }
}


