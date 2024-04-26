using Dominio.Tablas;
using iTextSharp.text.xml.simpleparser;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Compra
{
    public class AgregarProducto
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public string Categoria { get; set; }
            public ICollection<Guid> ListaVenta { get; set; }
            public ICollection<Guid> ListaCompra {  get; set; }
            public ICollection<Guid> ListaPronosticoDemanda { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly EntityContext entityContext;
            public Manejador(EntityContext entityContext)
            {
                this.entityContext = entityContext;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Guid productoId = Guid.NewGuid();
                var producto = new Dominio.Tablas.Producto()
                {
                    Productoid = productoId,
                    Nombre = request.Nombre,
                    Precio = request.Precio,
                    Categoria = request.Categoria,
                    FechaCreacion = DateTime.UtcNow
                };

                //Agregamos el nuevo producto
                entityContext.Producto.Add(producto);
                if (request.ListaVenta != null)
                {
                    foreach (var id in request.ListaVenta)
                    {
                        var productoVenta = new ProductoVenta()
                        {
                            ProductoId = productoId,
                            VentaId = id,
                        };
                        entityContext.ProductoVenta.Add(productoVenta);
                    }                     
                }
                if (request.ListaCompra != null)
                {
                    foreach (var id in request.ListaCompra)
                    {
                        var productoCompra = new ProductoCompra()
                        {
                            ProductoId = productoId,
                            CompraId = id,
                        };
                        entityContext.ProductoCompra.Add(productoCompra);
                    }
                }
                if(request.ListaPronosticoDemanda != null)
                {
                    foreach(var id in request.ListaPronosticoDemanda)
                    {
                        var productoPronosticoDemanda = new ProductoPronosticoDemanda()
                        {
                            ProductoId = productoId,
                            PronosticoDemandaId = id,
                        };
                        entityContext.ProductoPronosticoDemanda.Add(productoPronosticoDemanda);
                    }
                }

                var valor = await entityContext.SaveChangesAsync();

                if(valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el Producto");
            }
        }
    }
}
