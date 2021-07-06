using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace TrueOrFalse.Tests.WindowWrappers
{
    public class ResultWindow
    {
        private readonly Window _window;

        public ResultWindow(Window window)
        {
            _window = window;
        }

        public string GetResult()
        {
            Label label = _window.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text)).As<Label>();
            return label.Text;
        }
    }
}
