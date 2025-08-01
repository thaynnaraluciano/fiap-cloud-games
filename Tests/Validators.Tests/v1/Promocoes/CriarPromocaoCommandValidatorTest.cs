using CommonTestUtilities.Commands.Promocoes;
using Domain.Commands.v1.Promocoes.CriarPromocao;
using FluentAssertions;

namespace Validators.Tests.v1.Promocoes
{
    public class CriarPromocaoCommandValidatorTest
    {
        private readonly CriarPromocaoCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_dados_validos()
        {
            var request = CriarPromocaoCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Deve_falhar_quando_nome_for_vazio_ou_nulo(string nome)
        {
            var request = CriarPromocaoCommandBuilder.Build();
            request.Nome = nome;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome" && e.ErrorMessage == "O nome da promoção é obrigatório.");
        }

        [Fact]
        public void Deve_falhar_quando_nome_excede_50_caracteres()
        {
            var request = CriarPromocaoCommandBuilder.Build();
            request.Nome = new string('x', 51);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome" && e.ErrorMessage == "O nome da promoção deve ter no máximo 50 caracteres.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Deve_falhar_quando_desconto_nao_for_maior_que_zero(decimal desconto)
        {
            var request = CriarPromocaoCommandBuilder.Build();
            request.Desconto = desconto;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Desconto" && e.ErrorMessage == "O desconto deve ser maior que zero.");
        }

        [Fact]
        public void Deve_falhar_quando_data_inicio_maior_que_data_fim()
        {
            var request = CriarPromocaoCommandBuilder.Build();
            request.DataInicio = DateTime.Now.AddDays(10);
            request.DataFim = DateTime.Now;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "DataInicio" && e.ErrorMessage == "A data de início deve ser anterior ou igual à data de fim.");
        }

        [Fact]
        public void Deve_falhar_quando_data_fim_for_anterior_ao_hoje()
        {
            var request = CriarPromocaoCommandBuilder.Build();
            request.DataFim = DateTime.Now.AddDays(-1);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "DataFim" && e.ErrorMessage == "A data de fim deve ser maior ou igual à data atual.");
        }

        [Fact]
        public void Deve_falhar_quando_lista_de_jogos_estiver_vazia()
        {
            var request = CriarPromocaoCommandBuilder.Build();
            request.JogosIds = new List<Guid>();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "JogosIds" && e.ErrorMessage == "A lista de jogos IDs não pode estar vazia.");
        }
    }
}
