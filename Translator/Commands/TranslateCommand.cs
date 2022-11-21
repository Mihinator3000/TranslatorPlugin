using System.Collections.Generic;
using System.Linq;
using LibreTranslate.Net;
using Microsoft.VisualStudio.Text;
using Translator.Models;
using Translator.Tools;

namespace Translator.Commands;

[Command(PackageIds.Translate)]
internal sealed class TranslateCommand : BaseCommand<TranslateCommand>
{
    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        var documentView = await VS.Documents.GetActiveDocumentViewAsync();

        var selection = documentView?.TextView?.Selection;
        SnapshotSpan? selectedSpan = selection?.SelectedSpans.FirstOrDefault();
        if (!selectedSpan.HasValue)
            return;

        var textToTranslate = selectedSpan.Value.GetText();

        var (sourceLanguageCode, targetLanguageCode) = ParseLanguageCodes(e.InValue.ToString());

        var translatedText = await LibreTranslator.TranslateAsync(
            sourceLanguageCode,
            targetLanguageCode,
            textToTranslate);

        documentView.TextBuffer?.Replace(selectedSpan.Value, translatedText);
    }

    private static (LanguageCode, LanguageCode) ParseLanguageCodes(string arguments)
    {
        string[] splitArguments = arguments.Split();
        string sourceLanguage = splitArguments[0];
        string targetLanguage = splitArguments[1];

        IReadOnlyCollection<Language> supportedLanguages = LibreTranslator.SupportedLanguages;
        var sourceLanguageCode = supportedLanguages.First(l => l.Name == sourceLanguage).Code;
        var targetLanguageCode = supportedLanguages.First(l => l.Name == targetLanguage).Code;
        return (sourceLanguageCode, targetLanguageCode);
    }
}