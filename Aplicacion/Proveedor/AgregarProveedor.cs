using Dominio.Tablas;
using MediatR;
using Microsoft.VisualBasic;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Proveedor
{
    public class AgregarProveedor
    {
        public class Ejecutar : IRequest
        {
            public string RazonSocial { get; set; }
            public string RUC { get; set; }
            public string NumeroCelular { get; set; }
            public Guid UsuarioId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly EntityContext _entityContext;
            public Manejador(EntityContext entityContext) { 
            _entityContext = entityContext;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                Guid ProveedorId = Guid.NewGuid();
                var Proveedores = new Dominio.Tablas.Proveedor()
                {
                    ProveedorId = ProveedorId,
                    NumeroCelular = request.NumeroCelular,
                    RUC = request.RUC,
                    UsuarioId = request.UsuarioId,
                    RazonSocial = request.RazonSocial
                };
                _entityContext.Proveedor.Add(Proveedores);
                var valor = await _entityContext.SaveChangesAsync();
                if (valor > 0) {
                    return Unit.Value;
                }
                throw new Exception("No se pudo guardar el Proveedor");

            }
        }
    }
}
