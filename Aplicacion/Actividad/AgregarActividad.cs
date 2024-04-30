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

namespace Aplicacion.Actividad
{
    public class AgregarActividad
    {
        public class Ejecutar : IRequest
        {
            public string TipoActividad { get; set; }
            public string DescripcionActividad { get; set; }
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


                Guid ActividadId = Guid.NewGuid();
                var nuevaActividad = new Dominio.Actividad()
                {
                    ActividadId = ActividadId,
                    Usuario = usuario,
                    TipoActividad = request.TipoActividad,
                    DescripcionActividad = request.DescripcionActividad,
                    FechaCreacion = DateTime.UtcNow
                };
                _entityContext.Actividad.Add(nuevaActividad);
                var resultados = await _entityContext.SaveChangesAsync();
                if (resultados > 0) {
                    return Unit.Value;
                }
                throw new Exception("No se pudo agregar la actividad");

            }
        }

    }
}
