using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueOrFalse.Models;
using TrueOrFalse.ViewModels;
using Xunit;

namespace TrueOrFalse.Tests.UnitTests.ViewModels
{
    public class GameViewModelTests
    {
        private readonly List<Statement> _statements;
        private readonly TaskCompletionSource _resultDialogTcs;
        private readonly Mock<IDialogService> _mockDialogService;
        private readonly GameViewModel _viewModel;

        public GameViewModelTests()
        {
            _statements = new List<Statement>
            {
                new Statement("1 equals one", true),
                new Statement("2 equals two ", true),
                new Statement("3 equals zero", false),
                new Statement("4 equals four", true),
                new Statement("5 equals zero", false)
            };

            _resultDialogTcs = new TaskCompletionSource();
            _mockDialogService = new Mock<IDialogService>();
            _mockDialogService.Setup(s => s.OpenInfoWindow(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_resultDialogTcs.Task);

            _viewModel = new GameViewModel(_mockDialogService.Object, _statements);
        }

        [Fact]
        public void Constructor_Always_RunsCorrectly()
        {
            Assert.Equal(_statements[0].Text, _viewModel.StatementText);
            Assert.Equal(_statements.Count, _viewModel.NumberOfStatements);
            Assert.Equal(1, _viewModel.StatementNumber);
            Assert.Equal(0, _viewModel.Score);
        }

        [Fact]
        public void FalseTrue_WhenCorrect_RunsCorrectly()
        {
            int i = 1;
            foreach (Statement statement in _statements)
            {
                Assert.Equal(statement.Text, _viewModel.StatementText);
                Assert.Equal(i, _viewModel.StatementNumber);

                if (statement.IsTrue)
                {
                    _viewModel.True();
                }
                else
                {
                    _viewModel.False();
                }

                Assert.Equal(i, _viewModel.Score);
                i++;
            }

            _mockDialogService.Verify(s => s.OpenInfoWindow("Result", GameResult.Win.ToString()), Times.Once);
            _resultDialogTcs.SetResult();

            Assert.Equal(_statements[0].Text, _viewModel.StatementText);
            Assert.Equal(_statements.Count, _viewModel.NumberOfStatements);
            Assert.Equal(1, _viewModel.StatementNumber);
            Assert.Equal(0, _viewModel.Score);
        }

        [Fact]
        public void FalseTrue_WhenIncorrect_RunsCorrectly()
        {
            int i = 1;
            foreach (Statement statement in _statements)
            {
                Assert.Equal(statement.Text, _viewModel.StatementText);
                Assert.Equal(i, _viewModel.StatementNumber);

                if (statement.IsTrue)
                {
                    _viewModel.False();
                }
                else
                {
                    _viewModel.True();
                }

                Assert.Equal(0, _viewModel.Score);
                i++;
            }

            _mockDialogService.Verify(s => s.OpenInfoWindow("Result", GameResult.Loss.ToString()), Times.Once);
            _resultDialogTcs.SetResult();

            Assert.Equal(_statements[0].Text, _viewModel.StatementText);
            Assert.Equal(_statements.Count, _viewModel.NumberOfStatements);
            Assert.Equal(1, _viewModel.StatementNumber);
            Assert.Equal(0, _viewModel.Score);
        }
    }
}
