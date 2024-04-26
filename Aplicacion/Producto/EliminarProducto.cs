using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Producto
{
    public class EliminarProducto
    {
        public class Ejecutar : IRequest
        {
            public Guid Id { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar> { 
        
            private readonly EntityContext _entityContext;
            public Manejador(EntityContext entityContext)
            {
                _entityContext = entityContext;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var productoVentaDB = _entityContext.ProductoVenta.Where(x=> x.ProductoId == request.Id).ToList();
                var productoCompraDB = _entityContext.ProductoCompra.Where(x=> x.ProductoId == request.Id).ToList();
                var productoPronosticoDemandaDB = _entityContext.ProductoPronosticoDemanda.Where(x=> x.ProductoId == request.Id).ToList();
                //ELIMINAMOS LAS FK
                foreach(var productoVenta in productoVentaDB)
                {
                    _entityContext.ProductoVenta.Remove(productoVenta);
                }
                foreach(var productoCompra in productoCompraDB)
                {
                    _entityContext.ProductoCompra.Remove(productoCompra);
                }
                foreach(var productoPronosticoDemanda in productoPronosticoDemandaDB)
                {
                    _entityContext.ProductoPronosticoDemanda.Remove(productoPronosticoDemanda);
                }

                var productoEncontrado = await _entityContext.Producto.FindAsync(request.Id);

                if(productoEncontrado == null)
                {
                    throw new ManejadorExepcion(System.Net.HttpStatusCode.NotFound, new { message = "No se encontro el producto" });
                }

                _entityContext.Remove(productoEncontrado);
                var respuesta = await _entityContext.SaveChangesAsync();
                if(respuesta > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudieron guardar los datos");
            }
        }
    }
}
