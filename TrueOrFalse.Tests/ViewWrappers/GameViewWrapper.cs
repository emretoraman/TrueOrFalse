using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System.Linq;

namespace TrueOrFalse.Tests.ViewWrappers
{
    public class GameViewWrapper
    {
        private readonly Window _window;

        public GameViewWrapper(Window window)
        {
            _window = window;
        }

        public void False()
        {
            _window.FindFirstDescendant(cf => cf.ByName("False")).As<Button>().Click();
        }

        public void True()
        {
            _window.FindFirstDescendant(cf => cf.ByName("True")).As<Button>().Click();
        }

        public string GetResult()
        {
            //todo:
            var x = _window.ModalWindows;
            Window modalWindow = _window.ModalWindows.First();
            Label label = modalWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text)).As<Label>();
            return label.Text;
        }

    }
}
