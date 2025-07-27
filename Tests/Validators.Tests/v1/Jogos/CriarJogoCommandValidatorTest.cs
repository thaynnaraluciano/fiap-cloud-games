﻿using CommonTestUtilities.Commands.Jogos;
using Domain.Commands.v1.Jogos.CriarJogo;
using FluentAssertions;

namespace Validators.Tests.v1.Jogos
{
    public class CriarJogoCommandValidatorTest
    {
        private readonly CriarJogoCommandValidator _validator = new();

        [Fact]
        public void ValidarComando_Valido_DeveRetornarSucesso()
        {
            var request = CriarJogoCommandBuilder.Build();
            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_nome_esta_vazio()
        {
            var request = CriarJogoCommandBuilder.Build();
            request.Nome = string.Empty;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome" && e.ErrorMessage == "O nome do jogo é obrigatório.");
        }

        [Fact]
        public void Deve_falhar_quando_nome_tem_menos_de_3_caracteres()
        {
            var request = CriarJogoCommandBuilder.Build();
            request.Nome = "AB";

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome" && e.ErrorMessage == "O nome do jogo deve ter entre 3 e 100 caracteres.");
        }

        [Fact]
        public void Deve_falhar_quando_nome_tem_mais_de_100_caracteres()
        {
            var request = CriarJogoCommandBuilder.Build();
            request.Nome = new string('A', 101);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome" && e.ErrorMessage == "O nome do jogo deve ter entre 3 e 100 caracteres.");
        }

        [Fact]
        public void Deve_falhar_quando_descricao_esta_vazia()
        {
            var request = CriarJogoCommandBuilder.Build();
            request.Descricao = string.Empty;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Descricao" && e.ErrorMessage == "A descrição do jogo é obrigatória.");
        }

        [Fact]
        public void Deve_falhar_quando_descricao_tem_menos_de_10_caracteres()
        {
            var request = CriarJogoCommandBuilder.Build();
            request.Descricao = "Curto";

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Descricao" && e.ErrorMessage == "A descrição do jogo deve ter entre 10 e 500 caracteres.");
        }

        [Fact]
        public void Deve_falhar_quando_descricao_tem_mais_de_500_caracteres()
        {
            var request = CriarJogoCommandBuilder.Build();
            request.Descricao = new string('D', 501);

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Descricao" && e.ErrorMessage == "A descrição do jogo deve ter entre 10 e 500 caracteres.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Deve_falhar_quando_preco_e_zero_e_negativo(decimal preco)
        {
            var request = CriarJogoCommandBuilder.Build();
            request.Preco = preco;

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Preco" && e.ErrorMessage == "O preço do jogo deve ser maior que zero.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-365)]
        public void Deve_falhar_quando_data_lancamento_esta_no_passado(int diasNoFuturo)
        {
            var command = CriarJogoCommandBuilder.Build();
            command.DataLancamento = DateTime.Now.AddDays(diasNoFuturo);

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "DataLancamento" && e.ErrorMessage == "A data de lançamento não pode ser no passado.");
        }
    }
}
