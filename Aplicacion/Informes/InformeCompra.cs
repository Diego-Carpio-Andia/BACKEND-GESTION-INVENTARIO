using MediatR;
using Persistencia.DapperConexion.Informes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Informes
{
    public class InformeCompra
    {
        public class Ejecutar : IRequest<List<InformesCompraModel>>
        {
            public int Cantidad { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar, List<InformesCompraModel>>
        {
            private readonly IRepositorioInformes _repositorioInformes;
            public Manejador(IRepositorioInformes repositorioInformes)
            {
                _repositorioInformes = repositorioInformes;
            }

            public async Task<List<InformesCompraModel>> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var InformesCompra = await _repositorioInformes.ObtenerInformesCompraPorCantidad(request.Cantidad);
                return InformesCompra.ToList();
            }
        }
    }
}
