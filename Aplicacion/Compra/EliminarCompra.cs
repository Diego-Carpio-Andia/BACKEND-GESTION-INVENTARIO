using Aplicacion.ManejadorError;
using FluentValidation.Internal;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Compra
{
    public class EliminarCompra
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
                var productoCompraDB = _entityContext.ProductoCompra.Where(x => x.CompraId == request.Id).ToList();
                foreach (var productoCompra in productoCompraDB)
                {
                    _entityContext.ProductoCompra.Remove(productoCompra);
                }

                var CompraEncontrada = await _entityContext.Compra.FindAsync(request.Id);
                if(CompraEncontrada == null)
                {
                    throw new ManejadorExepcion(System.Net.HttpStatusCode.NotFound, new {message = "No se encontro la compra"});
                }
                _entityContext.Remove(CompraEncontrada);
                var respuesta = await _entityContext.SaveChangesAsync();
                if(respuesta > 0) {
                    return Unit.Value;
                }
                throw new Exception("Error en eliminar la compra");
            }
        }
    }
}
