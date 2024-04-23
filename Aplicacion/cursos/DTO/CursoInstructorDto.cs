using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.cursos.DTO
{
    public class CursoInstructorDto
    {
        public Guid CursoId { get; set; }
        public Guid InstructorId { get; set; }
    }
}
