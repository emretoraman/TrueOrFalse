using TechTalk.SpecFlow;

namespace TrueOrFalse.Tests.AcceptanceTests
{
    [Binding]
    public class PlayingSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public PlayingSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I added five statements")]
        public void GivenIAddedFiveStatements()
        {
            _scenarioContext.Pending();
        }

        [When(@"I give five answers right")]
        public void WhenIGiveFiveAnswersRight()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I win the game")]
        public void ThenIWinTheGame()
        {
            _scenarioContext.Pending();
        }
    }
}
