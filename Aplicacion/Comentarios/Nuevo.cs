using Dominio;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Comentarios
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            [Required(ErrorMessage = "Por favor ingrese el alumno")]
            public string Alumno { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el puntaje")]
            public int Puntaje { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el comentario")]
            public string Comentario { get; set; }
            [Required(ErrorMessage = "Por favor ingrese el cursoId")]
            public Guid CursoId { get; set; }   

        }

        public class Manejador : IRequestHandler<Ejecuta> { 
            private readonly EntityContext _context;
            public Manejador(EntityContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var comentario = new Comentario
                {
                    ComentarioId = Guid.NewGuid(),
                    Alumno = request.Alumno,
                    Puntaje = request.Puntaje,
                    ComentarioTexto = request.Comentario,
                    CursoId = request.CursoId,
                    FechaCreacion = DateTime.UtcNow
                    

                };

                _context.Comentario.Add(comentario);
                //confirmacion de la insercion de comentarios
                var resultado =  await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el comentario");
            }
        }
    }
}
