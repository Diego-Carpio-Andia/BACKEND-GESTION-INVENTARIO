using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.DapperConexion.Instructor
{
    //mapea la data que devolvera el procedimiento almacenado
    public class InstructorModel
    {
        //deben ser del mismo nombre con el procedimiento almacenado de la DB
        //a menos que pongas un alias en el valor
        public Guid InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Grado { get; set; }

        public DateTime? FechaCreacion { get; set; }

    }
}
