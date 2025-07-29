using CommonTestUtilities.Commands.Promocoes;
using Domain.Commands.v1.Promocoes.BuscarPromocaoPorId;
using FluentAssertions;

namespace Validators.Tests.v1.Promocoes
{
    public class BuscarPromocaoPorIdCommandValidatorTest
    {
        private readonly BuscarPromocaoPorIdCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_id_for_valido()
        {
            var request = BuscarPromocaoPorIdCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();

        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void Deve_falhar_quando_id_for_vazio(string guidString)
        {
            var guid = Guid.Parse(guidString);
            var request = new BuscarPromocaoPorIdCommand(guid);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID da promoção não pode ser um GUID vazio.");
        }
    }
}
