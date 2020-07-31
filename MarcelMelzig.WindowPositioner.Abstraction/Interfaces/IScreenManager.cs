using System.Collections.Generic;

namespace MarcelMelzig.WindowPositioner.Abstraction.Interfaces
{
    /// <summary>
    /// Interface for a <see cref="IScreen"/> manager.
    /// </summary>
    public interface IScreenManager
    {
        #region Methods

        /// <summary>
        /// Return all screens.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> with <see cref="IScreen"/> as T.
        /// </returns>
        IEnumerable<IScreen> GetAll();

        /// <summary>
        /// Sets the position of the <see cref="IWindow"/> s on the screen.
        /// </summary>
        /// <param name="screen">
        /// A <see cref="IScreen"/>
        /// </param>
        void SetWindowsPositionsOnScreen(IScreen screen);

        #endregion Methods
    }
}