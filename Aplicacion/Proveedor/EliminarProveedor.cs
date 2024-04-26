using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Proveedor
{
    public class EliminarProveedor
    {
        public class Ejecutar : IRequest
        {
            public Guid id { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly EntityContext _entityContext;
            public Manejador(EntityContext entityContext)
            {
                _entityContext = entityContext;
            }
            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var encontrarProveedor = await _entityContext.Proveedor.FindAsync(request.id);
                if(encontrarProveedor == null)
                {
                    throw new ManejadorExepcion(System.Net.HttpStatusCode.NotFound, new { Message = "No se encontro el proveedor " });
                }

                _entityContext.Remove(encontrarProveedor);
                var respuesta = await _entityContext.SaveChangesAsync();
                if (respuesta > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar");
            }
        }
    }
}
