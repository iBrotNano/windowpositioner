using MarcelMelzig.WindowPositioner.Core.WindowManagement;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace MarcelMelzig.WindowPositioner.Core.Test.WindowManagment
{
    public class WindowManagerTest
    {
        #region Tests

        [Fact]
        public void GetAllReturnsWindowsIfThereAreSome()
        {
            var processesWithWindow = Process.GetProcesses()
                .Where(wp => !string.IsNullOrEmpty(wp.MainWindowTitle));

            var windowManager = new WindowManager();
            var windows = windowManager.GetAll();
            Assert.Equal(processesWithWindow.Count(), windows.Count());
        }

        [Fact]
        public void MaximizeWindowThrowsArgumentNullExceptionIfwindowIsNull()
        {
            var windowManager = new WindowManager();

            Assert.Throws<ArgumentNullException>("window"
                , () => windowManager.MaximizeWindow(null));
        }

        [Fact]
        public void MoveThrowsArgumentNullExceptionIfwindowIsNull()
        {
            var windowManager = new WindowManager();
            Assert.Throws<ArgumentNullException>("window"
                , () => windowManager.Move(null, 0, 0, 100, 100, true));
        }

        #endregion Tests
    }
}