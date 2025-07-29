using CommonTestUtilities.Commands.Usuarios;
using Domain.Commands.v1.Usuarios.RemoverUsuario;
using FluentAssertions;

namespace Validators.Tests.v1.Usuarios
{
    public class RemoverUsuarioCommandValidatorTest
    {
        private readonly RemoverUsuarioCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_comando_for_valido()
        {
            var request = RemoverUsuarioCommandBuilder.Build();

            var resultado = _validator.Validate(request);

            resultado.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_id_estiver_vazio()
        {
            var request = new RemoverUsuarioCommand(Guid.Empty);

            var resultado = _validator.Validate(request);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID do usuário não pode estar vazio.");
        }

        [Fact]
        public void Deve_falhar_quando_id_for_nulo()
        {
            var request = new RemoverUsuarioCommand(default);

            var resultado = _validator.Validate(request);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                (e.ErrorMessage == "O ID do usuário não pode estar vazio." ||
                 e.ErrorMessage == "O ID do usuário não pode ser nulo."));
        }
    }
}
