using System.Text.RegularExpressions;

namespace VesteTemplate.Shared.Extensions;

public static class SpecialCharacterExtensions
{
    public static string RemoverCaracteresEspeciais(this string valor) => Regex.Replace(valor, "[^0-9a-zA-Z]+", " ");
}
