using Aplicacion.Venta;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Paginacion;
using System.Threading.Tasks;
using System;
using Aplicacion.Compra;

namespace WebApi.Controllers
{
    public class CompraController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(AgregarCompra.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Modificar(Guid id, EditarCompra.Ejecutar data)
        {
            data.CompraId = id;
            return await Mediator.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            //Creamos la instancia y le asignamos el Id es como hacer el put 
            return await Mediator.Send(new EliminarCompra.Ejecuta { Id = id });
        }
        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionCompra.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}
