using Dominio.Tablas;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Venta
{
    public class AgregarVenta
    {
        public class Ejecuta : IRequest
        {
            public int Cantidad {  get; set; }
            public Guid UsuarioId {  get; set; }
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
                Guid _VentaId = Guid.NewGuid();
                var venta = new Dominio.Tablas.Venta()
                {
                    VentaId = _VentaId,
                    Cantidad = request.Cantidad,
                    UsuarioId = request.UsuarioId,
                    FechaCreacion = DateTime.UtcNow
                };
                _entityContext.Venta.Add(venta);

                if(request.ListaProducto != null)
                {
                    foreach(var item in request.ListaProducto)
                    {
                        var ProductoVenta = new ProductoVenta()
                        {
                            VentaId = _VentaId,
                            ProductoId = item
                        };
                        _entityContext.ProductoVenta.Add(ProductoVenta);
                    }
                }

                var valor = await _entityContext.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar la venta");
            }
        }
    }
}
