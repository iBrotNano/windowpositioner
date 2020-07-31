using MarcelMelzig.WindowPositioner.Abstraction.Enumerations;
using MarcelMelzig.WindowPositioner.Abstraction.Interfaces;
using MarcelMelzig.WindowPositioner.Core.WindowManagement;
using System.Collections.Generic;
using System.Linq;

namespace MarcelMelzig.WindowPositioner.Core.ScreenManagement
{
    public class ScreenManager : IScreenManager
    {
        #region Methods

        public IEnumerable<IScreen> GetAll()
        {
            var screens = new List<IScreen>();

            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                if (screen.Bounds.Height > screen.Bounds.Width)
                    screens.Add(new Screen(screen.WorkingArea.X,
                        screen.WorkingArea.Y,
                        screen.WorkingArea.Height,
                        screen.WorkingArea.Width,
                        ScreenOrientation.Portrait,
                        screen.Primary));
                else

                    screens.Add(new Screen(screen.WorkingArea.X,
                        screen.WorkingArea.Y,
                        screen.WorkingArea.Height,
                        screen.WorkingArea.Width,
                        ScreenOrientation.Landscape,
                        screen.Primary));
            }

            return screens;
        }

        public void SetWindowsPositionsOnScreen(IScreen screen)
        {
            var windowManager = new WindowManager();
            var screenWindows = GetWindowsOnScreen(screen, windowManager);

            if (screen.Orientation == ScreenOrientation.Landscape)
            {
                if (screen.IsPrimary)
                    SetFullScreen(windowManager, screenWindows);
                else
                    SetColumnsOnLandscapeScreens(screen, windowManager, screenWindows);
            }

            if (screen.Orientation == ScreenOrientation.Portrait)
            {
                if (screen.IsPrimary)
                    SetFullScreen(windowManager, screenWindows);
                else
                    SetRowsOnPortraitScreens(screen, windowManager, screenWindows);
            }
        }

        private static void SetColumnsOnLandscapeScreens(IScreen screen,
            WindowManager windowManager,
            IEnumerable<IWindow> screenWindows)
        {
            int widthPerWindow = screen.Width / screenWindows.Count();
            int startX = screen.X;

            foreach (var window in screenWindows)
            {
                windowManager.Move(window, startX, screen.Y, widthPerWindow, screen.Height);
                startX += widthPerWindow;
            }
        }

        private static void SetFullScreen(WindowManager windowManager,
            IEnumerable<IWindow> screenWindows)
        {
            foreach (var window in screenWindows)
                windowManager.MaximizeWindow(window);
        }

        private static void SetRowsOnPortraitScreens(IScreen screen,
            WindowManager windowManager,
            IEnumerable<IWindow> screenWindows)
        {
            int heightPerWindow = screen.Height / screenWindows.Count();
            int startY = screen.Y;

            foreach (var window in screenWindows)
            {
                windowManager.Move(window, screen.X, startY, screen.Width, heightPerWindow);
                startY += heightPerWindow;
            }
        }

        private IEnumerable<IWindow> GetWindowsOnScreen(IScreen screen,
            WindowManager windowManager)
        {
            return windowManager
                .GetAll()
                .Where(w => IsWindowOnScreen(screen, w));
        }

        private bool IsWindowOnScreen(IScreen screen,
            IWindow window)
        {
            return window.X >= screen.X
                && window.X < screen.X + screen.Width - 8;
        }

        #endregion Methods
    }
}