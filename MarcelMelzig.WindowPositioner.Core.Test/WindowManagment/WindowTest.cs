using MarcelMelzig.WindowPositioner.Core.WindowManagement;
using System;
using Xunit;

namespace MarcelMelzig.WindowPositioner.Core.Test.WindowManagment
{
    public class WindowTest
    {
        #region Tests

        [Fact]
        public void WindowSetsProperties()
        {
            var pointer = new IntPtr();
            var window = new Window("Test", 100, 100, 200, 400, pointer);
            Assert.Equal("Test", window.Title);
            Assert.Equal(100, window.X);
            Assert.Equal(100, window.Y);
            Assert.Equal(200, window.Height);
            Assert.Equal(400, window.Width);
            Assert.Equal(pointer, window.WindowHandle);
        }

        [Fact]
        public void WindowThrowsArgumentNullExceptionIftitleIsNull()
        {
            Assert.Throws<ArgumentNullException>("title"
                , () => new Window(null, 0, 0, 100, 100, new IntPtr()));
        }

        #endregion Tests
    }
}