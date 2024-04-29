using Dominio.Tablas;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Compra
{
    public class AgregarCompra
    {
        public class Ejecuta : IRequest
        {
            public int Cantidad { get; set; }
            public Guid UsuarioId { get; set; }
            public List<Guid> ListaProducto { get; set; }
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
                Guid _CompraId = Guid.NewGuid();
                var compra = new Dominio.Tablas.Compra()
                {
                    CompraId = _CompraId,
                    Cantidad = request.Cantidad,
                    UsuarioId = request.UsuarioId,
                    FechaCreacion = DateTime.UtcNow
                };
                _entityContext.Compra.Add(compra);

                if (request.ListaProducto != null)
                {
                    foreach (var item in request.ListaProducto)
                    {
                        var ProductoCompra = new ProductoCompra()
                        {
                            CompraId = _CompraId,
                            ProductoId = item
                        };
                        _entityContext.ProductoCompra.Add(ProductoCompra);

                        _entityContext.ProductoCompra.Add(ProductoCompra);
                        //CAMBIAMOS LA CANTIDAD en producto restandole
                        var buscado = await _entityContext.Producto.FindAsync(item);
                        buscado.CantidadInventario += request.Cantidad;
                    }
                }

                var valor = await _entityContext.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar la compra");
            }
        }
    }
}
