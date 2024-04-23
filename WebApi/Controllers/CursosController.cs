using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//agregamos consultas
using Aplicacion.cursos;
using Aplicacion.cursos.DTO;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Paginacion;

namespace WebApi.Controllers
{
    // http://localhost:5000/api/Cursos
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : MiControllerBase
    {
        //mediator para que se comunique de web api a aplicacion haciendo la logica de negocio comunicandose con la base de datos
        //public readonly IMediator _mediator;
        //public CursosController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}
        //para evitar esta logica repititiva vamos a crear un controlador base padre 

        [HttpGet]
        //agregamos la autorizacion
        //[Authorize]
        //Vamos a hacer que todo el controller tenga el Authorize
        public async Task<ActionResult<List<CursoDto>>> Get()
        {
            //mediator de MIcontrollerbase que esta protegido
            return await Mediator.Send(new Consulta.ListaCursos());
        }

        // http://localhost:5000/api/Cursos/1
        //enpoint, cambiamos a curso DTO
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> Detalles(Guid id)
        {
            //la propiedad de public int Id { get; set; }
            return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }

        //Descripcion de la data lo que esta en agregar curso para ponerlo en postman
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(AgregarCurso.AgregarPeticion data)
        {
            return await Mediator.Send(data);
        }

        //Actualizamos data
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id,Editar.cabeceraEjecutar data)
        {   
            //Capturamos el id
            data.CursoId = id;
            return await Mediator.Send(data);
        }

        //id del cliente en "Editar(int id)"
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id)
        {
            return await Mediator.Send(new Eliminar.Ejecuta { id = id});
        }

        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionCurso.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}
