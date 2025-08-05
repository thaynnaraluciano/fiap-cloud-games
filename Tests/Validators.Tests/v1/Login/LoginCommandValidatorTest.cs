using CommonTestUtilities.Commands.Login;
using Domain.Commands.v1.Login;
using FluentAssertions;

namespace Validators.Tests.v1.Login
{
    public class LoginCommandValidatorTest
    {
        private readonly LoginCommandValidator _validator;

        public LoginCommandValidatorTest()
        {
            _validator = new LoginCommandValidator();
        }

        [Fact]
        public void Sucesso()
        {
            var request = LoginCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void EmailNulo_Erro()
        {
            var request = LoginCommandBuilder.Build();
            request.Email = null;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage.Equals("O email deve ser informado"));
        }

        [Fact]
        public void EmailVazio_Erro()
        {
            var request = LoginCommandBuilder.Build();
            request.Email = string.Empty;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage.Equals("O email deve ser informado"));
        }

        [Fact]
        public void EmailInvalido_Erro()
        {
            var request = LoginCommandBuilder.Build();
            request.Email = "emailInvalido";

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage.Equals("O email deve possuir um formato válido, example@example.com"));
        }

        [Fact]
        public void SenhaNula_Erro()
        {
            var request = LoginCommandBuilder.Build();
            request.Senha = null;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage.Equals("A senha deve ser informada"));
        }

        [Fact]
        public void SenhaVazia_Erro()
        {
            var request = LoginCommandBuilder.Build();
            request.Senha = string.Empty;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage.Equals("A senha deve ser informada"));
        }
    }
}