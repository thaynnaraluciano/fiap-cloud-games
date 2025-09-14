using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql
            (
                "Server=;Database=DbFiap;User=;Password=;",
                new MySqlServerVersion(new Version(8, 0, 42))
            );
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
