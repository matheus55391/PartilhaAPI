using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Defina suas entidades aqui, por exemplo:
    // public DbSet<Expense> Expenses { get; set; }
}
