﻿using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class RoleController : MiControllerBase
    {
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> Crear(RolNuevo.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }
        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> Eliminar(RolEliminar.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }
        [HttpGet]
        public async Task<ActionResult<List<IdentityRole>>> Lista()
        {
            //si no tiene parametros
            return await Mediator.Send(new RolLista.Ejecuta());
        }
        [HttpPost("agregarRoleUsuario")]
        public async Task<ActionResult<Unit>> AgregarRoleUsuario(UsuarioRolAgregar.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }
        [HttpPost("eliminarRoleUsuario")]
        public async Task<ActionResult<Unit>> EliminarRoleUsuario(UsuarioRolEliminar.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }
        [HttpGet("obtenerRolesPorUsuario/{username}")]
        public async Task<ActionResult<List<string>>> ObtenerRolesPorUsuario(string username)
        {
            return await Mediator.Send(new ObtenerRolesPorUsuario.Ejecuta { Username = username });
        }
        

    }
}
