using CommonTestUtilities.Commands.Promocoes;
using Domain.Commands.v1.Promocoes.AtualizarPromocao;
using FluentAssertions;

namespace Validators.Tests.v1.Promocoes
{
    public class AtualizarPromocaoCommandValidatorTest
    {

        private readonly AtualizarPromocaoCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_comando_for_valido()
        {
            var request = AtualizarPromocaoCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_id_for_vazio()
        {
            var request = AtualizarPromocaoCommandBuilder.Build();
            request.Id = Guid.Empty;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O Id da promoção é obrigatório.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_falhar_quando_nome_for_vazio_ou_nulo(string nome)
        {
            var request = AtualizarPromocaoCommandBuilder.Build();
            request.Nome = nome;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Nome" &&
                e.ErrorMessage == "O nome da promoção é obrigatório.");
        }

        [Fact]
        public void Deve_falhar_quando_nome_ultrapassa_50_caracteres()
        {
            var request = AtualizarPromocaoCommandBuilder.Build();
            request.Nome = new string('x', 51);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Nome" &&
                e.ErrorMessage == "O nome da promoção deve ter no máximo 50 caracteres.");
        }

        [Fact]
        public void Deve_falhar_quando_desconto_for_menor_ou_igual_a_zero()
        {
            var request = AtualizarPromocaoCommandBuilder.Build();
            request.Desconto = 0;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Desconto" &&
                e.ErrorMessage == "O desconto deve ser maior que zero.");
        }

        [Fact]
        public void Deve_falhar_quando_data_inicio_for_maior_que_data_fim()
        {
            var request = AtualizarPromocaoCommandBuilder.Build();
            request.DataInicio = DateTime.Now.AddDays(10);
            request.DataFim = DateTime.Now.AddDays(5);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "DataInicio" &&
                e.ErrorMessage == "A data de início deve ser anterior ou igual à data de fim.");
        }

        [Fact]
        public void Deve_falhar_quando_data_fim_for_igual_ou_menor_que_data_inicio()
        {
            var request = AtualizarPromocaoCommandBuilder.Build();
            request.DataInicio = DateTime.Now;
            request.DataFim = request.DataInicio;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "DataFim" &&
                e.ErrorMessage == "A data de fim deve ser posterior à data de início.");
        }

        [Fact]
        public void Deve_falhar_quando_lista_de_jogos_estiver_vazia()
        {
            var request = AtualizarPromocaoCommandBuilder.Build();
            request.JogosIds = [];

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "JogosIds" &&
                e.ErrorMessage == "A lista de jogos IDs não pode estar vazia.");
        }
    }
}
