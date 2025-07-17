using Infrastructure.Data.Models.Jogos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<JogoModel> Jogos { get; set; }
    }
}
