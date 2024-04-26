using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Proveedor
{
    public class EditarProveedor
    {
        public class Ejecutar : IRequest
        {
            public Guid id { get; set; }
            public string RazonSocial { get; set; }
            public string RUC {  get; set; }
            public string NumeroCelular { get; set; }   
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
                var ProveedorId = await _entityContext.Proveedor.FindAsync(request.id);
                if(ProveedorId == null)
                {
                    throw new ManejadorExepcion(System.Net.HttpStatusCode.NotFound, new { message = "No se encontro el Proveedor" });
                }
                ProveedorId.RUC = request.RUC;
                ProveedorId.RazonSocial = request.RazonSocial;
                ProveedorId.NumeroCelular = request.NumeroCelular;
                var resultados = await _entityContext.SaveChangesAsync();
                if(resultados > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Error no se pudo Editar el Proveedor");
            }
        }
    }
}
