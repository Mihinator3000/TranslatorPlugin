using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Translator.ToolWindows;

public class TranslatorWindow : BaseToolWindow<TranslatorWindow>
{
    [Guid("fef13c72-83b8-4a0f-a296-ae402a3d74d9")]
    internal class Pane : ToolWindowPane
    {
        public Pane()
        {
            BitmapImageMoniker = KnownMonikers.ToolWindow;
        }
    }

    public override Type PaneType => typeof(Pane);

    public override string GetTitle(int toolWindowId)
        => "Translator Window";

    public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        =>Task.FromResult<FrameworkElement>(new TranslatorWindowControl());
}