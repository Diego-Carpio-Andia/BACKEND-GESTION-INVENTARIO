using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class RolLista
    {
        public class Ejecuta : IRequest<List<IdentityRole>>{}
        public class Manejador : IRequestHandler<Ejecuta, List<IdentityRole>>
        {
            //representacion de los objetos de la base de datos para listar
            //context representa todos los elementos de la base de datos tambien con el coreidentitycore
            private readonly EntityContext _context;
            public Manejador(EntityContext context)
            {
                _context = context;
            }

            public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //tabla de roles configurado en identitycore
                var resultado = await _context.Roles.ToListAsync();
                return resultado;
            }
        }

    }
}
