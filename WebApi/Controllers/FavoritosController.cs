using Aplicacion.Actividad;
using Aplicacion.Favoritos;
using Aplicacion.Venta;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Paginacion;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class FavoritosController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(AgregarFavorito.Ejecutar data)
        {
            return await Mediator.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            //Creamos la instancia y le asignamos el Id es como hacer el put 
            return await Mediator.Send(new EliminarFavoritos.Ejecutar { FavoritosId = id });
        }
        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionFavoritos.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}
