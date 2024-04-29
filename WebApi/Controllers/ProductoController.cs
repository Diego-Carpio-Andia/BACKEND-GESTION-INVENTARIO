using Aplicacion.Compra;
using Aplicacion.Producto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Paginacion;
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
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Actualizar(Guid id,EditarProducto.Ejecuta data)
        {
            data.id = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new EliminarProducto.Ejecutar { Id = id });
        }

        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionProducto.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}
