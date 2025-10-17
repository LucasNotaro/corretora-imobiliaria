namespace CorretoraImobiliaria.Utils
{
    public static class CpfValidator
    {
        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = cpf.Replace(".", "").Replace("-", "").Replace(" ", "");

            return cpf.Length == 11;
        }

        public static string FormatarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return "";

            cpf = cpf.Replace(".", "").Replace("-", "").Replace(" ", "");

            if (cpf.Length != 11)
                return cpf;

            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        }
    }
}
