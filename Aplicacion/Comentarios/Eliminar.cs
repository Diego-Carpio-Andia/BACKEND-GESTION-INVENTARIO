using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.AspNetCore.Server.IIS.Core;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Comentarios
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }    
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            EntityContext _context;

            public Manejador(EntityContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var comentario = await _context.Comentario.FindAsync(request.Id);
                if( comentario == null ) {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el comentario" });
                }

                _context.Remove(comentario);
                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0 )
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo eliminar el comentario");

            }
        }
    }
}
