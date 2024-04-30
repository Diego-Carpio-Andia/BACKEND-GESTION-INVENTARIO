using Aplicacion.Interfaces;
using Dominio;

using MediatR;
using Microsoft.AspNetCore.Identity;
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
            public List<Guid> ListaProducto { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly EntityContext _entityContext;
            UserManager<Usuario> _userManager;
            IJWTGenerador _jwtGenerador;
            IUsuarioSesion _usuarioSesion;
            public Manejador(EntityContext entityContext, UserManager<Usuario> userManager, IJWTGenerador jWTGenerador, IUsuarioSesion usuarioSesion)
            {
                _entityContext = entityContext;
                _userManager = userManager;
                _jwtGenerador = jWTGenerador;
                _usuarioSesion = usuarioSesion;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //buscamos un usuario en la base de datos con ese username
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion()) ?? throw new Exception("El usuario no se encontró en la base de datos.");


                Guid _CompraId = Guid.NewGuid();
                var compra = new Dominio.Compra()
                {
                    CompraId = _CompraId,
                    Cantidad = request.Cantidad,
                    Usuario = usuario,
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
