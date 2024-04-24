using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Tablas
{
    public class Proveedor
    {
        public Guid ProveedorId { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string NumeroCelular { get; set; }
    }
}
