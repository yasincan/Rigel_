using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rigel.Data.Contexts
{
    public class ContextFactory : IDesignTimeDbContextFactory<RigelContext>
    {
        public RigelContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RigelContext>();
            optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=Rigel;Trusted_Connection=True;MultipleActiveResultSets=true;");

            return new RigelContext(optionsBuilder.Options);
        }
    }
}
