using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<pessoaTeste> pessoaTeste { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    public class pessoaTeste 
    {
        public int id { get; set; }
        public string? NomePessoa { get; set; }
    }
}
