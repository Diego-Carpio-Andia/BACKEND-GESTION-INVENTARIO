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
    public class InformeTotal
    {
        public class Ejecutar : IRequest<List<InformesTotales>> { }
        public class Manejador : IRequestHandler<Ejecutar, List<InformesTotales>>
        {
            private readonly IRepositorioInformes _repositorioInformes;
            public Manejador(IRepositorioInformes repositorioInformes)
            {
                _repositorioInformes = repositorioInformes;
            }
            public async Task<List<InformesTotales>> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var resultado = await _repositorioInformes.ObtenerInformesTotalesCantidad();
                return resultado.ToList();
            }
        }
    }
}
