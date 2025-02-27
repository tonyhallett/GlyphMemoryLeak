using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace GlyphMemoryLeak
{
    /// <summary>
    /// Interaction logic for MemoryLeakingUserControl.xaml
    /// </summary>
    public partial class MemoryLeakingUserControl : UserControl
    {
        public static bool HasAutomationPeer { get; set; } = true;
        public MemoryLeakingUserControl()
        {
            InitializeComponent();
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            if (HasAutomationPeer)
            {
                return base.OnCreateAutomationPeer();
            }
            return null;
        }
    }
}
