using MediatR;
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
            public string Nombre { get; set; }
            public decimal? Precio { get; set; }
            public string Categoria { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
