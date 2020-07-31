using MarcelMelzig.WindowPositioner.Abstraction.Interfaces;
using System;

namespace MarcelMelzig.WindowPositioner.Core.WindowManagement
{
    /// <summary>
    /// Implementation of a <see cref="IWindow"/>.
    /// </summary>
    public class Window : IWindow
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="title">
        /// The window's title.
        /// </param>
        /// <param name="x">
        /// The window's x coordinate of the upper left point.
        /// </param>
        /// <param name="y">
        /// The window's y coordinate of the upper left point.
        /// </param>
        /// <param name="height">
        /// The window's height.
        /// </param>
        /// <param name="width">
        /// The window's width.
        /// </param>
        /// <param name="windowHandle">
        /// The window's handle used by Windows.
        /// </param>
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

        /// <summary>
        /// The window's height.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// The window's title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The window's width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The window's handle used by Windows.
        /// </summary>
        public IntPtr WindowHandle { get; }

        /// <summary>
        /// The window's x coordinate of the upper left point.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// The window's y coordinate of the upper left point.
        /// </summary>
        public int Y { get; }

        #endregion Properties
    }
}