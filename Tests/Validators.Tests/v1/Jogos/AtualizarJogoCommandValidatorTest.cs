using CommonTestUtilities.Commands.Jogos;
using Domain.Commands.v1.Jogos.AtualizarJogo;
using FluentAssertions;

namespace Validators.Tests.v1.Jogos
{
    public class AtualizarJogoCommandValidatorTest
    {
        private readonly AtualizarJogoCommandValidator _validator = new();

        [Fact]
        public void Deve_passar_quando_dados_estao_validos()
        {
            var request = AtualizarJogoCommandBuilder.Build();

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_id_estiver_vazio()
        {
            var request = AtualizarJogoCommandBuilder.BuildWithId(Guid.Empty);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Id" &&
                e.ErrorMessage == "O ID do jogo é obrigatório.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_falhar_quando_nome_for_nulo_ou_vazio(string nome)
        {
            var request = AtualizarJogoCommandBuilder.Build();
            request.Nome = nome;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Nome" &&
                e.ErrorMessage == "O nome do jogo é obrigatório.");
        }

        [Fact]
        public void Deve_falhar_quando_nome_tem_menos_de_3_caracteres()
        {
            var request = AtualizarJogoCommandBuilder.Build();
            request.Nome = "AB";

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome" && e.ErrorMessage == "O nome do jogo deve ter entre 3 e 100 caracteres.");
        }

        [Fact]
        public void Deve_falhar_quando_nome_tem_mais_de_100_caracteres()
        {
            var request = AtualizarJogoCommandBuilder.Build();
            request.Nome = new string('A', 101);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome" && e.ErrorMessage == "O nome do jogo deve ter entre 3 e 100 caracteres.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_falhar_quando_descricao_for_nula_ou_vazia(string descricao)
        {
            var request = AtualizarJogoCommandBuilder.Build();
            request.Descricao = descricao;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Descricao" &&
                e.ErrorMessage == "A descrição do jogo é obrigatória.");
        }

        [Fact]
        public void Deve_falhar_quando_descricao_tem_menos_de_10_caracteres()
        {
            var request = AtualizarJogoCommandBuilder.Build();
            request.Descricao = "Curto";

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Descricao" && e.ErrorMessage == "A descrição do jogo deve ter entre 10 e 500 caracteres.");
        }

        [Fact]
        public void Deve_falhar_quando_descricao_tem_mais_de_500_caracteres()
        {
            var request = AtualizarJogoCommandBuilder.Build();
            request.Descricao = new string('D', 501);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Descricao" && e.ErrorMessage == "A descrição do jogo deve ter entre 10 e 500 caracteres.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Deve_falhar_quando_preco_for_zero_ou_negativo(decimal preco)
        {
            var request = AtualizarJogoCommandBuilder.Build();
            request.Preco = preco;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "Preco" &&
                e.ErrorMessage == "O preço do jogo deve ser maior que zero.");
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-2)]
        public void Deve_falhar_quando_data_de_lancamento_estiver_no_passado(int diasNoPassado)
        {
            var request = AtualizarJogoCommandBuilder.Build();
            request.DataLancamento = DateTime.Now.AddDays(diasNoPassado);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "DataLancamento" &&
                e.ErrorMessage == "A data de lançamento não pode ser no passado.");
        }
    }
}
