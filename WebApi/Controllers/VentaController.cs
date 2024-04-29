using Aplicacion.Venta;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Paginacion;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class VentaController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(AgregarVenta.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Modificar(Guid id, EditarVenta.Ejecutar data)
        {
            data.VentaId = id;
            return await Mediator.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            //Creamos la instancia y le asignamos el Id es como hacer el put 
            return await Mediator.Send(new EliminarVenta.Ejecuta { Id = id });
        }
        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionVenta.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}
