using Aplicacion.Interfaces;
using Dominio;
using iTextSharp.text.xml.simpleparser;
using MediatR;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Identity;
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
            public int CantidadInventario { get; set; }
            public string Imagen {  get; set; }
            public decimal PrecioProveedor { get; set; }
            public decimal DolarActual { get; set; }
            public Guid ProveedorId { get; set; }
            public ICollection<Guid> ListaVenta { get; set; }
            public ICollection<Guid> ListaCompra {  get; set; }
            public ICollection<Guid> ListaPronosticoDemanda { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly EntityContext entityContext;
            UserManager<Usuario> _userManager;
            IJWTGenerador _jwtGenerador;
            IUsuarioSesion _usuarioSesion;
            public Manejador(EntityContext _entityContext, UserManager<Usuario> userManager, IJWTGenerador jWTGenerador, IUsuarioSesion usuarioSesion)
            {
                entityContext = _entityContext;
                _userManager = userManager;
                _jwtGenerador = jWTGenerador;
                _usuarioSesion = usuarioSesion;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //buscamos un usuario en la base de datos con ese username
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion()) ?? throw new Exception("El usuario no se encontró en la base de datos.");


                Guid productoId = Guid.NewGuid();               
                var producto = new Dominio.Producto()
                {
                    Productoid = productoId,
                    Nombre = request.Nombre,
                    Precio = request.Precio,
                    Usuario = usuario,
                    Categoria = request.Categoria,
                    ProveedorId = request.ProveedorId,
                    CantidadInventario = request.CantidadInventario,
                    FechaCreacion = DateTime.UtcNow
                };
                if (!string.IsNullOrEmpty(request.Imagen))
                {
                    producto.Imagen = Convert.FromBase64String(request.Imagen);
                }
                if (request.PrecioProveedor > 0)
                {
                    producto.PrecioProveedor = request.PrecioProveedor;
                }
                if(request.DolarActual > 0)
                {
                    producto.DolarActual = request.DolarActual;
                }
                //Agregamos el nuevo producto
                entityContext.Producto.Add(producto);
                if (request.ListaVenta != null)
                {
                    foreach (var id in request.ListaVenta)
                    {
                        var productoVenta = new Dominio.ProductoVenta()
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
                        var productoCompra = new Dominio.ProductoCompra()
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
                        var productoPronosticoDemanda = new Dominio.ProductoPronosticoDemanda()
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
