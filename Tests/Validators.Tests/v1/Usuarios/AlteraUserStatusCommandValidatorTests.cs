using CommonTestUtilities.Commands.Usuarios;
using Domain.Commands.v1.Usuarios.AlterarStatusUsuario;
using FluentAssertions;

namespace Validators.Tests.v1.Usuarios
{
    public class AlteraUserStatusCommandValidatorTests
    {
        private readonly AlterarStatusUsuarioCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_comando_for_valido()
        {
            var request = AlteraUserStatusCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_cGuid_for_vazio()
        {
            var request = new AlterarStatusUsuarioCommand
            {
                cGuid = Guid.Empty,
                bStatus = true
            };

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "cGuid" &&
                e.ErrorMessage == "O ID do user não pode ser vazio.");
        }
    }
}
