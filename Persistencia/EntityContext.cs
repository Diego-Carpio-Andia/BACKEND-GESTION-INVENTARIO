using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


//Ahora vamos a convertir a entidades a las clases 
namespace Persistencia
{
    public class EntityContext : IdentityDbContext<Usuario> //DbContext antiguo, usaremos indetityDbContext para migrar todas las tablas necesarias para usuarios
    {
        //Contructor representacion de la base de datos
        public EntityContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //generamos las tablas donde se almacenara los datos del usuario con IdentityDbContext
            //el archivo de migracion
            base.OnModelCreating(modelBuilder);

          
            //configurar las claves primarias compuestas en una ENTIDAD llamada ProductoVenta (FK Y PK) a la vez tanto ProductoId como VentaId
            modelBuilder.Entity<ProductoVenta>().HasKey(pv => new { pv.ProductoId, pv.VentaId});

            //configurar las claves primarias compuestas en una ENTIDAD llamada CursoInstructor
            modelBuilder.Entity<ProductoCompra>().HasKey(pc => new { pc.ProductoId, pc.CompraId});

            //configurar las claves primarias compuestas en una ENTIDAD llamada CursoInstructor
            modelBuilder.Entity<ProductoPronosticoDemanda>().HasKey(pp => new { pp.ProductoId, pp.PronosticoDemandaId});


            


        }
       
        public DbSet<Documento> Documento { get; set; }

        //Convertimos a entidades cada una de las clases con los mismos nombres
        public DbSet<Actividad> Actividad { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Favoritos> Favoritos {  get; set; }
        public DbSet<Notificacion> Notificacion {  get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<ProductoCompra> ProductoCompra { get; set; }
        public DbSet<ProductoPronosticoDemanda> ProductoPronosticoDemanda { get; set; }
        public DbSet<ProductoVenta> ProductoVenta { get; set; } 
        public DbSet<PronosticoDemanda> PronosticoDemanda { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Venta> Venta { get; set; } 
    }
}