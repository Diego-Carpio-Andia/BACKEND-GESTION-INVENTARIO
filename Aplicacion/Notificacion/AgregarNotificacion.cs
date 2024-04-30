
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Notificacion
{
    public class AgregarNotificacion
    {
        public class Ejecutar : IRequest
        {
            public Guid PronosticoDemandaId { get; set; }
            public int SegundosNotificacion {  get; set; }
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
                Guid Notificacion = Guid.NewGuid();

                var nuevaNotificacion = new Dominio.Notificacion()
                {
                    NotificacionId = Notificacion,
                    PronosticoDemandaId = request.PronosticoDemandaId,
                    SegundosNotificacion = request.SegundosNotificacion,
                    FechaCreacion = DateTime.UtcNow
                };
                
                _entityContext.Notificacion.Add(nuevaNotificacion);
                var resultado = await _entityContext.SaveChangesAsync();
                if(resultado > 0) {
                    return Unit.Value;
                }
                throw new Exception("No se pudo agregar la notificacion");
            }
        }
    }
}
