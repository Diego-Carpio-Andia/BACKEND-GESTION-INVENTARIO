﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Tablas
{
    public class ProductoCompra
    {
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        public Guid CompraId { get; set; }
        public Compra Compra { get; set; }
    }
}
