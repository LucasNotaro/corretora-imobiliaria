namespace CorretoraImobiliaria.Models
{
    public class Apartamento : Imovel
    {
        public decimal ValorAluguelDiario { get; private set; }
        public decimal TaxaCondominio { get; private set; }

        public Apartamento(int id, string endereco, int numero, Proprietario proprietario, decimal valorAluguelDiario, decimal taxaCondominio)
            : base(id, endereco, numero, proprietario)
        {
            if (valorAluguelDiario <= 0) throw new ArgumentException("Valor do aluguel diário inválido.");
            if (taxaCondominio < 0) throw new ArgumentException("Taxa de condomínio inválida.");
            
            ValorAluguelDiario = valorAluguelDiario;
            TaxaCondominio = taxaCondominio;
        }

        public override decimal CalcularAluguel(int dias)
        {
            if (dias <= 0) throw new ArgumentException("Quantidade de dias inválida.");
            decimal valorBase = dias * ValorAluguelDiario;
            decimal valorTaxa = dias * TaxaCondominio;
            return valorBase + valorTaxa;
        }

        public override string ObterStatusAluguel()
        {
            return Alugado 
                ? $"O apartamento nº {Numero} está alugado" 
                : $"O apartamento nº {Numero} está disponível";
        }
    }
}
