using Aplicacion.Venta;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Paginacion;
using System.Threading.Tasks;
using System;
using Aplicacion.PronosticoDemanda;

namespace WebApi.Controllers
{
    public class PronosticoDemandaController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(AgregarPronosticoDemanda.Ejecutar data)
        {
            return await Mediator.Send(data);
        }
        
        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionPronosticoDemanda.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}
