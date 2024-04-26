using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Actividad
{
    public class AgregarActividad
    {
        public class Ejecutar : IRequest
        {
            public Guid UsuarioId { get; set; }
            public string TipoActividad { get; set; }
            public string DescripcionActividad { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly EntityContext _entityContext;
            public Manejador(EntityContext entityContext)
            {
                _entityContext = entityContext;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                Guid ActividadId = Guid.NewGuid();
                var nuevaActividad = new Dominio.Tablas.Actividad()
                {
                    ActividadId = ActividadId,
                    UsuarioId = request.UsuarioId,
                    TipoActividad = request.TipoActividad,
                    DescripcionActividad = request.DescripcionActividad,
                    FechaCreacion = DateTime.UtcNow
                };
                _entityContext.Actividad.Add(nuevaActividad);
                var resultados = await _entityContext.SaveChangesAsync();
                if (resultados > 0) {
                    return Unit.Value;
                }
                throw new Exception("No se pudo agregar la actividad");

            }
        }

    }
}
