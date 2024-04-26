using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


//VAMOS A AGREGAR UN MIDDLEWARE
namespace Aplicacion.cursos
{
    public class AgregarCurso
    {
        //data a enviar en la peticion
        public class AgregarPeticion : IRequest
        {
            //AGREGAMOS VALIDACION, en este caso Titulo va a ser requerido si o si
            //[Required(ErrorMessage="Por favor ingrese el titulo del curso")]
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime FechaPublicacion { get; set; }
            //lista de instructores usando Guid en vez de int
            public List<Guid> ListaInstructor {  get; set; }
            public decimal Precio {  get; set; }  
            public decimal Promocion { get; set; }  


            
        }

        //USAREMOS FLUENT VALIDATION
        //public class EjecutaValidacion : AbstractValidator<AgregarPeticion>
        //{
        //    public EjecutaValidacion()
        //    {
        //        Agregamos una regla de validacion para el titulo
        //        RuleFor(x => x.Titulo).NotEmpty();
        //    }
        //}

        //logica de la peticion
        public class Manejador : IRequestHandler<AgregarPeticion>
        {

            private readonly EntityContext _context;
            public Manejador(EntityContext context)
            {
                _context = context;
            }

            //"control c" a la peticion
            public async Task<Unit> Handle(AgregarPeticion request, CancellationToken cancellationToken)
            {
                //valor aleatorio que va a ser el identificador del id
                Guid _cursoId = Guid.NewGuid(); 
                var curso = new Curso
                {   
                    CursoId = _cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FechaCreacion = DateTime.UtcNow
                };

                //agregamos el nuevo registro de curso
                _context.Curso.Add(curso);

                //vamos a agregar multiples instructores ya sea 1 o varios
                if (request.ListaInstructor != null)
                {
                    foreach(var id in request.ListaInstructor) {
                        var cursoInstructor = new CursoInstructor
                        {
                            CursoId = _cursoId,
                            InstructorId = id,
                        };
                        _context.CursoInstructor.Add(cursoInstructor);
                    }
                }

                var precioEntidad = new Precio
                {
                    CursoId = _cursoId,
                    PrecioActual = request.Precio,
                    Promocion = request.Promocion,
                    PrecioId = Guid.NewGuid(),
                };

                _context.Precio.Add(precioEntidad); 

                //confirmamos la transaccion ademas de devolver un estado de la transaccion
                //0 => no se hizo la transaccion
                //1 => si se hizo la transaccion
                //SaveChangesAsync();numero de operaciones que se esta efectuando en la base de datos
                var valor = await _context.SaveChangesAsync();

                if (valor > 0)
                {
                    //enviamos un flag 1 o 0
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el curso");
            }
         
        }
    }

}
