using Microsoft.EntityFrameworkCore;
using Rigel.Data.Entities;

namespace Rigel.Data.Contexts
{
    public class RigelContext : DbContext
    {
        public RigelContext(DbContextOptions<RigelContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
