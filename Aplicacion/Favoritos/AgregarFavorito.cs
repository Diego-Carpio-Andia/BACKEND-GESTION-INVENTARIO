using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Favoritos
{
    public class AgregarFavorito
    {
        public class Ejecutar : IRequest
        {
            public Guid ProductoId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly EntityContext _entityContext;
            public Manejador(EntityContext entityContext)
            {
                _entityContext = entityContext;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                Guid FavoritoId = Guid.NewGuid();
                var nuevoFavorito = new Dominio.Tablas.Favoritos()
                {
                    FavoritosId = FavoritoId,
                    ProductoId = request.ProductoId,
                    FechaCreacion = DateTime.UtcNow
                };
                _entityContext.Favoritos.Add(nuevoFavorito);
                var resultado = await _entityContext.SaveChangesAsync();
                if(resultado > 0) {
                    return Unit.Value;
                }
                throw new Exception("No se pudo guardar el Favorito");
            }
        }

    }
}
