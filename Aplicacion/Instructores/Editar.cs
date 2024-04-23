using MediatR;
using Persistencia.DapperConexion.Instructor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Instructores
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            [Required(ErrorMessage = "Por favor ingrese el codigo del instructor")]
            public Guid InstructorId { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el nombre del instructor")]
            public string Nombre { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el Apellido del instructor")]
            public string Apellidos { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el Grado del instructor")]
            public string Grado { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly IInstructor _instructorRepositorio;
            public Manejador(IInstructor instructorRepositorio)
            {
                _instructorRepositorio = instructorRepositorio;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var resultado = await _instructorRepositorio.Actualiza(request.InstructorId, request.Nombre,request.Apellidos,request.Grado);
                if(resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo actualizar la data del instructor ");
            }
        }


    }
}
