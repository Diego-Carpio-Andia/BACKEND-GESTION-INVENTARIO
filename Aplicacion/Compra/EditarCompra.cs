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

namespace Aplicacion.Compra
{
    public class EditarCompra
    {
        public class Ejecutar : IRequest
        {
            public Guid CompraId { get; set; }
            public string MetodoPago { get; set; }
            public int Cantidad { get; set; }
            public List<Guid> ListaProducto { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly EntityContext entityContext;
            public Manejador(EntityContext entityContext)
            {
                this.entityContext = entityContext;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var compra = await entityContext.Compra.FindAsync(request.CompraId);
                if(compra == null) {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mesaje = "No se encontro la compra" });
                }
                compra.Cantidad = request.Cantidad;
                compra.FechaCreacion = DateTime.UtcNow;
                compra.MetodoPago = request.MetodoPago;
                if(request.ListaProducto != null)
                {
                    if(request.ListaProducto.Count > 0)
                    {
                        //ELIMINAR
                        var ProductoCompraDB = entityContext.ProductoCompra.Where(x=>x.CompraId == request.CompraId).ToList();
                        foreach(var productoCompra in ProductoCompraDB)
                        {
                            entityContext.Remove(productoCompra);
                        }
                        //AGREGAR
                        foreach(var id in request.ListaProducto)
                        {
                            var nuevoProductoCompra = new Dominio.ProductoCompra()
                            {
                                CompraId = request.CompraId,
                                ProductoId = id,
                            };
                            entityContext.ProductoCompra.Add(nuevoProductoCompra);
                        }
                    }
                }

                var resultados = await entityContext.SaveChangesAsync();    
                if(resultados > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Error no se guardaron los cambios");
            }
        }
    }
}
