using MarcelMelzig.WindowPositioner.Core.ScreenManagement;

namespace MarcelMelzig.WindowPositioner.Cmd
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var screenManager = new ScreenManager();

            foreach (var screen in screenManager.GetAll())
                screenManager.SetWindowsPositionsOnScreen(screen);
        }
    }
}