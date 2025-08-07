using CommonTestUtilities.Commands.Biblioteca;
using CommonTestUtilities.Commands.Jogos;
using Domain.Commands.v1.Biblioteca.ComprarJogo;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.Tests.v1.Biblioteca
{
    public class ComprarJogoCommandValidatorTest
    {
        private readonly ComprarJogoCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_dados_estao_validos()
        {
            var request = ComprarJogoCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }
        [Fact]
        public void Deve_falhar_quando_id_user_estiver_vazio()
        {
            var request = ComprarJogoCommandBuilder.BuildWithId(Guid.Empty, Guid.NewGuid());

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "IdUsuario" &&
                e.ErrorMessage == "O ID do usuário não pode ser um GUID vazio.");
        }
        [Fact]
        public void Deve_falhar_quando_id_jogo_estiver_vazio()
        {
            var request = ComprarJogoCommandBuilder.BuildWithId(Guid.NewGuid(), Guid.Empty);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "IdJogo" &&
                e.ErrorMessage == "O ID do jogo não pode ser um GUID vazio.");
        }


    }
}
