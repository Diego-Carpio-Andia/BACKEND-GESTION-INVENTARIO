using Aplicacion.Notificacion;
using Aplicacion.Venta;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class NotificacionController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(AgregarNotificacion.Ejecutar data)
        {
            return await Mediator.Send(data);
        }
    }
}
