using Bogus;
using Domain.Commands.v1.Biblioteca.ComprarJogo;
using Domain.Commands.v1.Biblioteca.ConsultaBiblioteca;

namespace CommonTestUtilities.Commands.Biblioteca
{
    public class ConsultaBibliotecaCommandBuilder
    {
        public static ConsultaBibliotecaCommand Build()
        {
            return new Faker<ConsultaBibliotecaCommand>()
                .RuleFor(c => c.IdUsuario, _ => Guid.NewGuid());
        }
        public static ConsultaBibliotecaCommand BuildWithId(Guid IdUsuario)
        {
            var faker = Build();
            faker.IdUsuario = IdUsuario;
            return faker;
        }

    }
}
