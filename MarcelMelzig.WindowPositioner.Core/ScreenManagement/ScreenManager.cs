using MarcelMelzig.WindowPositioner.Abstraction.Enumerations;
using MarcelMelzig.WindowPositioner.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarcelMelzig.WindowPositioner.Core.ScreenManagement
{
    /// <summary>
    /// Implementation of <see cref="IScreenManager"/> to manage <see cref="IScreen"/> s.
    /// </summary>
    public class ScreenManager : IScreenManager
    {
        #region Fields

        /// <summary>
        /// A <see cref="IWindowManager"/>, for the <see cref="IWindow"/> management.
        /// </summary>
        private readonly IWindowManager _windowManager;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="windowManager">
        /// <see cref="IWindowManager"/> for <see cref="IWindow"/> management.
        /// </param>
        public ScreenManager(IWindowManager windowManager)
        {
            _windowManager = windowManager
                ?? throw new ArgumentNullException(nameof(windowManager));
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Return all screens.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> with <see cref="IScreen"/> as T.
        /// </returns>
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

        /// <summary>
        /// Sets the position of the <see cref="IWindow"/> s on the screen.
        /// </summary>
        /// <param name="screen">
        /// A <see cref="IScreen"/>
        /// </param>
        public void SetWindowsPositionsOnScreen(IScreen screen)
        {
            if (screen is null)
                throw new ArgumentNullException(nameof(screen));

            var screenWindows = GetWindowsOnScreen(screen, _windowManager);

            if (screen.Orientation == ScreenOrientation.Landscape)
            {
                if (screen.IsPrimary)
                    SetFullScreen(_windowManager, screenWindows);
                else
                    SetColumnsOnLandscapeScreens(screen, _windowManager, screenWindows);
            }

            if (screen.Orientation == ScreenOrientation.Portrait)
            {
                if (screen.IsPrimary)
                    SetFullScreen(_windowManager, screenWindows);
                else
                    SetRowsOnPortraitScreens(screen, _windowManager, screenWindows);
            }
        }

        /// <summary>
        /// Stacks the <see cref="IScreen"/> instance's <see cref="IWindow"/> instances in columns
        /// on landscape screens.
        /// </summary>
        /// <param name="screen">
        /// The <see cref="IScreen"/>, which <see cref="IWindow"/> instances are positioned.
        /// </param>
        /// <param name="windowManager">
        /// A <see cref="IWindowManager"/> to manage the <see cref="IWindow"/> instances.
        /// </param>
        /// <param name="screenWindows">
        /// The <see cref="IWindow"/> instances on the <see cref="IScreen"/>.
        /// </param>
        private static void SetColumnsOnLandscapeScreens(IScreen screen,
            IWindowManager windowManager,
            IEnumerable<IWindow> screenWindows)
        {
            if (screenWindows.Count() == 0)
                return;

            int widthPerWindow = screen.Width / screenWindows.Count();
            int startX = screen.X;

            foreach (var window in screenWindows)
            {
                windowManager.Move(window, startX, screen.Y, widthPerWindow, screen.Height);
                startX += widthPerWindow;
            }
        }

        /// <summary>
        /// Sets the given <see cref="IWindow"/> instances to full screen mode.
        /// </summary>
        /// <param name="windowManager">
        /// A <see cref="IWindowManager"/> to manage the <see cref="IWindow"/> instances.
        /// </param>
        /// <param name="screenWindows">
        /// The <see cref="IWindow"/> instances on the <see cref="IScreen"/>.
        /// </param>
        private static void SetFullScreen(IWindowManager windowManager,
            IEnumerable<IWindow> screenWindows)
        {
            foreach (var window in screenWindows)
                windowManager.MaximizeWindow(window);
        }

        /// <summary>
        /// Stacks the <see cref="IScreen"/> instance's <see cref="IWindow"/> instances in rows on
        /// portrait screens.
        /// </summary>
        /// <param name="screen">
        /// The <see cref="IScreen"/>, which <see cref="IWindow"/> instances are positioned.
        /// </param>
        /// <param name="windowManager">
        /// A <see cref="IWindowManager"/> to manage the <see cref="IWindow"/> instances.
        /// </param>
        /// <param name="screenWindows">
        /// The <see cref="IWindow"/> instances on the <see cref="IScreen"/>.
        /// </param>
        private static void SetRowsOnPortraitScreens(IScreen screen,
            IWindowManager windowManager,
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

        /// <summary>
        /// Returns all <see cref="IWindow"/> instances on the given <see cref="IScreen"/>.
        /// </summary>
        /// <param name="screen">
        /// The <see cref="IScreen"/>, which <see cref="IWindow"/> instances should be returned.
        /// </param>
        /// <param name="windowManager">
        /// A <see cref="IWindowManager"/> to manage the <see cref="IWindow"/> instances.
        /// </param>
        /// <returns>
        /// All <see cref="IWindow"/> instances on the given <see cref="IScreen"/>.
        /// </returns>
        private IEnumerable<IWindow> GetWindowsOnScreen(IScreen screen,
            IWindowManager windowManager)
        {
            return windowManager
                .GetAll()
                .Where(w => IsWindowOnScreen(screen, w));
        }

        /// <summary>
        /// Return <c>true</c> if the given <see cref="IWindow"/> is on the <see cref="IScreen"/>.
        /// </summary>
        /// <param name="screen">
        /// A <see cref="IScreen"/>.
        /// </param>
        /// <param name="window">
        /// A <see cref="IWindow"/>.
        /// </param>
        /// <returns>
        /// <c>true</c>, if the given <see cref="IWindow"/> is on the <see cref="IScreen"/>.
        /// </returns>
        private bool IsWindowOnScreen(IScreen screen,
            IWindow window)
        {
            return window.X >= screen.X
                && window.X < screen.X + screen.Width - 8;
        }

        #endregion Methods
    }
}