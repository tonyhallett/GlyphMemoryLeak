using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace GlyphMemoryLeak.Glyphs
{

    [ContentType("CSharp")]
    [TagType(typeof(UserControlTag))]
    [Name("MemoryLeak.UserControlTaggerProvider")]
    [Export(typeof(IViewTaggerProvider))]

    internal class UserControlTaggerProvider : IViewTaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            return new UserControlTagger(textView, buffer) as ITagger<T>;
        }
    }
}
