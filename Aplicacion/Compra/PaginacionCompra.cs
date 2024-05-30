using Aplicacion.Interfaces;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;
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
            //public string Cantidad { get; set; }
            //numero de pagina
            public int NumeroPagina { get; set; }
            //cantidad de elementos
            public int CantidadElementos { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, PaginacionModel>
        {
            UserManager<Usuario> _userManager;
            IJWTGenerador _jwtGenerador;
            IUsuarioSesion _usuarioSesion;
            private readonly EntityContext _cursosOnlineContext;

            private readonly IPaginacion _paginacionRepositorio;
            public Manejador(IPaginacion paginacionRepositorio, UserManager<Usuario> userManager, IJWTGenerador jwtGenerador, IUsuarioSesion usuarioSesion, EntityContext context)
            {
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
                _userManager = userManager;
                _cursosOnlineContext = context;
                _paginacionRepositorio = paginacionRepositorio;
            }
            public async Task<PaginacionModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //buscamos un usuario en la base de datos con ese username
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion()) ?? throw new Exception("El usuario no se encontró en la base de datos.");

                var storeProcedure = "usp_obtener_compra_paginacion";
                var ordenamientoColumna = "Cantidad";
                var parametrosFiltro = new Dictionary<string, object> {
                    { "UsuarioId" , usuario.Id }
                };
                return await _paginacionRepositorio.devolverPaginacion(storeProcedure, request.NumeroPagina, request.CantidadElementos, parametrosFiltro, ordenamientoColumna);
            }

        }
    }
}
