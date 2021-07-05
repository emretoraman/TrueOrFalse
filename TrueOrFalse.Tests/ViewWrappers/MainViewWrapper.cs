using FlaUI.Core.AutomationElements;
using System.Linq;
using TrueOrFalse.Models;

namespace TrueOrFalse.Tests.ViewWrappers
{
    public class MainViewWrapper
    {
        private readonly Window _window;

        public MainViewWrapper(Window window)
        {
            _window = window;
        }

        public Window GetGameWindow()
        {
            //todo:
            var x = _window.ModalWindows;
            return _window.ModalWindows.First();
        }

        public void StartGame()
        {
            _window.FindFirstDescendant(cf => cf.ByName("StartGame")).As<MenuItem>().Click();
        }

        public void Cut()
        {
            _window.FindFirstDescendant(cf => cf.ByName("Cut")).As<MenuItem>().Click();
        }

        public Statement GetStatement()
        {
            string text = GetStatementTextTextBox().Text;
            bool isTrue = GetStatementIsTrueCheckBox().IsChecked == true;
            return new Statement(text, isTrue);
        }

        public void SetStatement(Statement statement)
        {
            SetStatementText(statement.Text);
            SetStatementIsTrue(statement.IsTrue);
        }

        public void AddStatement(Statement statement)
        {
            SetStatement(statement);
            ClickAddStatementButton();
        }

        public void RemoveStatement()
        {
            _window.FindFirstDescendant(cf => cf.ByName("RemoveStatement")).As<Button>().Click();
        }

        public void SaveStatement()
        {
            _window.FindFirstDescendant(cf => cf.ByName("SaveStatement")).As<Button>().Click();
        }

        public void PreviousStatement()
        {
            _window.FindFirstDescendant(cf => cf.ByName("PART_DecreaseButton")).As<Button>().Click();
        }

        public void NextStatement()
        {
            _window.FindFirstDescendant(cf => cf.ByName("PART_IncreaseButton")).As<Button>().Click();
        }

        private int GetCurrentStatementNumber()
        {
            TextBox textBox = _window.FindFirstDescendant(cf => cf.ByName("PART_TextBox")).As<TextBox>();
            return int.Parse(textBox.Text);
        }

        private TextBox GetStatementTextTextBox()
        {
            return _window.FindFirstDescendant("StatementText").As<TextBox>();
        }

        private CheckBox GetStatementIsTrueCheckBox()
        {
            return _window.FindFirstDescendant("StatementIsTrue").As<CheckBox>();
        }

        private void SetStatementText(string text)
        {
            GetStatementTextTextBox().Text = text;
        }

        private void SetStatementIsTrue(bool isTrue)
        {
            GetStatementIsTrueCheckBox().IsChecked = isTrue;
        }

        private void ClickAddStatementButton()
        {
            _window.FindFirstDescendant(cf => cf.ByName("AddStatement")).As<Button>().Click();
        }
    }
}
