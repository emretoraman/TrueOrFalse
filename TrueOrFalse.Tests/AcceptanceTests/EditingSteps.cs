using System.Collections.Generic;
using System.Windows;
using TechTalk.SpecFlow;
using TrueOrFalse.Models;
using TrueOrFalse.Tests.WindowWrappers;
using Xunit;

namespace TrueOrFalse.Tests.AcceptanceTests
{
    [Binding]
    [Scope(Feature = "Editing")]
    public class EditingSteps
    {
        private List<Statement> _statements;
        private Statement _savedStatement;

        [Given(@"I have five statements")]
        public void GivenIHaveFiveStatements()
        {
            _statements = new List<Statement>
            {
                new Statement("1 equals one", true),
                new Statement("2 equals two ", true),
                new Statement("3 equals zero", false),
                new Statement("4 equals four", true),
                new Statement("5 equals zero", false)
            };
        }

        [Given(@"I added one statement")]
        public void GivenIAddedOneStatement()
        {
            Windows.Main.AddStatement(_statements[0]);
        }

        [Given(@"current statement is not empty")]
        public static void GivenCurrentStatementIsNotEmpty()
        {
            Windows.Main.SetStatementNumber(1);
        }

        [Given(@"I added two statements")]
        public void GivenIAddedTwoStatements()
        {
            Windows.Main.AddStatement(_statements[0]);
            Windows.Main.AddStatement(_statements[1]);
        }

        [When(@"I add one statement")]
        public void WhenIAddOneStatement()
        {
            Windows.Main.AddStatement(_statements[0]);
        }

        [When(@"I edit both text and statement's flag")]
        public static void WhenIEditBothTextAndStatementSFlag()
        {
            Statement statement = new("6 equals six", true);
            Windows.Main.SetStatement(statement);
        }

        [When(@"I save the editings")]
        public void WhenISaveTheEditings()
        {
            _savedStatement = Windows.Main.GetStatement();
            Windows.Main.SaveStatement();
        }

        [When(@"I remove one of them")]
        public static void WhenIRemoveOneOfThem()
        {
            Windows.Main.SetStatementNumber(1);
            Windows.Main.RemoveStatement();
        }

        [When(@"I cut the statement's text")]
        public static void WhenICutTheStatementSText()
        {
            Windows.Main.Cut();
        }

        [Then(@"it gets saved and I can get back to it")]
        public void ThenItGetsSavedAndICanGetBackToIt()
        {
            Windows.Main.PreviousStatement();
            Statement actual = Windows.Main.GetStatement();
            Statement expected = _statements[0];

            Assert.True(expected.HasEqualValues(actual));
        }

        [Then(@"it gets saved")]
        public void ThenItGetsSaved()
        {
            Windows.Main.PreviousStatement();
            Statement actual = Windows.Main.GetStatement();
            Statement expected = _savedStatement;

            Assert.True(expected.HasEqualValues(actual));
        }

        [Then(@"only one statement remains in the list")]
        public static void ThenOnlyOneStatementRemainsInTheList()
        {
            Assert.Equal(1, Windows.Main.GetNumberOfStatements());
        }

        [Then(@"it gets removed from the UI and saved into clipboard")]
        public void ThenItGetsRemovedFromTheUIAndSavedIntoClipboard()
        {
            string expected = _statements[0].Text;
            Assert.Empty(Windows.Main.GetStatement().Text);
            ThreadHelper.StartStaTask(() => 
            {
                Assert.Equal(expected, Clipboard.GetText());
            }).Wait();
        }
    }
}
