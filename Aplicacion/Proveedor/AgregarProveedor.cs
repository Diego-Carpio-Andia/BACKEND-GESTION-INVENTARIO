using Aplicacion.Interfaces;
using Dominio;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Proveedor
{
    public class AgregarProveedor
    {
        public class Ejecutar : IRequest
        {
            public string RazonSocial { get; set; }
            public string RUC { get; set; }
            public string NumeroCelular { get; set; }
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

                Guid ProveedorId = Guid.NewGuid();
                var Proveedores = new Dominio.Proveedor()
                {
                    ProveedorId = ProveedorId,
                    NumeroCelular = request.NumeroCelular,
                    RUC = request.RUC,
                    Usuario = usuario,
                    RazonSocial = request.RazonSocial
                };
                _entityContext.Proveedor.Add(Proveedores);
                var valor = await _entityContext.SaveChangesAsync();
                if (valor > 0) {
                    return Unit.Value;
                }
                throw new Exception("No se pudo guardar el Proveedor");

            }
        }
    }
}
