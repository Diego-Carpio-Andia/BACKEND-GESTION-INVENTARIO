using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


//Ahora vamos a convertir a entidades a las clases 
namespace Persistencia
{
    public class CursosOnlineContext : IdentityDbContext<Usuario> //DbContext antiguo, usaremos indetityDbContext para migrar todas las tablas necesarias para usuarios
    {
        //Contructor representacion de la base de datos
        public CursosOnlineContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //generamos las tablas donde se almacenara los datos del usuario con IdentityDbContext
            //el archivo de migracion
            base.OnModelCreating(modelBuilder);

            //configurar las claves primarias compuestas en una ENTIDAD llamada CursoInstructor
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new { ci.InstructorId, ci.CursoId });

        }
        //Convertir a identidades a cada una de las clases con las mismas nombres 
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Precio> Precio { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<CursoInstructor> CursoInstructor { get; set; }
        public DbSet<Documento> Documento { get; set; }
    }
}