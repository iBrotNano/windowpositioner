using MarcelMelzig.WindowPositioner.Abstraction.Enumerations;
using MarcelMelzig.WindowPositioner.Abstraction.Interfaces;

namespace MarcelMelzig.WindowPositioner.Core.ScreenManagement
{
    /// <summary>
    /// Implementation of <see cref="IScreen"/> to represent a screen.
    /// </summary>
    public class Screen : IScreen
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x">
        /// The x coordinate of the left upper point of the screen.
        /// </param>
        /// <param name="y">
        /// The y coordinate of the left upper point of the screen.
        /// </param>
        /// <param name="height">
        /// Screen height
        /// </param>
        /// <param name="width">
        /// Screen width
        /// </param>
        /// <param name="orientation">
        /// The orientation of the screen.
        /// </param>
        /// <param name="isPrimary">
        /// <c>true</c>, if the screen is the primary screen.
        /// </param>
        public Screen(int x,
            int y,
            int height,
            int width,
            ScreenOrientation orientation,
            bool isPrimary)
        {
            Height = height;
            Orientation = orientation;
            Width = width;
            X = x;
            Y = y;
            IsPrimary = isPrimary;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Screen height
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// <c>true</c>, if the screen is the primary screen.
        /// </summary>
        public bool IsPrimary { get; }

        /// <summary>
        /// The orientation of the screen.
        /// </summary>
        public ScreenOrientation Orientation { get; }

        /// <summary>
        /// Screen width
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The x coordinate of the left upper point of the screen.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// The y coordinate of the left upper point of the screen.
        /// </summary>
        public int Y { get; }

        #endregion Properties
    }
}