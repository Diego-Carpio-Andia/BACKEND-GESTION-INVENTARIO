using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Dominio;
using System.Threading;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using Aplicacion.ManejadorError;
using System.Net;
using Aplicacion.cursos.DTO;
using AutoMapper;

namespace Aplicacion.cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDto>
        {
            //parametro de entrada
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, CursoDto>
        {
            //Representacion de la instancia del entity framework
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;
            public Manejador(CursosOnlineContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                //incluiremos instructorres
                //var curso = await _context.Curso.FindAsync(request.Id);
                var curso = await _context.Curso
                    //incluye comentario
                    .Include(x => x.ComentarioLista)
                    //incluye precioPromocion
                    .Include(x => x.PrecioPromocion)
                    //incluye a la lista de instructores
                    .Include(x => x.InstructoresLink).ThenInclude(y => y.Instructor).FirstOrDefaultAsync(a => a.CursoId == request.Id);

                if (curso == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mesaje = "No se encontro el curso" });
                }

                var cursoDto = _mapper.Map<Curso, CursoDto>(curso);

                return cursoDto;
            }
        }
    }
}