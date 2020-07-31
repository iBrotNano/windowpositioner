using MarcelMelzig.WindowPositioner.Abstraction.Enumerations;

namespace MarcelMelzig.WindowPositioner.Abstraction.Interfaces
{
    public interface IScreen
    {
        #region Properties

        int Height { get; }
        bool IsPrimary { get; }
        ScreenOrientation Orientation { get; }

        int Width { get; }
        int X { get; }

        int Y { get; }

        #endregion Properties
    }
}