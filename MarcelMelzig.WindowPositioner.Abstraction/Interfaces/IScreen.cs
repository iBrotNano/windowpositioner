using MarcelMelzig.WindowPositioner.Abstraction.Enumerations;

namespace MarcelMelzig.WindowPositioner.Abstraction.Interfaces
{
    /// <summary>
    /// Interface for screens.
    /// </summary>
    public interface IScreen
    {
        #region Properties

        /// <summary>
        /// Screen height
        /// </summary>
        int Height { get; }

        /// <summary>
        /// <c>true</c>, if the screen is the primary screen.
        /// </summary>
        bool IsPrimary { get; }

        /// <summary>
        /// The orientation of the screen.
        /// </summary>
        ScreenOrientation Orientation { get; }

        /// <summary>
        /// Screen width
        /// </summary>
        int Width { get; }

        /// <summary>
        /// The x coordinate of the left upper point of the screen.
        /// </summary>
        int X { get; }

        /// <summary>
        /// The y coordinate of the left upper point of the screen.
        /// </summary>
        int Y { get; }

        #endregion Properties
    }
}