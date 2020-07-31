using MarcelMelzig.WindowPositioner.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace MarcelMelzig.WindowPositioner.Core.WindowManagement
{
    /// <summary>
    /// Implementation of <see cref="IWindowManager"/> to manage <see cref="IWindow"/> instances.
    /// </summary>
    public class WindowManager : IWindowManager
    {
        #region Fields

        /// <summary>
        /// A constant to define the maximized mode used by a Win32 method.
        /// </summary>
        private const int _sW_SHOWMAXIMIZED = 3;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Returns all <see cref="IWindow"/> instances.
        /// </summary>
        /// <returns>
        /// All <see cref="IWindow"/> instances.
        /// </returns>
        public IEnumerable<IWindow> GetAll()
        {
            var windowProcesses = Process.GetProcesses()
                .Where(wp => !string.IsNullOrEmpty(wp.MainWindowTitle));

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

        /// <summary>
        /// Sets a window to full screen.
        /// </summary>
        /// <param name="window">
        /// The <see cref="IWindow"/> to maximize.
        /// </param>
        public void MaximizeWindow(IWindow window)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            ShowWindow(window.WindowHandle, _sW_SHOWMAXIMIZED);
        }

        /// <summary>
        /// Moves and optionally resizes a <see cref="IWindow"/>.
        /// </summary>
        /// <param name="window">
        /// The <see cref="IWindow"/> to be moved.
        /// </param>
        /// <param name="x">
        /// The target x coordinate on the <see cref="IWindow"/> instance's <see cref="IScreen"/>.
        /// </param>
        /// <param name="y">
        /// The target y coordinate on the <see cref="IWindow"/> instance's <see cref="IScreen"/>.
        /// </param>
        /// <param name="width">
        /// The new width of the <see cref="IWindow"/>.
        /// </param>
        /// <param name="height">
        /// The new height of the <see cref="IWindow"/>.
        /// </param>
        /// <param name="repaint">
        /// <c>true</c> to repaint the <see cref="IWindow"/>. The default is <c>true</c>.
        /// </param>
        /// <returns>
        /// </returns>
        public bool Move(IWindow window,
            int x,
            int y,
            int width,
            int height,
            bool repaint = true)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            return MoveWindow(window.WindowHandle,
                x,
                y,
                width,
                height,
                repaint);
        }

        #endregion Methods

        #region DLLImports

        /// <summary>
        /// Win32 method to get the dimensions of a window.
        /// </summary>
        /// <param name="hwnd">
        /// The window's handle.
        /// </param>
        /// <param name="rectangle">
        /// A <see cref="Rect"/> struct to define the dimensions.
        /// </param>
        /// <returns>
        /// <c>true</c> if the method succeeds.
        /// </returns>
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        /// <summary>
        /// Win32 method to move a window.
        /// </summary>
        /// <param name="hWnd">
        /// The window's handler.
        /// </param>
        /// <param name="X">
        /// The new x coordinate on the desktop window.
        /// </param>
        /// <param name="Y">
        /// The new y coordinate on the desktop window.
        /// </param>
        /// <param name="nWidth">
        /// The new width of the window.
        /// </param>
        /// <param name="nHeight">
        /// The new height of the window.
        /// </param>
        /// <param name="bRepaint">
        /// <c>true</c> to repaint the window.
        /// </param>
        /// <returns>
        /// <c>true</c> if the method succeeds.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        /// Shows a window in the given mode.
        /// </summary>
        /// <param name="hWnd">
        /// A window's handler.
        /// </param>
        /// <param name="nCmdShow">
        /// The mode the window should be shown with.
        /// </param>
        /// <returns>
        /// <c>true</c> if the method succeeds.
        /// </returns>
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion DLLImports
    }
}