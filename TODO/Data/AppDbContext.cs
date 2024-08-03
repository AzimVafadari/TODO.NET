using Microsoft.EntityFrameworkCore;
using TODO.Models;

namespace TODO.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { set; get; }
        public DbSet<Todo> Todos { set; get; }
        public DbSet<Status> Statuses { set; get; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Todo>()
                .HasKey(t => t.TodoId);

            modelBuilder.Entity<Todo>()
                .Property(t => t.TodoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Status>()
                .HasKey(s => s.StatusId);
            
            modelBuilder.Entity<Status>()
                .Property(s => s.StatusId)
                .ValueGeneratedOnAdd();
        }
    }
}
