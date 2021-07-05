using System.Collections.Generic;
using TechTalk.SpecFlow;
using TrueOrFalse.Models;
using TrueOrFalse.Tests.WindowWrappers;
using Xunit;

namespace TrueOrFalse.Tests.AcceptanceTests
{
    [Binding]
    [Scope(Feature = "Playing")]
    public class PlayingSteps
    {
        private List<Statement> _statements;

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

        [Given(@"I added five statements")]
        public void GivenIAddedFiveStatements()
        {
            foreach (Statement statement in _statements)
            {
                Windows.Main.AddStatement(statement);
            }
        }

        [When(@"I start game")]
        public static void WhenIStartGame()
        {
            Windows.Main.StartGame();
        }

        [When(@"I give five answers right")]
        public void WhenIGiveFiveAnswersRight()
        {
            foreach (Statement statement in _statements)
            {
                if (statement.IsTrue)
                {
                    Windows.Game.True();
                }
                else
                {
                    Windows.Game.False();
                }
            }
        }

        [Then(@"I win the game")]
        public static void ThenIWinTheGame()
        {
            Assert.Equal("Win", Windows.Game.GetResult());
        }
    }
}
