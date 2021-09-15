using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QueroBilhete.Infra.Utilities.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string RemoveAcentos(this string valor)
        {
            return (valor == null) ? valor : new string(valor.Normalize(NormalizationForm.FormD).Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark).ToArray()).ToUpper();
        }

        public static string ApenasNumeros(this string valor)
        {
            return (valor == null) ? valor : new Regex(@"[^0-9a]").Replace(valor, "").ToString();
        }

        public static DateTime AjustaData(this DateTime valor)
        {
            return (valor == null) ? valor : Convert.ToDateTime(valor.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public static string EncodeBase64(this string value)
        {
            return (value == null) ? value : Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        public static string DecodeBase64(this string value)
        {
            return (value == null) ? value : Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
    }
}
