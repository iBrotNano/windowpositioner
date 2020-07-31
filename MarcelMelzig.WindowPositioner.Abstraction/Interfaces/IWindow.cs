using System;

namespace MarcelMelzig.WindowPositioner.Abstraction.Interfaces
{
    /// <summary>
    /// Interface for windows.
    /// </summary>
    public interface IWindow
    {
        #region Properties

        /// <summary>
        /// The window's height.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// The window's title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// The window's width.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// The window's handle used by Windows.
        /// </summary>
        IntPtr WindowHandle { get; }

        /// <summary>
        /// The window's x coordinate of the upper left point.
        /// </summary>
        int X { get; }

        /// <summary>
        /// The window's y coordinate of the upper left point.
        /// </summary>
        int Y { get; }

        #endregion Properties
    }
}