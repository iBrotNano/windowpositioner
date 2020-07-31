using MarcelMelzig.WindowPositioner.Abstraction.Interfaces;
using System;

namespace MarcelMelzig.WindowPositioner.Core.WindowManagement
{
    public class Window : IWindow
    {
        #region Constructors

        public Window(string title,
            int x,
            int y,
            int height,
            int width,
            IntPtr windowHandle)
        {
            Title = title
                ?? throw new ArgumentNullException(nameof(title));

            Height = height;
            Width = width;
            WindowHandle = windowHandle;
            X = x;
            Y = y;
        }

        #endregion Constructors

        #region Properties

        public int Height { get; }

        public string Title { get; }

        public int Width { get; }

        public IntPtr WindowHandle { get; }

        public int X { get; }

        public int Y { get; }

        #endregion Properties
    }
}