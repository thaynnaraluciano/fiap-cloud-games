using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Adm.AlteraStatusUser;
using Infrastructure.Data.Models.Biblioteca;
using Infrastructure.Data.Models.Jogos;
using Infrastructure.Data.Models.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<JogoModel> Jogos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<BibliotecaModel> Biblioteca { get; set; }
    }
}
