using System.Collections.Generic;

namespace MarcelMelzig.WindowPositioner.Abstraction.Interfaces
{
    public interface IScreenManager
    {
        #region Methods

        IEnumerable<IScreen> GetAll();

        void SetWindowsPositionsOnScreen(IScreen screen);

        #endregion Methods
    }
}