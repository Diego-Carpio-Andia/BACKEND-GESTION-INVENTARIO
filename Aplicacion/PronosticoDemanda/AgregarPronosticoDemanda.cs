using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.PronosticoDemanda
{
    public class AgregarPronosticoDemanda
    {
        public class Ejecutar : IRequest
        {
            public int CantidadPronosticada { get; set; }
            public List<Guid> Productos { get; set; }
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
                Guid PronosticoDemandaId = Guid.NewGuid();
                var PronosticoDemanda = new Dominio.PronosticoDemanda()
                {
                    PronosticoDemandaId = PronosticoDemandaId,
                    CantidadPronosticada = request.CantidadPronosticada,
                    FechaCreacion = DateTime.UtcNow
                };
                _entityContext.PronosticoDemanda.Add(PronosticoDemanda);

                if(request.Productos != null) { 
                    
                    foreach(var producto  in request.Productos)
                    {
                        var newProductoPronosticoDemanda = new Dominio.ProductoPronosticoDemanda()
                        {
                            ProductoId = producto,
                            PronosticoDemandaId = PronosticoDemandaId
                        };
                        _entityContext.ProductoPronosticoDemanda.Add(newProductoPronosticoDemanda);
                    }
                }

                var resultado = await _entityContext.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo guardar el pronostico de demanda");
            }
        }
    }
}
