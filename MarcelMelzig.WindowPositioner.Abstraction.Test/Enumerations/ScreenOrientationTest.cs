using Xunit;

namespace MarcelMelzig.WindowPositioner.Abstraction.Enumerations.Test
{
    public class ScreenOrientationTest
    {
        #region Tests

        [Fact]
        public void ScreenOrientationValuesAreLikeExpected()
        {
            Assert.Equal(0, (int)ScreenOrientation.Landscape);
            Assert.Equal(1, (int)ScreenOrientation.Portrait);
            Assert.Equal("Landscape", ScreenOrientation.Landscape.ToString());
            Assert.Equal("Portrait", ScreenOrientation.Portrait.ToString());
        }

        #endregion Tests
    }
}