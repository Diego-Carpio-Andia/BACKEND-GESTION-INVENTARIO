using MediatR;
using Persistencia.DapperConexion.Paginacion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Compra
{
    public class PaginacionCompra
    {
        //retornara PaginacionModel
        public class Ejecuta : IRequest<PaginacionModel>
        {
            //filtrado por ahora solo por titulo
            public string Cantidad { get; set; }
            //numero de pagina
            public int NumeroPagina { get; set; }
            //cantidad de elementos
            public int CantidadElementos { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, PaginacionModel>
        {
            private readonly IPaginacion _paginacionRepositorio;
            public Manejador(IPaginacion paginacionRepositorio)
            {
                _paginacionRepositorio = paginacionRepositorio;
            }    
            public async Task<PaginacionModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var storeProcedure = "usp_obtener_compra_paginacion";
                var ordenamientoColumna = "Cantidad";
                var parametrosFiltro = new Dictionary<string, object> {
                    { "Cantidad" , request.Cantidad }
                };
                return await _paginacionRepositorio.devolverPaginacion(storeProcedure, request.NumeroPagina, request.CantidadElementos, parametrosFiltro, ordenamientoColumna);
            }

        }
    }
}
