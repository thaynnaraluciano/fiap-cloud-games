using CommonTestUtilities.Commands.Usuarios;
using Domain.Commands.v1.Usuarios.CriarUsuario;
using FluentAssertions;

namespace Validators.Tests.v1.Usuarios
{
    public class CriarUsuarioCommandValidatorTest
    {
        private readonly CriarUsuarioCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_comando_for_valido()
        {
            var request = CriarUsuarioCommandBuilder.Build();

            var resultado = _validator.Validate(request);

            resultado.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_nome_estiver_vazio()
        {
            var request = CriarUsuarioCommandBuilder.Build();
            request.Nome = string.Empty;

            var resultado = _validator.Validate(request);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e =>
                e.PropertyName == "Nome" &&
                e.ErrorMessage == "O nome do usuário é obrigatório.");
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("a")]
        public void Deve_falhar_quando_nome_tiver_menos_de_3_caracteres(string nomeInvalido)
        {
            var request = CriarUsuarioCommandBuilder.Build();
            request.Nome = nomeInvalido;

            var resultado = _validator.Validate(request);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e =>
                e.PropertyName == "Nome" &&
                e.ErrorMessage == "O nome o usuário deve ter entre 5 e 100 caracteres");
        }

        [Fact]
        public void Deve_falhar_quando_email_estiver_vazio()
        {
            var request = CriarUsuarioCommandBuilder.Build();
            request.Email = string.Empty;

            var resultado = _validator.Validate(request);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e =>
                e.PropertyName == "Email" &&
                e.ErrorMessage == "O e-mail do usuário é obrigatório.");
        }

        [Fact]
        public void Deve_falhar_quando_email_tiver_menos_de_3_caracteres()
        {
            var request = CriarUsuarioCommandBuilder.Build();
            request.Email = "a";

            var resultado = _validator.Validate(request);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e =>
                e.PropertyName == "Email" &&
                e.ErrorMessage == "O e-mail o usuário deve ter entre 5 e 100 caracteres");
        }
    }
}
