using Microsoft.EntityFrameworkCore;
using PartilhaAPI.Models;

namespace PartilhaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionMember> TransactionMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do relacionamento entre User e Friend
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendUser)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração do relacionamento entre User e Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.TransactionsCreated)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.PaidBy)
                .WithMany(u => u.TransactionsPaid)
                .HasForeignKey(t => t.PaidById)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração do relacionamento entre Transaction e TransactionMember
            modelBuilder.Entity<TransactionMember>()
                .HasOne(tm => tm.Transaction)
                .WithMany(t => t.Members)
                .HasForeignKey(tm => tm.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TransactionMember>()
                .HasOne(tm => tm.User)
                .WithMany(u => u.TransactionMembers)
                .HasForeignKey(tm => tm.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
