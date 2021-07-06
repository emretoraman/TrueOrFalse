using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System.Linq;
using TrueOrFalse.Models;

namespace TrueOrFalse.Tests.WindowWrappers
{
    public class MainWindow
    {
        private readonly Window _window;

        public MainWindow(Window window)
        {
            _window = window;
        }

        public Window GetGameWindow()
        {
            return _window.ModalWindows.First(w => w.Name == "Game");
        }

        public void StartGame()
        {
            MenuItem file = _window.FindFirstDescendant(cf => cf.ByName("File")).As<MenuItem>();
            file.Items.First(i=>i.AutomationId == "StartGame").Click();
        }

        public void Cut()
        {
            MenuItem edit = _window.FindFirstDescendant(cf => cf.ByName("Edit")).As<MenuItem>();
            edit.Items.First(i => i.AutomationId == "Cut").Click();
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
            _window.FindFirstDescendant("RemoveStatement").As<Button>().Click();
        }

        public void SaveStatement()
        {
            _window.FindFirstDescendant("SaveStatement").As<Button>().Click();
        }

        public void PreviousStatement()
        {
            _window.FindFirstDescendant("PART_DecreaseButton").As<Button>().Click();
        }

        public void NextStatement()
        {
            _window.FindFirstDescendant("PART_IncreaseButton").As<Button>().Click();
        }

        public void SetStatementNumber(int number)
        {
            GetStamentNumberTextBox().Text = number.ToString();
        }

        public int GetNumberOfStatements()
        {
            SetStatementNumber(1);

            int number = 0;
            while (!string.IsNullOrWhiteSpace(GetStatementTextTextBox().Text))
            {
                NextStatement();
                number++;
            }

            return number;
        }

        //private int GetStatementNumber()
        //{
        //    TextBox textBox = _window.FindFirstDescendant(cf => cf.ByName("PART_TextBox")).As<TextBox>();
        //    return int.Parse(textBox.Text);
        //}

        private TextBox GetStamentNumberTextBox()
        {
            return _window.FindFirstDescendant("PART_TextBox").As<TextBox>();
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
            _window.FindFirstDescendant("AddStatement").As<Button>().Click();
        }
    }
}
