namespace CorretoraImobiliaria.Utils
{
    public static class TelefoneValidator
    {
        public static bool ValidarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return false;

            telefone = telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            return telefone.Length == 11 && telefone.All(char.IsDigit);
        }

        public static string FormatarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return "";

            telefone = telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            if (telefone.Length != 11 || !telefone.All(char.IsDigit))
                return telefone;

            return $"({telefone.Substring(0, 2)}) {telefone.Substring(2, 5)}-{telefone.Substring(7, 4)}";
        }
    }
}
