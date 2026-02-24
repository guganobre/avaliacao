using Avaliacao.Domain.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Avaliacao.Application.Validators
{
    public class CriarSeguroRequestValidator : AbstractValidator<CriarSeguroRequest>
    {
        public CriarSeguroRequestValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Requisição não pode ser nula.");

            RuleFor(x => x.Veiculo)
                .NotNull()
                .WithMessage("Dados do veículo são obrigatórios.");

            RuleFor(x => x.Segurado)
                .NotNull()
                .WithMessage("Dados do segurado são obrigatórios.");

            RuleFor(x => x.Veiculo.MarcaModelo)
                .NotEmpty()
                .WithMessage("Marca/Modelo do veículo é obrigatório.")
                .MaximumLength(100)
                .WithMessage("Marca/Modelo não pode ter mais de 100 caracteres.");

            RuleFor(x => x.Veiculo.Valor)
                .GreaterThan(0)
                .WithMessage("Valor do veículo deve ser maior que zero.");

            RuleFor(x => x.Segurado.Nome)
                .NotEmpty()
                .WithMessage("Nome do segurado é obrigatório.")
                .MinimumLength(3)
                .WithMessage("Nome deve ter no mínimo 3 caracteres.")
                .MaximumLength(150)
                .WithMessage("Nome não pode ter mais de 150 caracteres.")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$")
                .WithMessage("Nome deve conter apenas letras e espaços.");

            RuleFor(x => x.Segurado.Cpf)
                .NotEmpty()
                .WithMessage("CPF do segurado é obrigatório.")
                .Custom((cpf, context) =>
                {
                    var cpfLimpo = LimparCpf(cpf);
                    
                    if (!ValidarCpf(cpfLimpo))
                    {
                        context.AddFailure("Cpf", "CPF inválido.");
                    }
                });

            RuleFor(x => x.Segurado.Idade)
                .GreaterThanOrEqualTo(18)
                .WithMessage("Idade do segurado deve ser no mínimo 18 anos.")
                .LessThanOrEqualTo(100)
                .WithMessage("Idade do segurado deve ser no máximo 100 anos.");
        }

        private static string LimparCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return string.Empty;

            return Regex.Replace(cpf, @"[^\d]", "");
        }

        private static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                return false;

            if (!cpf.All(char.IsDigit))
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            var digitos = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            var primeiroDigito = CalcularDigitoVerificador(digitos, 10);
            if (digitos[9] != primeiroDigito)
                return false;

            var segundoDigito = CalcularDigitoVerificador(digitos, 11);
            if (digitos[10] != segundoDigito)
                return false;

            return true;
        }

        private static int CalcularDigitoVerificador(int[] digitos, int multiplicadorInicial)
        {
            int soma = 0;
            for (int i = 0; i < multiplicadorInicial - 1; i++)
            {
                soma += digitos[i] * (multiplicadorInicial - i);
            }

            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}
