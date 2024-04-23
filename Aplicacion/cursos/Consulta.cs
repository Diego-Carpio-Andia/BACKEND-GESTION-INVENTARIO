using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Dominio;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Aplicacion.cursos.DTO;
using AutoMapper;

//el patron es el siguiente
namespace Aplicacion.cursos
{
    public class Consulta
    {
        //lo que va a devolver cuando ejecutes  la clase consulta
        //cabecera
        public class ListaCursos : IRequest<List<CursoDto>> { }
        //Metodo logica
        public class Manejador : IRequestHandler<ListaCursos, List<CursoDto>>
        {

            //Representacion de la instancia del entity framework
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;   
            public Manejador(CursosOnlineContext context, IMapper mapper /*mapeo de DTOs con identidades*/)
            {
                this._context = context;
                _mapper = mapper;
            }

            public async Task<List<CursoDto>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                //devuelve curso con sus instructores relacionados con InstructoresLink cursos se relaciono con cursoInstructores
                //Ahora se tiene que relacionar de cursoInstructores a Instructores relacionados a este curso
                //Esto esta mal ya que devolvemos directamente la data al cliente de la entidad curso directamente
                //para ello vamos a crear un DTO que esta orientado a entregar data a un cliente
                
                //DAta que tiene que mapear de cursos a CursosDTO
                var cursos = await _context.Curso
                    //incluye comentario
                    .Include(x=>x.ComentarioLista)
                    //incluye precioPromocion
                    .Include(x=>x.PrecioPromocion)
                    //incluye a la lista de instructores
                    .Include(x=>x.InstructoresLink)
                    .ThenInclude(x=>x.Instructor).ToListAsync();
                //return cursos;

                //libreria automaper convierte de una clase entity framework a clase DTO
                //el proposito de la clase Entity framework es hacer una persistencia en la DB
                //para mostrar correctamente la data a un cliente se utiliza DTO ya que las consultas son complejas
                //Clase que modelar la data que se envia al cliente su unico objetivo es ser el punto de modelacion de la data de entitycore y lo que desea el usuario
                //AUTOMAPPER PARA ESO
                //puedes encontrar el automapper en MappingProfile.cs

                //mapear List<Curso> ORIGEN a DESTINO List<CursoDTO>
                var cursosDTO = _mapper.Map<List<Curso>, List<CursoDto>>(cursos);
                
                return cursosDTO; 
            }

        }

    }
}