using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aplicacion.cursos.DTO
{
    public class PrecioDto
    {
        public Guid PrecioId { get; set; }
        //VAMOS A INDICARLE LA LONGITUD DE ESTE DECIMAL
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioActual { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Promocion { get; set; }
        public Guid CursoId { get; set; }
    }
}
