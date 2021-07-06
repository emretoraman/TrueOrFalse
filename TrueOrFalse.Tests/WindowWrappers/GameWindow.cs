using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System.Linq;

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

        public string GetResult()
        {
            Window modalWindow = _window.ModalWindows.First(w => w.Name == "Result");
            Label label = modalWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text)).As<Label>();
            return label.Text;
        }

    }
}
