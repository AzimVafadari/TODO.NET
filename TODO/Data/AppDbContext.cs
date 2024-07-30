using Microsoft.EntityFrameworkCore;
using TODO.Models;

namespace TODO.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { set; get; }
        public DbSet<Todo> Todos { set; get; }
    }
}
