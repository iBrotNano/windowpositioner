using MarcelMelzig.WindowPositioner.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace MarcelMelzig.WindowPositioner.Core.WindowManagement
{
    public class WindowManager : IWindowManager
    {
        #region Fields

        private const int SW_SHOWMAXIMIZED = 3;

        #endregion Fields

        #region Methods

        public IEnumerable<IWindow> GetAll()
        {
            var windowProcesses = Process.GetProcesses()
                .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle));

            var windows = new List<IWindow>();

            foreach (var windowProcess in windowProcesses)
            {
                var handle = windowProcess.MainWindowHandle;
                var rect = new Rect();
                GetWindowRect(handle, ref rect);

                windows.Add(new Window(windowProcess.MainWindowTitle,
                    rect.UpperLeftX,
                    rect.UpperLeftY,
                    rect.LowerRightY - rect.UpperLeftY,
                    rect.LowerRightX - rect.UpperLeftX,
                    handle));
            }

            return windows;
        }

        public void MaximizeWindow(IWindow window)
        {
            ShowWindow(window.WindowHandle, SW_SHOWMAXIMIZED);
        }

        public bool Move(IWindow window,
                    int x,
            int y,
            int width,
            int height,
            bool repaint = true)
        {
            return MoveWindow(window.WindowHandle,
                x,
                y,
                width,
                height,
                repaint);
        }

        #endregion Methods

        #region DLLImports

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion DLLImports
    }
}