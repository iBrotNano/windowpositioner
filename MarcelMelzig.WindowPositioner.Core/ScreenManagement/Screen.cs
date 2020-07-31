using MarcelMelzig.WindowPositioner.Abstraction.Enumerations;
using MarcelMelzig.WindowPositioner.Abstraction.Interfaces;

namespace MarcelMelzig.WindowPositioner.Core.ScreenManagement
{
    public class Screen : IScreen
    {
        #region Constructors

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

        public int Height { get; }

        public bool IsPrimary { get; }
        public ScreenOrientation Orientation { get; }

        public int Width { get; }

        public int X { get; }

        public int Y { get; }

        #endregion Properties
    }
}