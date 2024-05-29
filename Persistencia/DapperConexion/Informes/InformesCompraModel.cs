using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistencia.DapperConexion.Informes
{
    public class InformesCompraModel
    {
        public string Producto { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public int Cantidad {  get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public DateTime FechaCreacion { get; set; }
        public decimal TOTALCOMPRADO { get; set; }

    }
}
