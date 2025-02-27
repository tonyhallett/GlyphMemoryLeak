using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using System.Collections.Generic;

namespace GlyphMemoryLeak.Glyphs
{
    internal class UserControlTagger : ITagger<UserControlTag>
    {
        public UserControlTagger(ITextView view, ITextBuffer sourceBuffer)
        {
            TagsChanged?.Invoke(this, new SnapshotSpanEventArgs(new SnapshotSpan(sourceBuffer.CurrentSnapshot, 0, sourceBuffer.CurrentSnapshot.Length)));
        }
        public IEnumerable<ITagSpan<UserControlTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            // for every line create tag
            foreach (SnapshotSpan span in spans)
            {
                ITextSnapshotLine line = span.Start.GetContainingLine();
                yield return new TagSpan<UserControlTag>(line.Extent, new UserControlTag());
            }
        }
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
    }
}
