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
    public class InformeTendencia
    {
        public class Ejecutar : IRequest<List<InformesTendenciaModel>>
        {
            public int Cantidad { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar, List<InformesTendenciaModel>>
        {
            private readonly IRepositorioInformes _repositorioInformes;
            public Manejador(IRepositorioInformes repositorioInformes)
            {
                _repositorioInformes = repositorioInformes;
            }

            public async Task<List<InformesTendenciaModel>> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var resultado = await _repositorioInformes.ObtenerInformesTendenciaPorCantidad(request.Cantidad);
                return resultado.ToList();
            }
        }
    }
}
