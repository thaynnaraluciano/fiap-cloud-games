using CommonTestUtilities.Commands.Jogos;
using Domain.Commands.v1.Jogos.RemoverJogo;
using FluentAssertions;

namespace Validators.Tests.v1.Jogos
{
    public class RemoverJogoCommandValidatorTest
    {
        private readonly RemoverJogoCommandValidator _validator;

        public RemoverJogoCommandValidatorTest()
        {
            _validator = new RemoverJogoCommandValidator();
        }

        [Fact]
        public void Deve_passar_quando_id_for_valido()
        {
            var command = RemoverJogoCommandBuilder.Build();

            var result = _validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void Deve_falhar_quando_id_for_guid_vazio(string guidStr)
        {
            var command = new RemoverJogoCommand(Guid.Parse(guidStr));

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID do jogo não pode ser um GUID vazio.");
        }

        [Fact]
        public void Deve_falhar_quando_id_for_vazio()
        {
            var command = new RemoverJogoCommand(Guid.Empty);

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID do jogo não pode ser um GUID vazio.");
        }
    }
}
