using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comentario
    {
        //A NIVEL EMPRESARIAL SE PONE EL GUID
        public Guid ComentarioId { get; set; }
        public string Alumno { get; set; }
        public int Puntaje { get; set; }
        public string ComentarioTexto { get; set; }
        public Guid CursoId { get; set; }
        public Curso Curso { get; set; }
        //nuevo campo para actualizar la migracion, para que se actualice en la base de datos
        public DateTime? FechaCreacion { get; set; }
    }

}