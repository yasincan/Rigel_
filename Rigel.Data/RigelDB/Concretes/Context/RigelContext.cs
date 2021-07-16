using Microsoft.EntityFrameworkCore;
using Rigel.Data.RigelDB.Concretes.Entities;

namespace Rigel.Data.RigelDB.Concretes.Context
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
