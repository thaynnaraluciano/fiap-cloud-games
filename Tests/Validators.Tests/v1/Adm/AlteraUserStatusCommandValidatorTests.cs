using CommonTestUtilities.Commands.Adm;
using Domain.Commands.v1.Adm.AlteraStatusUser;
using FluentAssertions;

namespace Validators.Tests.v1.Adm
{
    public class AlteraUserStatusCommandValidatorTests
    {
        private readonly AlteraUserStatusCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_comando_for_valido()
        {
            var command = AlteraUserStatusCommandBuilder.Build();

            var result = _validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_cGuid_for_vazio()
        {
            var command = new AlteraUserStatusCommand
            {
                cGuid = Guid.Empty,
                bStatus = true
            };

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "cGuid" &&
                e.ErrorMessage == "O ID do user não pode ser vazio.");
        }
    }
}
