using FlaUI.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using TrueOrFalse.Tests.WindowWrappers;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace TrueOrFalse.Tests.AcceptanceTests
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeScenario]
        public static void BeforeScenario()
        {
            KillRunningApplication();

            string path = Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "TrueOrFalse.exe"
            ));
            Application application = Application.Launch(path);
            Windows.SetApplication(application);
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            KillRunningApplication();
        }

        private static void KillRunningApplication()
        {
            Process process = Process.GetProcessesByName("TrueOrFalse").FirstOrDefault();
            if (process == null) return;

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }
        }
    }
}
