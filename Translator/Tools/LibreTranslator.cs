using System.Collections.Generic;
using System.Threading.Tasks;
using LibreTranslate.Net;
using Translator.Models;
using LibTranslator = LibreTranslate.Net.LibreTranslate;

namespace Translator.Tools;

public static class LibreTranslator
{
    private static readonly LibTranslator Translator = new("http://localhost:5000");

    public static IReadOnlyCollection<Language> SupportedLanguages { get; } = new Language[]
    {
        new(LanguageCode.English, "English"),
        new(LanguageCode.Russian, "Russian"),
        new(LanguageCode.Arabic, "Arabic"),
        new(LanguageCode.Japanese, "Japanese"),
        new(LanguageCode.French, "French"),
        new(LanguageCode.German, "German"),
        new(LanguageCode.Spanish, "Spanish")
    };

    public static async Task<string> TranslateAsync(
        LanguageCode sourceLanguageCode,
        LanguageCode targetLanguageCode,
        string text)
    {
        var translateRequest = new Translate
        {
            Source = sourceLanguageCode,
            Target = targetLanguageCode,
            Text = text
        };

        return await Translator.TranslateAsync(translateRequest);
    }
}