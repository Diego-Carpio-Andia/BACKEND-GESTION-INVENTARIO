using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Favoritos
{
    public class EliminarFavoritos
    {
        public class Ejecutar : IRequest
        {
            public Guid FavoritosId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly EntityContext entityContext;
            public Manejador(EntityContext entityContext)
            {
                this.entityContext = entityContext;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var buscarFavorito = await entityContext.Favoritos.FindAsync(request.FavoritosId);
                if(buscarFavorito == null)
                {
                    new ManejadorExepcion(System.Net.HttpStatusCode.NotFound, new {message = "No se encontro el favorito"});
                } 
                entityContext.Remove(buscarFavorito);
                var resultados = await entityContext.SaveChangesAsync();
                if(resultados > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar");
            }
        }

    }
}
