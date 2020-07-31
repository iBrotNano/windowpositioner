using MarcelMelzig.WindowPositioner.Core.ScreenManagement;
using MarcelMelzig.WindowPositioner.Core.WindowManagement;

namespace MarcelMelzig.WindowPositioner.Cmd
{
    /// <summary>
    /// The main class of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The starting method of the application.
        /// </summary>
        private static void Main()
        {
            var screenManager = new ScreenManager(new WindowManager());

            foreach (var screen in screenManager.GetAll())
                screenManager.SetWindowsPositionsOnScreen(screen);
        }
    }
}