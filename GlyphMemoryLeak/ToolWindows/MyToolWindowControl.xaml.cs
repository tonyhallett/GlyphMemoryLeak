using Microsoft.Diagnostics.Runtime;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GlyphMemoryLeak
{
    public partial class MyToolWindowControl : UserControl
    {
        public MyToolWindowControl()
        {
            InitializeComponent();
            cbAutomationPeer.IsChecked = MemoryLeakingUserControl.HasAutomationPeer;
            cbAutomationPeer.Checked += CbAutomationPeer_Checked;
            cbAutomationPeer.Unchecked += CbAutomationPeer_Unchecked;
            
        }

        private void CbAutomationPeer_Unchecked(object sender, RoutedEventArgs e)
        {
            MemoryLeakingUserControl.HasAutomationPeer = false;
        }

        private void CbAutomationPeer_Checked(object sender, RoutedEventArgs e)
        {
            MemoryLeakingUserControl.HasAutomationPeer = true;
        }

        private void GCCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }



        private string GetMemoryLeakInfo()
        {
            GCCollect();

            try
            {
                var pid = Process.GetCurrentProcess().Id;
                var numMemoryLeakingUserControls = 0;
                using (DataTarget dt = DataTarget.CreateSnapshotAndAttach(pid))
                {
                    ClrRuntime runtime = dt.ClrVersions[0].CreateRuntime();
                    var heapObjects = runtime.Heap.EnumerateObjects();
                    var memoryLeakingUserControls = heapObjects.Where(clrObject => clrObject.Type?.Name == typeof(MemoryLeakingUserControl).FullName).ToList();
                    numMemoryLeakingUserControls = memoryLeakingUserControls.Count;
                }
                
                return $"Num UserControls: {numMemoryLeakingUserControls}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void BtnShowStats_Click(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            
            btnShowStats.IsEnabled = false;
            var message = await Task.Factory.StartNew(
                GetMemoryLeakInfo, 
                System.Threading.CancellationToken.None,
                TaskCreationOptions.None, TaskScheduler.Default
            );

            var msgBox = new Community.VisualStudio.Toolkit.MessageBox();
            _ = msgBox.ShowAsync(message); ;
            
           
            btnShowStats.IsEnabled = true;

        }

       
    }
}