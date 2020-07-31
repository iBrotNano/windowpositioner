using System.Collections.Generic;

namespace MarcelMelzig.WindowPositioner.Abstraction.Interfaces
{
    /// <summary>
    /// Interface for a manager to manage <see cref="IWindow"/> instances.
    /// </summary>
    public interface IWindowManager
    {
        #region Methods

        /// <summary>
        /// Returns all <see cref="IWindow"/> instances.
        /// </summary>
        /// <returns>
        /// </returns>
        IEnumerable<IWindow> GetAll();

        /// <summary>
        /// Sets a window to full screen.
        /// </summary>
        /// <param name="window">
        /// The <see cref="IWindow"/> to maximize.
        /// </param>
        void MaximizeWindow(IWindow window);

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
        bool Move(IWindow window,
                    int x,
            int y,
            int width,
            int height,
            bool repaint = true);

        #endregion Methods
    }
}