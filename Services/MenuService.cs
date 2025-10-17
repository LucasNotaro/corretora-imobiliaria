using CorretoraImobiliaria.Models;
using CorretoraImobiliaria.Utils;

namespace CorretoraImobiliaria.Services
{
    public class MenuService
    {
        private readonly ImovelService imovelService;

        public MenuService()
        {
            imovelService = new ImovelService();
        }

        public void Executar()
        {
            Console.WriteLine("=== SISTEMA DE CORRETORA IMOBILIÁRIA ===\n");

            bool continuar = true;
            while (continuar)
            {
                ExibirMenu();
                int opcao = LerInteiro("Escolha uma opção: ");

                switch (opcao)
                {
                    case 1: CadastrarImovel(); break;
                    case 2: ListarImoveis(); break;
                    case 3: AlugarImovel(); break;
                    case 4: DisponibilizarImovel(); break;
                    case 5: CalcularValorAluguel(); break;
                    case 6: ExcluirImovel(); break;
                    case 7: continuar = false; Console.WriteLine("Obrigado por usar o sistema!"); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private void ExibirMenu()
        {
            Console.WriteLine("=== MENU PRINCIPAL ===");
            Console.WriteLine("1. Cadastrar imóvel (casa/apartamento)");
            Console.WriteLine("2. Listar imóveis cadastrados");
            Console.WriteLine("3. Alugar imóvel");
            Console.WriteLine("4. Disponibilizar imóvel");
            Console.WriteLine("5. Calcular valor do aluguel por período");
            Console.WriteLine("6. Excluir imóvel");
            Console.WriteLine("7. Sair\n");
        }

        private int LerInteiro(string label)
        {
            Console.Write(label);
            int valor;
            string input;
            while (!int.TryParse(input = Console.ReadLine() ?? "", out valor))
            {
                if (string.IsNullOrEmpty(input))
                {
                    Console.Write("Entrada vazia. Digite um número: ");
                }
                else
                {
                    Console.Write("Entrada inválida. Digite um número: ");
                }
            }
            return valor;
        }

        private decimal LerDecimal(string label)
        {
            Console.Write(label);
            decimal valor;
            string input;
            while (!decimal.TryParse(input = Console.ReadLine() ?? "", out valor))
            {
                if (string.IsNullOrEmpty(input))
                {
                    Console.Write("Entrada vazia. Digite um valor decimal: ");
                }
                else
                {
                    Console.Write("Entrada inválida. Digite um valor decimal: ");
                }
            }
            return valor;
        }

        private string LerTexto(string label)
        {
            Console.Write(label);
            return (Console.ReadLine() ?? "").Trim();
        }

        private string LerCpf(string label)
        {
            Console.Write(label);
            string cpf;
            while (!CpfValidator.ValidarCpf(cpf = Console.ReadLine() ?? ""))
            {
                if (string.IsNullOrEmpty(cpf))
                {
                    Console.Write("CPF vazio. Digite um CPF válido: ");
                }
                else
                {
                    Console.Write("CPF inválido. Digite um CPF válido: ");
                }
            }
            return cpf;
        }

        private string LerTelefone(string label)
        {
            Console.Write(label);
            string telefone;
            while (!TelefoneValidator.ValidarTelefone(telefone = Console.ReadLine() ?? ""))
            {
                if (string.IsNullOrEmpty(telefone))
                {
                    Console.Write("Telefone vazio. Digite um telefone válido: ");
                }
                else
                {
                    Console.Write("Telefone inválido. Digite um telefone válido (11 dígitos): ");
                }
            }
            return telefone;
        }

        private void CadastrarImovel()
        {
            Console.WriteLine("\n=== CADASTRAR IMÓVEL ===");
            Console.WriteLine("1. Casa");
            Console.WriteLine("2. Apartamento");
            int tipo = LerInteiro("Escolha o tipo: ");

            if (tipo < 1 || tipo > 2)
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            string endereco = LerTexto("Endereço: ");
            int numero = LerInteiro("Número: ");

            Console.WriteLine("\n--- Dados do Proprietário ---");
            string nomeProprietario = LerTexto("Nome do proprietário: ");
            string telefoneProprietario = LerTelefone("Telefone do proprietário: ");
            string cpfProprietario = LerCpf("CPF do proprietário: ");

            decimal valorDiario = LerDecimal("Valor do aluguel diário: ");

            var proprietario = new Proprietario(nomeProprietario, telefoneProprietario, cpfProprietario);
            Imovel imovel;

            if (tipo == 1)
            {
                imovel = new Casa(imovelService.ObterProximoId(), endereco, numero, proprietario, valorDiario);
            }
            else
            {
                decimal taxaCondominio = LerDecimal("Taxa de condomínio diária: ");
                imovel = new Apartamento(imovelService.ObterProximoId(), endereco, numero, proprietario, valorDiario, taxaCondominio);
            }

            imovelService.AdicionarImovel(imovel);
            Console.WriteLine("\nImóvel cadastrado com sucesso!");
        }

        private void ListarImoveis()
        {
            Console.WriteLine("\n=== LISTA DE IMÓVEIS ===");
            var imoveis = imovelService.ListarImoveis();
            
            if (imoveis.Count == 0)
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
                return;
            }

            for (int i = 0; i < imoveis.Count; i++)
            {
                var imovel = imoveis[i];
                Console.WriteLine($"\n--- Imóvel {i + 1} ---");
                Console.WriteLine($"ID: {imovel.Id}");
                Console.WriteLine($"Endereço: {imovel.Endereco}");
                Console.WriteLine($"Número: {imovel.Numero}");
                Console.WriteLine($"Status: {imovel.ObterStatusAluguel()}");
                Console.WriteLine($"Proprietário: {imovel.Proprietario.ContatoProprietario()}");
                
                if (imovel is Casa casa)
                {
                    Console.WriteLine($"Tipo: Casa | Valor Diário: R$ {casa.ValorAluguelDiario:F2}");
                }
                else if (imovel is Apartamento apartamento)
                {
                    Console.WriteLine($"Tipo: Apartamento | Valor Diário: R$ {apartamento.ValorAluguelDiario:F2} | Taxa Condomínio: R$ {apartamento.TaxaCondominio:F2}");
                }
            }
        }

        private void AlugarImovel()
        {
            Console.WriteLine("\n=== ALUGAR IMÓVEL ===");
            if (imovelService.ObterQuantidadeImoveis() == 0)
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
                return;
            }

            ListarImoveis();
            int indice = LerInteiro("Digite o número do imóvel: ") - 1;

            try
            {
                imovelService.AlugarImovel(indice);
                Console.WriteLine("Imóvel alugado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alugar imóvel: {ex.Message}");
            }
        }

        private void DisponibilizarImovel()
        {
            Console.WriteLine("\n=== DISPONIBILIZAR IMÓVEL ===");
            if (imovelService.ObterQuantidadeImoveis() == 0)
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
                return;
            }

            ListarImoveis();
            int indice = LerInteiro("Digite o número do imóvel: ") - 1;

            try
            {
                imovelService.DisponibilizarImovel(indice);
                Console.WriteLine("Imóvel disponibilizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao disponibilizar imóvel: {ex.Message}");
            }
        }

