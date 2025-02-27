using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System.Windows;

namespace GlyphMemoryLeak.Glyphs
{
    internal class UserControlGlyphFactory : IGlyphFactory
    {
        public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
        {
            return new MemoryLeakingUserControl();
            
        }
    }
}
