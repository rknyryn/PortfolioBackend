using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Application.Utilities.Extensions;

public static partial class StringExtensions
{
    #region Methods

    public static string RemoveAccents(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        { return text; }

        text = text.Normalize(NormalizationForm.FormD);
        char[] chars = text
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
            != UnicodeCategory.NonSpacingMark).ToArray();

        return new string(chars).Normalize(NormalizationForm.FormC);
    }

    public static string Slugify(this string phrase)
    {
        phrase += string.Concat("-", Guid.NewGuid().ToString().Split('-').FirstOrDefault().AsSpan(0, 3));

        // Remove all accents and make the string lower case.
        string output = phrase.ToLower();
        output = output
            .Replace('ı', 'i')
            .Replace('ö', 'o')
            .Replace('ü', 'u')
            .Replace('ş', 's')
            .Replace('ç', 'c')
            .Replace('ğ', 'g');


        output = output.RemoveAccents();
        // Remove all special characters from the string.
        output = SpecialCharacters().Replace(output, "");

        // Remove all additional spaces in favour of just one.
        output = AdditionalSpaces().Replace(output, " ").Trim();

        // Replace all spaces with the hyphen.
        output = AllSpaces().Replace(output, "-");

        // Return the slug.
        return output;
    }

    [GeneratedRegex("[^A-Za-z0-9\\s-]")]
    private static partial Regex SpecialCharacters();
    [GeneratedRegex("\\s+")]
    private static partial Regex AdditionalSpaces();
    [GeneratedRegex("\\s")]
    private static partial Regex AllSpaces();

    #endregion Methods
}
