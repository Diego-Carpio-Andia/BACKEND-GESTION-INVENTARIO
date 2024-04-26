using Aplicacion.ManejadorError;
using Dominio;
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
    public class Editar
    {
        public class cabeceraEjecutar : IRequest
        {
            public Guid CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            //para que permita nulos DateTime?
            public DateTime? FechaPublicacion { get; set; }
            //Parametro para editar lista de instructores
            public List<Guid> ListaInstructor { get; set; }
            public decimal? Precio {  get; set; }    
            //Pueden ser nulos con ?
            public decimal? Promocion { get; set; }
        }

        public class Manejador : IRequestHandler<cabeceraEjecutar>
        {
            private readonly EntityContext _context;
            public Manejador(EntityContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(cabeceraEjecutar request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId);
                if(curso == null)
                {
                    //404 no se encontro el recurso
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mesaje = "No se encontro el curso" });
                }

                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;
                curso.FechaCreacion = DateTime.UtcNow;

                //logica para actualizar el precio del curso
                //devolver el primer valor que encuentre y cumpla la condicion
                var PrecioEntidad = _context.Precio.Where(x=>x.CursoId == request.CursoId).FirstOrDefault();    
                if(PrecioEntidad != null)
                {
                    //si hay precio 
                    PrecioEntidad.Promocion = request.Promocion ?? PrecioEntidad.Promocion;
                    PrecioEntidad.PrecioActual = request.Precio ?? PrecioEntidad.PrecioActual;
                }
                else
                {
                    //si no hay precio
                    PrecioEntidad = new Precio
                    {
                        PrecioId = Guid.NewGuid(),
                        PrecioActual = request.Precio ?? 0,
                        Promocion = request.Promocion ?? 0,
                        CursoId = curso.CursoId,
                    };
                    await _context.Precio.AddAsync(PrecioEntidad);
                }

                //setearemos la lista de instructores si el cliente elimina los clientes
                if(request.ListaInstructor != null) { 
                    if(request.ListaInstructor.Count > 0) {
                        /*Eliminar los isntructores actuales del curso en la base de datos*/ //todos los que concuerden con cursoId
                        //devuelve codigos GUID de la base de datos
                        var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.CursoId).ToList();
                        foreach(var instructorEliminar in instructoresDB) { 
                            /*Eliminar id*/
                            _context.CursoInstructor.Remove(instructorEliminar);    
                        }
                        /*Los que agregue en la peticion*/
                        foreach(var id in request.ListaInstructor)
                        {
                            var nuevoInstructor = new CursoInstructor
                            {
                                CursoId = request.CursoId,
                                InstructorId = id
                            };
                            _context.CursoInstructor.Add(nuevoInstructor);
                        }
                    }
                }

                var resultado = await _context.SaveChangesAsync();

                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se guardaron los cambios en el curso");

            }

        }


    }
}
