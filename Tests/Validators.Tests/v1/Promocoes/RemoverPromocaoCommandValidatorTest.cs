using CommonTestUtilities.Commands.Promocoes;
using Domain.Commands.v1.Promocoes.RemoverPromocao;
using FluentAssertions;

namespace Validators.Tests.v1.Promocoes
{
    public class RemoverPromocaoCommandValidatorTest
    {
        private readonly RemoverPromocaoCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_comando_for_valido()
        {
            var request = RemoverPromocaoCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_id_for_vazio()
        {
            var request = new RemoverPromocaoCommand(Guid.Empty);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID da promoção não pode ser vazio.");
        }

        [Fact]
        public void Deve_falhar_quando_id_for_guid_empty()
        {
            var request = new RemoverPromocaoCommand(Guid.Empty);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID da promoção não pode ser um GUID vazio.");
        }
    }
}
