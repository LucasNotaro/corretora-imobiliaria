namespace CorretoraImobiliaria.Models
{
    public abstract class Imovel
    {
        protected int id;
        protected string endereco = string.Empty;
        protected int numero;
        protected bool alugado;
        protected Proprietario proprietario = null!;
        public int Id 
        { 
            get => id; 
            private set => id = value; 
        }

        public string Endereco 
        { 
            get => endereco; 
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Endereço inválido.");
                endereco = value.Trim();
            }
        }

        public int Numero 
        { 
            get => numero; 
            set 
            {
                if (value <= 0)
                    throw new ArgumentException("Número inválido.");
                numero = value;
            }
        }

        public bool Alugado 
        { 
            get => alugado; 
            private set => alugado = value; 
        }

        public Proprietario Proprietario 
        { 
            get => proprietario; 
            private set => proprietario = value ?? throw new ArgumentNullException(nameof(value)); 
        }

        protected Imovel(int id, string endereco, int numero, Proprietario proprietario)
        {
            Id = id;
            Endereco = endereco;
            Numero = numero;
            Proprietario = proprietario;
            Alugado = false;
        }

        public void Alugar()
        {
            if (Alugado) throw new InvalidOperationException("Imóvel já está alugado.");
            Alugado = true;
        }

        public void Disponibilizar()
        {
            if (!Alugado) throw new InvalidOperationException("Imóvel já está disponível.");
            Alugado = false;
        }

        public abstract decimal CalcularAluguel(int dias);

        public virtual string ObterStatusAluguel()
        {
            return Alugado ? "Alugado" : "Disponível";
        }

    }
}
