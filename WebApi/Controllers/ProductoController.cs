using Aplicacion.Compra;
using Aplicacion.Producto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class ProductoController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(AgregarProducto.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new EliminarProducto.Ejecutar { Id = id });
        }
    }
}
