using CommonTestUtilities.Commands.Usuarios;
using Domain.Commands.v1.Usuarios.AtualizarUsuario;
using FluentAssertions;

namespace Validators.Tests.v1.Usuarios
{
    public class AtualizarUsuarioCommandValidatorTest
    {
        private readonly AtualizarUsuarioCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_comando_for_valido()
        {
            var request = AtualizarUsuarioCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_Id_for_vazio()
        {
            var request = AtualizarUsuarioCommandBuilder.Build();
            request.Id = Guid.Empty;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID do usuário é obrigatório.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("AB")]
        public void Deve_falhar_quando_Nome_for_invalido(string nomeInvalido)
        {
            var request = AtualizarUsuarioCommandBuilder.Build();
            request.Nome = nomeInvalido;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("ab")]
        public void Deve_falhar_quando_Email_for_invalido(string emailInvalido)
        {
            var request = AtualizarUsuarioCommandBuilder.Build();
            request.Email = emailInvalido;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Email");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        public void Deve_falhar_quando_PerfilUsuario_for_invalido(int perfilInvalido)
        {
            var request = AtualizarUsuarioCommandBuilder.Build();
            request.PerfilUsuario = perfilInvalido;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "PerfilUsuario" &&
                e.ErrorMessage == "Tipo de usuário incorreto");
        }
    }
}
