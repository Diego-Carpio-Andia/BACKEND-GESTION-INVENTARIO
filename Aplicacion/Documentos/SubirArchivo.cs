using Dominio;
using iTextSharp.text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Documentos
{
    public class SubirArchivo
    {
        public class Ejecuta : IRequest
        {
            public Guid ObjetoReferencia { get; set; }
            public string Data { get; set; }
            public string Nombre { get; set; }
            public string Extension { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            //insertamos en la DB
            private readonly EntityContext _context;
            public Manejador(EntityContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // si no le pones el default or async el buscador se queda atascado ejecutandose el objeto next
                var document = await _context.Documento.Where(x => x.ObjetoReferencia == request.ObjetoReferencia).FirstOrDefaultAsync();
                if(document == null) {
                    //puede guardar ese archivo directamente
                    var doc = new Documento
                    {
                        Contenido = Convert.FromBase64String(request.Data),
                        Nombre = request.Nombre,
                        Extension = request.Extension,
                        DocumentoId = Guid.NewGuid(),
                        FechaCreacion = DateTime.UtcNow,
                        ObjetoReferencia = request.ObjetoReferencia,
                    };
                    //AGREGAMOS AL CONTEXTO
                    _context.Documento.Add(doc);
                }
                else
                {
                    //Si no es nulo se actualiza
                    document.Contenido = Convert.FromBase64String(request.Data);
                    document.Nombre = request.Nombre;
                    document.Extension = request.Extension;
                    document.FechaCreacion = DateTime.UtcNow;
                    document.ObjetoReferencia = request.ObjetoReferencia;

                }
                //GUARDAMOS TODOS LOS CAMBIOS QUE SE REALIZO
                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar el archivo");

            }
        }
    }
}
