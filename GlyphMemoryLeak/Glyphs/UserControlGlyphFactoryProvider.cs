using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace GlyphMemoryLeak.Glyphs
{
    [ContentType("CSharp")]
    [TagType(typeof(UserControlTag))]
    [Export(typeof(IGlyphFactoryProvider))]
    [Order(Before = "VsTextMarker")]
    [Name("MemoryLeak.UserControlGlyphFactoryProvider")]
    internal class UserControlGlyphFactoryProvider : IGlyphFactoryProvider
    {
        public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin)
        {
            return new UserControlGlyphFactory();
        }
    }
}
