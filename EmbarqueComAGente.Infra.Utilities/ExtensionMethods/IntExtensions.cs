namespace QueroBilhete.Infra.Utilities.ExtensionMethods
{
    public static class IntExtensions
    {
        public static bool IsNumeric(this string valor)
        {
            return int.TryParse(valor, out _);
        }
    }
}
