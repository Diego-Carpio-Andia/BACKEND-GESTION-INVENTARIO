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
    public class InformeVenta
    {
        public class Ejecutar : IRequest<List<InformesVentaModel>>
        {
            public int Cantidad { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar, List<InformesVentaModel>>
        {
            private readonly IRepositorioInformes _repositorioInformes;
            public Manejador(IRepositorioInformes repositorioInformes)
            {
                _repositorioInformes = repositorioInformes;
            }

            public async Task<List<InformesVentaModel>> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var InformesVenta = await _repositorioInformes.ObtenerInformesVentaPorCantidad(request.Cantidad);
                return InformesVenta.ToList();
            }
        }
    }
}
