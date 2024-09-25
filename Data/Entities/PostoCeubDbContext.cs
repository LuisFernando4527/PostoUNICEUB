using Microsoft.EntityFrameworkCore;

namespace PostoCeub.Data.Entities
{
    public class PostoCeubDbContext : DbContext
    {
        public PostoCeubDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Enfermeiro> Enfermeiro { get; set; }
        
    }
}
