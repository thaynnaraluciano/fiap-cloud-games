using CommonTestUtilities.Commands.Usuarios;
using Domain.Commands.v1.Usuarios.BuscarUsuarioPorId;
using FluentAssertions;

namespace Validators.Tests.v1.Usuarios
{
    public class BuscarUsuarioPorIdCommandValidatorTest
    {
        private readonly BuscarUsuarioPorIdCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_Id_for_valido()
        {
            var request = BuscarUsuarioPorIdCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_Id_for_vazio()
        {
            var request = new BuscarUsuarioPorIdCommand(Guid.Empty);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID do usuário não pode ser um GUID vazio.");
        }
    }
}