        private void CalcularValorAluguel()
        {
            Console.WriteLine("\n=== CALCULAR VALOR DO ALUGUEL ===");
            if (imovelService.ObterQuantidadeImoveis() == 0)
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
                return;
            }

            ListarImoveis();
            int indice = LerInteiro("Digite o número do imóvel: ") - 1;
            int dias = LerInteiro("Número de dias: ");

            try
            {
                decimal valor = imovelService.CalcularValorAluguel(indice, dias);
                Console.WriteLine($"\nValor do aluguel para {dias} dias: R$ {valor:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao calcular aluguel: {ex.Message}");
            }
        }

        private void ExcluirImovel()
        {
            Console.WriteLine("\n=== EXCLUIR IMÓVEL ===");
            if (imovelService.ObterQuantidadeImoveis() == 0)
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
                return;
            }

            ListarImoveis();
            int indice = LerInteiro("Digite o número do imóvel para excluir: ") - 1;

            try
            {
                var removido = imovelService.ExcluirImovel(indice);
                Console.WriteLine("Imóvel excluído com sucesso.");
                Console.WriteLine("Dados do imóvel excluído:");
                Console.WriteLine($"ID: {removido.Id}");
                Console.WriteLine($"Endereço: {removido.Endereco}");
                Console.WriteLine($"Número: {removido.Numero}");
                Console.WriteLine($"Status: {removido.ObterStatusAluguel()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir imóvel: {ex.Message}");
            }
        }
    }
}
