using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Rigel.Data.RigelDB.Concretes.Context;

namespace Rigel.Data.Contexts
{
    public class ContextFactory : IDesignTimeDbContextFactory<RigelContext>
    {
        public RigelContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RigelContext>();
            optionsBuilder.UseNpgsql("Host=192.168.1.111;Port=5432;Database=RigelDB;User ID=postgres;Password=Ps1234567;");

            return new RigelContext(optionsBuilder.Options);
        }
    }
}
