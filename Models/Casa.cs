namespace CorretoraImobiliaria.Models
{
    public class Casa : Imovel
    {
        public decimal ValorAluguelDiario { get; private set; }

        public Casa(int id, string endereco, int numero, Proprietario proprietario, decimal valorAluguelDiario)
            : base(id, endereco, numero, proprietario)
        {
            if (valorAluguelDiario <= 0) throw new ArgumentException("Valor do aluguel diário inválido.");
            ValorAluguelDiario = valorAluguelDiario;
        }

        public override decimal CalcularAluguel(int dias)
        {
            if (dias <= 0) throw new ArgumentException("Quantidade de dias inválida.");
            return dias * ValorAluguelDiario;
        }

        public override string ObterStatusAluguel()
        {
            return Alugado ? "A casa está alugada" : "A casa está disponível";
        }
    }
}
