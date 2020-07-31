using System.Collections.Generic;

namespace MarcelMelzig.WindowPositioner.Abstraction.Interfaces
{
    public interface IWindowManager
    {
        #region Methods

        IEnumerable<IWindow> GetAll();

        void MaximizeWindow(IWindow window);

        bool Move(IWindow window,
                    int x,
            int y,
            int width,
            int height,
            bool repaint = true);

        #endregion Methods
    }
}