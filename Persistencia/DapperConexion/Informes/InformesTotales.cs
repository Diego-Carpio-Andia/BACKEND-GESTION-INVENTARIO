using System;

namespace Persistencia.DapperConexion.Informes
{
    public class InformesTotales
    {
        public int CantidadVendida { get; set; }
        public decimal TotalPrecioVendido { get; set; }
        public int CantidadComprada { get; set; }
        public decimal TotalPrecioComprado { get; set; }
    }
}
