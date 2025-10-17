using CorretoraImobiliaria.Utils;

namespace CorretoraImobiliaria.Models
{
    public class Proprietario
    {
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string CPF { get; private set; }

        public Proprietario(string nome, string telefone, string cpf)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido.");
            if (string.IsNullOrWhiteSpace(telefone)) throw new ArgumentException("Telefone inválido.");
            if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException("CPF inválido.");

            if (!TelefoneValidator.ValidarTelefone(telefone))
                throw new ArgumentException("Telefone inválido. Verifique os dígitos informados.");
            if (!CpfValidator.ValidarCpf(cpf))
                throw new ArgumentException("CPF inválido. Verifique os dígitos informados.");

            Nome = nome.Trim();
            Telefone = TelefoneValidator.FormatarTelefone(telefone);
            CPF = CpfValidator.FormatarCpf(cpf);
        }

        public string ContatoProprietario()
        {
            return $"Nome: {Nome} | Telefone: {Telefone} | CPF: {CPF}";
        }
    }
}
