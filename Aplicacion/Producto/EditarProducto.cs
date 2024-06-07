using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Producto
{
    public class EditarProducto
    {
        public class Ejecuta : IRequest
        {
            public Guid id { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public string Imagen { get; set; }
            public string Categoria { get; set; }
            public string Frecuencia { get; set; }  
            public int CantidadInventario {  get; set; }
            public decimal PrecioProveedor { get; set; }
            public decimal DolarActual { get; set; }
            public Guid ProveedorId { get; set; }   
            public ICollection<Guid> ListaVenta { get; set; }
            public ICollection<Guid> ListaCompra { get; set; }
            public ICollection<Guid> ListaPronosticoDemanda { get; set; }
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
                var ProductoEncontrado = await _entityContext.Producto.FindAsync(request.id);
                if(ProductoEncontrado == null)
                {
                    throw new ManejadorExepcion(System.Net.HttpStatusCode.NotFound, new { message = "No se encontro el Producto"});
                }

                
                ProductoEncontrado.Nombre = request.Nombre;
                ProductoEncontrado.Precio = request.Precio;
                ProductoEncontrado.Categoria = request.Categoria;
                ProductoEncontrado.ProveedorId = request.ProveedorId;
                ProductoEncontrado.CantidadInventario = request.CantidadInventario;
                // Verificar si se proporciona una nueva imagen
                if (!string.IsNullOrEmpty(request.Imagen))
                {
                    ProductoEncontrado.Imagen = Convert.FromBase64String(request.Imagen);
                }

                if (!string.IsNullOrEmpty(request.Frecuencia))
                {
                    ProductoEncontrado.Frecuencia = request.Frecuencia;
                }

                if (request.PrecioProveedor > 0)
                {
                    ProductoEncontrado.PrecioProveedor = request.PrecioProveedor;
                }
                if (request.DolarActual > 0)
                {
                    ProductoEncontrado.DolarActual = request.DolarActual;
                }

                if (request.ListaVenta !=null)
                {
                    if (request.ListaVenta.Count > 0) {
                        //ELIMINAR
                        var ProductoVentaDB = _entityContext.ProductoVenta.Where(x => x.ProductoId == request.id).ToList();
                        foreach(var ProductoVenta in ProductoVentaDB)
                        {
                            _entityContext.Remove(ProductoVenta);
                        }
                        //AGREGAR
                        foreach(var id in request.ListaVenta)
                        {
                            var nuevaVenta = new Dominio.ProductoVenta()
                            {
                                VentaId = id,
                                ProductoId = request.id
                            };
                        }
                    }
                }
                if(request.ListaCompra != null)
                {
                    if (request.ListaCompra.Count > 0) 
                    {
                        //ELIMINAR
                        var ProductoCompraDB = _entityContext.ProductoCompra.Where(x => x.ProductoId == request.id).ToList();
                        foreach(var ProductoCompra in ProductoCompraDB)
                        {
                            _entityContext.Remove(ProductoCompra);
                        }
                        //AGREGAR
                        foreach(var id in request.ListaCompra)
                        {
                            var nuevaCompra = new Dominio.ProductoCompra()
                            {
                                CompraId = id,
                                ProductoId = request.id
                            };
                        }
                    }
                }
                if(request.ListaPronosticoDemanda != null)
                {
                    if(request.ListaPronosticoDemanda.Count > 0)
                    {
                        //ELIMINAR
                        var ProductoPronosticoDemandaDB = _entityContext.ProductoPronosticoDemanda.Where(x=>x.ProductoId ==request.id).ToList();
                        foreach (var ProductoPronosticoDemanda in ProductoPronosticoDemandaDB)
                        {
                            _entityContext.Remove(ProductoPronosticoDemanda);
                        }
                        //AGREGAR
                        foreach(var id in request.ListaPronosticoDemanda)
                        {
                            var NuevoPronosticoDemanda = new Dominio.ProductoPronosticoDemanda()
                            {
                                PronosticoDemandaId = id,
                                ProductoId = request.id
                            };
                        }
                    }                    
                }

                var resultados = await _entityContext.SaveChangesAsync();
                if(resultados > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Error no se pudo editar");
            }
        }
    }
}
