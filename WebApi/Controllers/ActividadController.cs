using Aplicacion.Actividad;
using Aplicacion.Venta;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Paginacion;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class ActividadController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(AgregarActividad.Ejecutar data)
        {
            return await Mediator.Send(data);
        }
        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionActividad.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}
