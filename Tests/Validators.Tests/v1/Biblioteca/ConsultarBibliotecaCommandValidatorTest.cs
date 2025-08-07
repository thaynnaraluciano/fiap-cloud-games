using CommonTestUtilities.Commands.Biblioteca;
using Domain.Commands.v1.Biblioteca.ConsultaBiblioteca;
using FluentAssertions;


namespace Validators.Tests.v1.Biblioteca
{
    public class ConsultarBibliotecaCommandValidatorTest
    {
        private readonly ConsultaBibliotecaCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_id_for_valido()
        {
            var request = ConsultaBibliotecaCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_id_for_guid_vazio()
        {
            var request = ConsultaBibliotecaCommandBuilder.BuildWithId(Guid.Empty);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "IdUsuario" &&
                e.ErrorMessage == "O ID do usuário não pode ser um GUID vazio.");
        }

    }
}
