﻿using Aplicacion.ManejadorError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class ManejadorErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErrorMiddleware> _logger;
        //manejamos los estados de respuesta al cliente
        public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;   
        }
        //Metodo para manejar un request 
        public async Task Invoke(HttpContext context)
        {
            try
            {
                //pasa al siguuiente
                await _next(context);

            }
            catch(Exception ex)
            {
                await ManejadorExepcionAsincrono(context, ex, _logger);
            }

        }

        private async Task ManejadorExepcionAsincrono(HttpContext context, Exception ex, ILogger<ManejadorErrorMiddleware> logger)
        {
            object errores = null;
            switch (ex)
            {
                //excepcion HTTP
                case ManejadorExepcion me:
                    logger.LogError(ex, "Manejador Error");
                    errores = me.Error;
                    context.Response.StatusCode = (int)me.Codigo;
                    break;
                //Excepcion generico
                case Exception e:
                    logger.LogError(ex, "Error en el servidor");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if(errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new {errores});
                await context.Response.WriteAsync(resultados);
            }
        }


    }
}
