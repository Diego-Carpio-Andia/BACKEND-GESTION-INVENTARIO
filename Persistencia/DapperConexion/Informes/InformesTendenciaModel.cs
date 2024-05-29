using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistencia.DapperConexion.Informes
{
    public class InformesTendenciaModel
    {
        public string Nombre { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public byte[] Imagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CantidadPronosticada { get; set; }
    }
}
