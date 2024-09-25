using Microsoft.EntityFrameworkCore;
using PartilhaAPI.Models;

namespace PartilhaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        // Defina suas entidades aqui, por exemplo:
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais para a entidade User se necessário
        }
    }
}
    