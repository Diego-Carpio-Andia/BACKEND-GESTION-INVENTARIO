using Aplicacion.Interfaces;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.PronosticoDemanda
{
    public class AgregarPronosticoDemanda
    {
        public class Ejecutar : IRequest
        {
            public int CantidadPronosticada { get; set; }
            public List<Guid> Productos { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly EntityContext _entityContext;
            UserManager<Usuario> _userManager;
            IJWTGenerador _jwtGenerador;
            IUsuarioSesion _usuarioSesion;
            public Manejador(EntityContext entityContext, UserManager<Usuario> userManager, IJWTGenerador jWTGenerador, IUsuarioSesion usuarioSesion)
            {
                _entityContext = entityContext;
                _userManager = userManager;
                _jwtGenerador = jWTGenerador;
                _usuarioSesion = usuarioSesion;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                //buscamos un usuario en la base de datos con ese username
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion()) ?? throw new Exception("El usuario no se encontró en la base de datos.");


                Guid PronosticoDemandaId = Guid.NewGuid();
                var PronosticoDemanda = new Dominio.PronosticoDemanda()
                {
                    PronosticoDemandaId = PronosticoDemandaId,
                    CantidadPronosticada = request.CantidadPronosticada,
                    Usuario = usuario,
                    FechaCreacion = DateTime.UtcNow
                };
                _entityContext.PronosticoDemanda.Add(PronosticoDemanda);

                if(request.Productos != null) { 
                    
                    foreach(var producto  in request.Productos)
                    {
                        var newProductoPronosticoDemanda = new Dominio.ProductoPronosticoDemanda()
                        {
                            ProductoId = producto,
                            PronosticoDemandaId = PronosticoDemandaId
                        };
                        _entityContext.ProductoPronosticoDemanda.Add(newProductoPronosticoDemanda);
                    }
                }

                var resultado = await _entityContext.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo guardar el pronostico de demanda");
            }
        }
    }
}
