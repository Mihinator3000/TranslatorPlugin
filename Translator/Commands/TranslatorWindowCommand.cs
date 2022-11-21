using Translator.ToolWindows;

namespace Translator.Commands;

[Command(PackageIds.OpenTranslatorWindow)]
internal sealed class TranslatorWindowCommand : BaseCommand<TranslatorWindowCommand>
{
    protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        => TranslatorWindow.ShowAsync();
}