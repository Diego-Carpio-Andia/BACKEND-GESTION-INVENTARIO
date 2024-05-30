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
        public async Task<ActionResult<List<InformesCompraModel>>> ObtenerInformeCompra() { 
            return await Mediator.Send(new InformeCompra.Ejecutar());
        }
        [HttpPost("InformeVenta")]
        public async Task<ActionResult<List<InformesVentaModel>>> ObtenerInformeVenta()
        {
            return await Mediator.Send(new InformeVenta.Ejecutar());
        }
        [HttpPost("InformeTendencia")]
        public async Task<ActionResult<List<InformesTendenciaModel>>> ObtenerInformeTendencia()
        {
            return await Mediator.Send(new InformeTendencia.Ejecutar());
        }
        [HttpGet("InformeTotal")]
        public async Task<ActionResult<List<InformesTotales>>> ObtenerInformeTotal()
        {
            //SIN PARAMETROS  
            return await Mediator.Send(new InformeTotal.Ejecutar());
        }
    }
}


