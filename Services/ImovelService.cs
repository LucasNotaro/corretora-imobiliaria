using CorretoraImobiliaria.Models;

namespace CorretoraImobiliaria.Services
{
    public class ImovelService
    {
        private List<Imovel> imoveis = new();
        private int proximoId = 1;

        public void AdicionarImovel(Imovel imovel)
        {
            imoveis.Add(imovel);
        }

        public List<Imovel> ListarImoveis()
        {
            return imoveis.ToList();
        }

        public Imovel? BuscarImovelPorIndice(int indice)
        {
            if (indice < 0 || indice >= imoveis.Count)
                return null;
            return imoveis[indice];
        }

        public void AlugarImovel(int indice)
        {
            var imovel = BuscarImovelPorIndice(indice);
            if (imovel == null)
                throw new ArgumentException("Índice inválido.");
            
            if (imovel.Alugado)
                throw new InvalidOperationException("Este imóvel já está alugado.");
            
            imovel.Alugar();
        }

        public void DisponibilizarImovel(int indice)
        {
            var imovel = BuscarImovelPorIndice(indice);
            if (imovel == null)
                throw new ArgumentException("Índice inválido.");
            
            if (!imovel.Alugado)
                throw new InvalidOperationException("Este imóvel já está disponível.");
            
            imovel.Disponibilizar();
        }

        public decimal CalcularValorAluguel(int indice, int dias)
        {
            var imovel = BuscarImovelPorIndice(indice);
            if (imovel == null)
                throw new ArgumentException("Índice inválido.");
            
            return imovel.CalcularAluguel(dias);
        }

        public Imovel ExcluirImovel(int indice)
        {
            var imovel = BuscarImovelPorIndice(indice);
            if (imovel == null)
                throw new ArgumentException("Índice inválido.");
            
            imoveis.RemoveAt(indice);
            return imovel;
        }

        public int ObterProximoId()
        {
            return proximoId++;
        }

        public int ObterQuantidadeImoveis()
        {
            return imoveis.Count;
        }

        public List<Imovel> ObterImoveisDisponiveis()
        {
            return imoveis.Where(i => !i.Alugado).ToList();
        }

        public List<Imovel> ObterImoveisAlugados()
        {
            return imoveis.Where(i => i.Alugado).ToList();
        }
    }
}
