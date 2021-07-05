using FlaUI.Core;
using FlaUI.UIA3;

namespace TrueOrFalse.Tests.ViewWrappers
{
    public class ViewWrappers
    {
        private static Application _application;

        public static void SetApplication(Application application)
        {
            _application = application;
        }

        //todo:
        public static MainViewWrapper Main => new(_application.GetMainWindow(new UIA3Automation()));

        public static GameViewWrapper Game => new(Main.GetGameWindow());
    }
}
