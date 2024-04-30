using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Venta
{
    public class EditarVenta
    {
        public class Ejecutar : IRequest
        {
            public int Cantidad { get; set; }
            public Guid VentaId { get; set; }
            public List<Guid> ListaProducto { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecutar> {
            private readonly EntityContext entityContext;
            public Manejador(EntityContext entityContext)
            {
                this.entityContext = entityContext;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var venta = await entityContext.Venta.FindAsync(request.VentaId);
                if (venta == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mesaje = "No se encontro la compra" });
                }
                venta.Cantidad = request.Cantidad;
                venta.FechaCreacion = DateTime.UtcNow;
                if (request.ListaProducto != null)
                {
                    if (request.ListaProducto.Count > 0)
                    {
                        //ELIMINAR
                        var ProductoVentaDB = entityContext.ProductoVenta.Where(x => x.VentaId == request.VentaId).ToList();
                        foreach (var ProductoVenta in ProductoVentaDB)
                        {
                            entityContext.Remove(ProductoVenta);
                        }
                        //AGREGAR
                        foreach (var id in request.ListaProducto)
                        {
                            var nuevoProductoVenta = new Dominio.ProductoVenta()
                            {
                                VentaId = request.VentaId,
                                ProductoId = id,
                            };
                            entityContext.ProductoVenta.Add(nuevoProductoVenta);
                        }
                    }
                }

                var resultados = await entityContext.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Error no se guardaron los cambios");
            }
        }

    }
}
