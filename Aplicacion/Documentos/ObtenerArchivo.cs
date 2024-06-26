﻿using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Documentos
{
    public class ObtenerArchivo
    {
        public class Ejecuta : IRequest<ArchivoGenerico>
        {
            public Guid Id { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, ArchivoGenerico>
        {
            private readonly EntityContext _context;
            public Manejador(EntityContext context)
            {
                _context = context;
            }

            public async Task<ArchivoGenerico> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //si no le pones el default or async el buscador se queda atascado
                var archivo = await _context.Documento.Where(x => x.ObjetoReferencia == request.Id).FirstOrDefaultAsync();
                if(archivo == null)
                {
                    throw new ManejadorExepcion(System.Net.HttpStatusCode.NotFound, new {mesaje = "No se encontro la imagen"});
                }

                var archivoGenerico = new ArchivoGenerico
                {

                    Data = Convert.ToBase64String(archivo.Contenido),
                    Nombre = archivo.Nombre,
                    Extension = archivo.Extension,
                    ObjetoReferencia = archivo.ObjetoReferencia,
                    DocumentoId = archivo.DocumentoId,
                };

                return archivoGenerico;                    
            }
        }
    }
}
