using CommonTestUtilities.Commands.Jogos;
using Domain.Commands.v1.Jogos.AtualizarJogo;
using Domain.Commands.v1.Jogos.BuscarJogo;
using FluentAssertions;

namespace Validators.Tests.v1.Jogos
{
    public class BuscarPorIdCommandValidatorTest
    {
        private readonly BuscarJogoPorIdCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_id_for_valido()
        {
            var request = BuscarJogoPorIdCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_id_for_guid_vazio()
        {
            var request = BuscarJogoPorIdCommandBuilder.BuildComId(Guid.Empty);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID do jogo não pode ser um GUID vazio.");
        }
    }
}
