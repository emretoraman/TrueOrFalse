using FlaUI.Core.AutomationElements;

namespace TrueOrFalse.Tests.WindowWrappers
{
    public class GameWindow
    {
        private readonly Window _window;

        public GameWindow(Window window)
        {
            _window = window;
        }

        public void False()
        {
            _window.FindFirstDescendant("False").As<Button>().Click();
        }

        public void True()
        {
            _window.FindFirstDescendant("True").As<Button>().Click();
        }
    }
}
