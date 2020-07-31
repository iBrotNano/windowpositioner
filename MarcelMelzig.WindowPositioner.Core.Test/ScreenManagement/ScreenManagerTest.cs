using MarcelMelzig.WindowPositioner.Abstraction.Enumerations;
using MarcelMelzig.WindowPositioner.Abstraction.Interfaces;
using MarcelMelzig.WindowPositioner.Core.ScreenManagement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MarcelMelzig.WindowPositioner.Core.Test.ScreenManagement
{
    public class ScreenManagerTest
    {
        #region Tests

        [Fact]
        public void GetAllReturnsAllScreens()
        {
            var screens = System.Windows.Forms.Screen.AllScreens;
            var windowManager = Mock.Of<IWindowManager>();
            var screenManager = new ScreenManager(windowManager);
            var managedScreens = screenManager.GetAll();
            Assert.Equal(screens.Count(), managedScreens.Count());
            Assert.Equal(screens.First().WorkingArea.X, managedScreens.First().X);
            Assert.Equal(screens.First().WorkingArea.Y, managedScreens.First().Y);
            Assert.Equal(screens.First().WorkingArea.Height, managedScreens.First().Height);
            Assert.Equal(screens.First().WorkingArea.Width, managedScreens.First().Width);
            Assert.Equal(screens.First().Primary, managedScreens.First().IsPrimary);

            if (screens.First().Bounds.Height > screens.First().Bounds.Width)
                Assert.Equal(ScreenOrientation.Portrait, managedScreens.First().Orientation);
            else
                Assert.Equal(ScreenOrientation.Landscape, managedScreens.First().Orientation);
        }

        [Fact]
        public void ScreenManagerThrowsArgumentNullExceptionIfwindowManagerIsNull()
        {
            Assert.Throws<ArgumentNullException>("windowManager"
                , () => new ScreenManager(null));
        }

        [Fact]
        public void SetWindowsPositionsOnScreenIngnoresWindowsOnOtherScreens()
        {
            var window = Mock.Of<IWindow>(w =>
                w.X == -1
                && w.Y == -1);

            var windowManager = Mock.Of<IWindowManager>(wm =>
                wm.GetAll() == new List<IWindow>
                {
                    window
                });

            var serviceManager = new ScreenManager(windowManager);

            var screen = Mock.Of<IScreen>(s =>
                s.X == 0
                && s.Y == 0);

            serviceManager.SetWindowsPositionsOnScreen(screen);
            Assert.Equal(-1, window.X);
            Assert.Equal(-1, window.Y);
        }

        [Fact]
        public void SetWindowsPositionsOnScreenSetsColumnsOnLandscapeNonPrimaryScreens()
        {
            var window1 = Mock.Of<IWindow>(w =>
                w.X == 100
                && w.Y == 100);

            var window2 = Mock.Of<IWindow>(w =>
                w.X == 200
                && w.Y == 200);

            var windowManager = Mock.Of<IWindowManager>(wm =>
                wm.GetAll() == new List<IWindow>
                {
                    window1,
                    window2
                });

            var serviceManager = new ScreenManager(windowManager);

            var screen = Mock.Of<IScreen>(s =>
                s.X == 0
                && s.Y == 0
                && s.Height == 1000
                && s.Width == 2000
                && s.Orientation == ScreenOrientation.Landscape
                && s.IsPrimary == false);

            serviceManager.SetWindowsPositionsOnScreen(screen);

            Mock.Get(windowManager)
                .Verify(wm => wm.Move(window1, 0, 0, 1000, 1000, true)
                    , Times.Once());

            Mock.Get(windowManager)
                .Verify(wm => wm.Move(window2, 1000, 0, 1000, 1000, true)
                    , Times.Once());
        }

        [Fact]
        public void SetWindowsPositionsOnScreenSetsFullScreenOnPrimaryLandscapeScreens()
        {
            var window = Mock.Of<IWindow>(w =>
                w.X == 100
                && w.Y == 100);

            var windowManager = Mock.Of<IWindowManager>(wm =>
                wm.GetAll() == new List<IWindow>
                {
                    window
                });

            var serviceManager = new ScreenManager(windowManager);

            var screen = Mock.Of<IScreen>(s =>
                s.X == 0
                && s.Y == 0
                && s.Height == 1024
                && s.Width == 1950
                && s.Orientation == ScreenOrientation.Landscape
                && s.IsPrimary == true);

            serviceManager.SetWindowsPositionsOnScreen(screen);
            Mock.Get(windowManager).Verify(wm => wm.MaximizeWindow(window), Times.Once());
        }

        [Fact]
        public void SetWindowsPositionsOnScreenSetsFullScreenOnPrimaryPortraitScreens()
        {
            var window = Mock.Of<IWindow>(w =>
                w.X == 100
                && w.Y == 100);

            var windowManager = Mock.Of<IWindowManager>(wm =>
                wm.GetAll() == new List<IWindow>
                {
                    window
                });

            var serviceManager = new ScreenManager(windowManager);

            var screen = Mock.Of<IScreen>(s =>
                s.X == 0
                && s.Y == 0
                && s.Height == 1024
                && s.Width == 1950
                && s.Orientation == ScreenOrientation.Portrait
                && s.IsPrimary == true);

            serviceManager.SetWindowsPositionsOnScreen(screen);
            Mock.Get(windowManager).Verify(wm => wm.MaximizeWindow(window), Times.Once());
        }

        [Fact]
        public void SetWindowsPositionsOnScreenSetsRowsOnPortraitNonPrimaryScreens()
        {
            var window1 = Mock.Of<IWindow>(w =>
                w.X == 100
                && w.Y == 100);

            var window2 = Mock.Of<IWindow>(w =>
                w.X == 200
                && w.Y == 200);

            var windowManager = Mock.Of<IWindowManager>(wm =>
                wm.GetAll() == new List<IWindow>
                {
                    window1,
                    window2
                });

            var serviceManager = new ScreenManager(windowManager);

            var screen = Mock.Of<IScreen>(s =>
                s.X == 0
                && s.Y == 0
                && s.Height == 2000
                && s.Width == 1000
                && s.Orientation == ScreenOrientation.Portrait
                && s.IsPrimary == false);

            serviceManager.SetWindowsPositionsOnScreen(screen);

            Mock.Get(windowManager)
                .Verify(wm => wm.Move(window1, 0, 0, 1000, 1000, true)
                    , Times.Once());

            Mock.Get(windowManager)
                .Verify(wm => wm.Move(window2, 0, 1000, 1000, 1000, true)
                    , Times.Once());
        }

        [Fact]
        public void SetWindowsPositionsOnScreenThrowsArgumentNullExceptionIfscreenIsNull()
        {
            var windowManager = Mock.Of<IWindowManager>();
            var screenManager = new ScreenManager(windowManager);

            Assert.Throws<ArgumentNullException>("screen"
                , () => screenManager.SetWindowsPositionsOnScreen(null));
        }

        #endregion Tests
    }
}