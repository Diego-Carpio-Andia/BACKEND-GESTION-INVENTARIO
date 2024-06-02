﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Dominio
{
    public class Venta
    {
        public Guid VentaId { get; set; }
        public int Cantidad { get; set; }
        public string MetodoPago { get; set; }
        //como la clase usuario HEREDA de entityUser entonces 
        //automaticamente se hace la union de uno a muchos 
        public Usuario Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<ProductoVenta> ProductoLink { get; set; }
    }
}
