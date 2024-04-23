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
    public class Nuevo
    {
        public class Ejecuta : IRequest{
            [Required(ErrorMessage = "Por favor ingrese el nombre del instructor")]
            public string Nombre { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el Apellido del instructor")]
            public string Apellidos { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el Grado del instructor")]
            public string Grado { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly IInstructor _InstructorRepository;
            public Manejador(IInstructor instructorRepository)
            {
                _InstructorRepository = instructorRepository;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var resultado = await _InstructorRepository.Nuevo(request.Nombre,request.Apellidos,request.Grado);

                if(resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo inssertar el instructor");
            }
        }
    }
}
