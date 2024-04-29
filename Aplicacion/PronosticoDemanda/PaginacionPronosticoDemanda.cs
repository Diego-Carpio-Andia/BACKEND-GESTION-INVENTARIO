using MediatR;
using Persistencia.DapperConexion.Paginacion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Aplicacion.PronosticoDemanda
{
    public class PaginacionPronosticoDemanda
    {
        public class Ejecuta : IRequest<PaginacionModel>
        {
            //filtrado por ahora solo por titulo
            public int CantidadPronosticada { get; set; }
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
                var storeProcedure = "usp_obtener_pronosticoDemanda_paginacion";
                //Ordenamiento asc o desc por titulo
                var ordenamientoColumna = "CantidadPronosticada";
                //Agregamos por ahora 1 filtro clave - valor
                var parametrosFiltro = new Dictionary<string, object>
                {
                    { "CantidadPronosticada", request.CantidadPronosticada }
                };
                return await _paginacionRepositorio.devolverPaginacion(storeProcedure, request.NumeroPagina, request.CantidadElementos, parametrosFiltro, ordenamientoColumna);

            }

        }

    }
}
