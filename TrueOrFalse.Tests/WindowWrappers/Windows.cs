using FlaUI.Core;
using FlaUI.UIA3;

namespace TrueOrFalse.Tests.WindowWrappers
{
    public class Windows
    {
        private static Application _application;

        public static void SetApplication(Application application)
        {
            _application = application;
        }

        public static MainWindow Main => new(_application.GetMainWindow(new UIA3Automation()));

        public static GameWindow Game => new(Main.GetGameWindow());

        public static ResultWindow Result => new(_application.GetMainWindow(new UIA3Automation()));
    }
}
