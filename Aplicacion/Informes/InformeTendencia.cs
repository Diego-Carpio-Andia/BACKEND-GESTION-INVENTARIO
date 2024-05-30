using Aplicacion.Interfaces;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;
using Persistencia.DapperConexion.Informes;
using Persistencia.DapperConexion.Paginacion;
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
            
        }
        public class Manejador : IRequestHandler<Ejecutar, List<InformesTendenciaModel>>
        {
            private readonly IRepositorioInformes _repositorioInformes;

            UserManager<Usuario> _userManager;
            IJWTGenerador _jwtGenerador;
            IUsuarioSesion _usuarioSesion;
            private readonly EntityContext _cursosOnlineContext;

            private readonly IPaginacion _paginacionRepositorio;
            public Manejador(IRepositorioInformes repositorioInformes, IPaginacion paginacionRepositorio, UserManager<Usuario> userManager, IJWTGenerador jwtGenerador, IUsuarioSesion usuarioSesion, EntityContext context)
            {
                _repositorioInformes = repositorioInformes;
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
                _userManager = userManager;
                _cursosOnlineContext = context;
                _paginacionRepositorio = paginacionRepositorio;
            }

            public async Task<List<InformesTendenciaModel>> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                //buscamos un usuario en la base de datos con ese username
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion()) ?? throw new Exception("El usuario no se encontró en la base de datos.");


                var resultado = await _repositorioInformes.ObtenerInformesTendenciaPorCantidad(usuario.Id);
                return resultado.ToList();
            }
        }
    }
}
