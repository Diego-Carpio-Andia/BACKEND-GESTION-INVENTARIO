using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid id { get; set; }
            
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly EntityContext _context;

            //inyectado
            public Manejador(EntityContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //conseguiremos todos los instructores relacionados a este curso de la tabla cursoinstructor
                var CursoinstructoresDB = _context.CursoInstructor.Where(x=> x.CursoId == request.id).ToList();
                foreach(var InstructorCurso in CursoinstructoresDB)
                {
                    //eliminamos la referencia en la tabla cursoinstructor
                    _context.CursoInstructor.Remove(InstructorCurso);
                }

                var ComentariosDB = _context.Comentario.Where(x => x.CursoId == request.id);
                if (ComentariosDB != null)
                {
                    foreach (var com in ComentariosDB)
                    {
                        _context.Comentario.Remove(com);
                    }
                }

                var PrecioDB = _context.Precio.Where(x=>x.CursoId == request.id).FirstOrDefault();
                if(PrecioDB != null)
                {
                    _context.Precio.Remove(PrecioDB);
                }

                var curso = await _context.Curso.FindAsync(request.id);
                if (curso == null)
                {
                    //throw new Exception("No se encontro Curso");
                    //agregamos nuestra excepcion
                    //si no encuentra el curso es un notfound
                    //arreglo de datos , creamos un arreglo nuevo con un objeto cualquiera dandole el mensaje
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mesaje = "No se encontro el curso" });
                }
                _context.Remove(curso);
                //Mandamos confirmacion guardando los cambios en la base de datos con esta identidad
                var contenido = _context.SaveChanges();
                if (contenido > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("no se pudieron guardar los cambios");

            }

        }
    }
}
