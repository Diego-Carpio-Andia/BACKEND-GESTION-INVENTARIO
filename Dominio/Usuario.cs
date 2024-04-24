using Dominio.Tablas;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    //IdentityUser => contiene propiedades que manejaremos con identity
    //importante recordar que el usuario sera almacenado en la base de datos como AspNetUsers por la migracion
    public class Usuario : IdentityUser
    {
        //agregaremos algunas propiedades mas aparte de las que se heredo
        public string NombreCompleto { get; set; }
        public ICollection<Compra> ListaCompra {  get; set; }
        public ICollection<Venta> ListaVenta { get; set; }
        public ICollection<Actividad> ListaActividades { get; set; }
        public ICollection<Proveedor> ListaProveedores { get; set; }
    }
}
