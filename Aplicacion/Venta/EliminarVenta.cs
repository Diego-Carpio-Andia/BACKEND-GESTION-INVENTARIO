using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Venta
{
    public class EliminarVenta
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly EntityContext _entityContext;
            public Manejador(EntityContext entityContext) 
            { 
                _entityContext = entityContext;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var productoVentaDB = _entityContext.ProductoVenta.Where(x=> x.VentaId == request.Id).ToList();
                foreach(var producto in productoVentaDB) { 
                    _entityContext.ProductoVenta.Remove(producto);
                }

                var VentaEncontrada = await _entityContext.Venta.FindAsync(request.Id);

                if(VentaEncontrada == null) 
                {
                    throw new ManejadorExepcion(System.Net.HttpStatusCode.NotFound, new { message = "No se encontro la venta" }); 
                }
                _entityContext.Remove(VentaEncontrada);
                var respuesta = await _entityContext.SaveChangesAsync();
                if(respuesta > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Error en eliminar la venta");

            }
        }
    }
}
