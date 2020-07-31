using System;

namespace MarcelMelzig.WindowPositioner.Abstraction.Interfaces
{
    public interface IWindow
    {
        #region Properties

        int Height { get; }
        string Title { get; }
        int Width { get; }
        IntPtr WindowHandle { get; }
        int X { get; }
        int Y { get; }

        #endregion Properties
    }
}