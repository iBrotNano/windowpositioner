using MarcelMelzig.WindowPositioner.Abstraction.Enumerations;
using MarcelMelzig.WindowPositioner.Core.ScreenManagement;
using Xunit;

namespace MarcelMelzig.WindowPositioner.Core.Test.ScreenManagement
{
    public class ScreenTest
    {
        #region Tests

        [Fact]
        public void ScreenSetsProperties()
        {
            var screen = new Screen(10,
                11,
                400,
                600,
                ScreenOrientation.Landscape,
                false);

            Assert.Equal(10, screen.X);
            Assert.Equal(11, screen.Y);
            Assert.Equal(400, screen.Height);
            Assert.Equal(600, screen.Width);
            Assert.Equal(ScreenOrientation.Landscape, screen.Orientation);
            Assert.False(screen.IsPrimary);
        }

        #endregion Tests
    }
}