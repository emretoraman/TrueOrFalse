using TechTalk.SpecFlow;

namespace TrueOrFalse.Tests.AcceptanceTests
{
    [Binding]
    public class EditingSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public EditingSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have five statements")]
        public void GivenIHaveFiveStatements()
        {
            _scenarioContext.Pending();
        }

        [Given(@"I add one statement")]
        public void GivenIAddOneStatement()
        {
            _scenarioContext.Pending();
        }

        [Given(@"I added one statement")]
        public void GivenIAddedOneStatement()
        {
            _scenarioContext.Pending();
        }

        [Given(@"current statement is not empty")]
        public void GivenCurrentStatementIsNotEmpty()
        {
            _scenarioContext.Pending();
        }

        [Given(@"I added two statements")]
        public void GivenIAddedTwoStatements()
        {
            _scenarioContext.Pending();
        }

        [When(@"I edit both text and statement's flag")]
        public void WhenIEditBothTextAndStatementSFlag()
        {
            _scenarioContext.Pending();
        }

        [When(@"I save the editings")]
        public void WhenSaveTheEditings()
        {
            _scenarioContext.Pending();
        }

        [When(@"I remove one of them")]
        public void WhenIRemoveOneOfThem()
        {
            _scenarioContext.Pending();
        }

        [When(@"I cut the statement's text")]
        public void WhenICutTheStatementSText()
        {
            _scenarioContext.Pending();
        }

        [Then(@"it gets saved and I can get back to it")]
        public void ThenItGetsSavedAndICanGetBackToIt()
        {
            _scenarioContext.Pending();
        }

        [Then(@"it gets saved")]
        public void ThenItGetsSaved()
        {
            _scenarioContext.Pending();
        }

        [Then(@"only one statement remains in the list")]
        public void ThenOnlyOneStatementRemainsInTheList()
        {
            _scenarioContext.Pending();
        }

        [Then(@"it gets removed from the UI and saved into clipboard")]
        public void ThenItGetsRemovedFromTheUIAndSavedIntoClipboard()
        {
            _scenarioContext.Pending();
        }
    }
}
