using Aplicacion.Venta;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Paginacion;
using System.Threading.Tasks;
using System;
using Aplicacion.Proveedor;

namespace WebApi.Controllers
{
    public class ProveedorController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(AgregarProveedor.Ejecutar data)
        {
            return await Mediator.Send(data);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Modificar(Guid id, EditarProveedor.Ejecutar data)
        {
            data.id = id;
            return await Mediator.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            //Creamos la instancia y le asignamos el Id es como hacer el put 
            return await Mediator.Send(new EliminarProveedor.Ejecutar { id = id });
        }
        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionProveedor.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}
