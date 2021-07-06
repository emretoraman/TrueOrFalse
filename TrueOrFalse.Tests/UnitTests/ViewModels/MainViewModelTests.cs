using Caliburn.Micro;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TrueOrFalse.Models;
using TrueOrFalse.ViewModels;
using Xunit;

namespace TrueOrFalse.Tests.UnitTests.ViewModels
{
    public class MainViewModelTests
    {
        [Fact]
        public void Constructor_Always_RunsCorrectly()
        {
            MainViewModel viewModel = new MainViewModelBuilder(new Mock<IPersistence>().Object)
                .Build();

            Assert.True(Statement.Empty.HasEqualValues(viewModel.CurrentStatement));
            Assert.Equal(1, viewModel.CurrentNumber);
        }

        [Theory]
        [InlineData("", false, false)]
        [InlineData("", true, false)]
        [InlineData("Text", false, true)]
        [InlineData("Text", true, false)]
        public void CanAddStatement_Always_ReturnsExpected(string text, bool exists, bool expected)
        {
            Mock<IPersistence> mockPersistence = new();
            mockPersistence.Setup(p => p.Exists(It.IsAny<int>()))
                .Returns(exists);
            mockPersistence.Setup(p => p[It.IsAny<int>()])
                .Returns(new Statement("Text", true));

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .Build();
            viewModel.CurrentStatement.Text = text;

            Assert.Equal(expected, viewModel.CanAddStatement);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void CanRemoveStatement_Always_ReturnsExpected(bool exists, bool expected)
        {
            Mock<IPersistence> mockPersistence = new();
            mockPersistence.Setup(p => p.Exists(It.IsAny<int>()))
                .Returns(exists);
            mockPersistence.Setup(p => p[It.IsAny<int>()])
                .Returns(new Statement("Text", true));

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .Build();

            Assert.Equal(expected, viewModel.CanRemoveStatement);
        }

        [Theory]
        [InlineData("", false, false)]
        [InlineData("", true, false)]
        [InlineData("Text", false, false)]
        [InlineData("Text", true, true)]
        public void CanSaveStatement_Always_ReturnsExpected(string text, bool exists, bool expected)
        {
            Mock<IPersistence> mockPersistence = new();
            mockPersistence.Setup(p => p.Exists(It.IsAny<int>()))
                .Returns(exists);
            mockPersistence.Setup(p => p[It.IsAny<int>()])
                .Returns(new Statement("Text", true));

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .Build();
            viewModel.CurrentStatement.Text = text;

            Assert.Equal(expected, viewModel.CanSaveStatement);
        }

        [Fact]
        public void NewDb_Always_RunsCorrectly()
        {
            Mock<IPersistence> mockPersistence = new();

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .Build();
            viewModel.CurrentNumber = 2;
            viewModel.CurrentStatement.Text = "Text";
            viewModel.NewDb();

            mockPersistence.Verify(p => p.New(), Times.Once);
            Assert.True(Statement.Empty.HasEqualValues(viewModel.CurrentStatement));
            Assert.Equal(1, viewModel.CurrentNumber);
        }

        [Fact]
        public void OpenDb_WhenDialogReturnsTrue_RunsCorrectly()
        {
            string fileName = "db.xml";

            Mock<IPersistence> mockPersistence = new();

            Mock<IDialogService> mockDialogService = new();
            mockDialogService.Setup(s => s.OpenFileDialog())
                .Returns(new DialogResult(true, fileName));

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .WithDialogService(mockDialogService.Object)
                .Build();
            viewModel.CurrentNumber = 2;
            viewModel.CurrentStatement.Text = "Text";
            viewModel.OpenDb();

            mockDialogService.Verify(s => s.OpenFileDialog(), Times.Once);
            mockPersistence.Verify(p => p.Load(fileName), Times.Once);
            Assert.True(Statement.Empty.HasEqualValues(viewModel.CurrentStatement));
            Assert.Equal(1, viewModel.CurrentNumber);
        }

        [Fact]
        public void SaveDb_WhenNew_RunsCorrectly()
        {
            string fileName = "db.xml";

            Mock<IPersistence> mockPersistence = new();
            mockPersistence.Setup(s => s.FileName)
                .Returns((string)null);

            Mock<IDialogService> mockDialogService = new();
            mockDialogService.Setup(s => s.SaveFileDialog())
                .Returns(new DialogResult(true, fileName));

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .WithDialogService(mockDialogService.Object)
                .Build();
            viewModel.SaveDb();

            mockDialogService.Verify(s => s.SaveFileDialog(), Times.Once);
            mockPersistence.Verify(p => p.Save(fileName), Times.Once);
        }

        [Fact]
        public void SaveDb_WhenOverwriting_RunsCorrectly()
        {
            string fileName = "db.xml";

            Mock<IPersistence> mockPersistence = new();
            mockPersistence.Setup(s => s.FileName)
                .Returns(fileName);

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .Build();
            viewModel.SaveDb();

            mockPersistence.Verify(p => p.Save(fileName), Times.Once);
        }

        [Fact]
        public void SaveDbAs_WhenDialogReturnsTrue_RunsCorrectly()
        {
            string fileName = "db.xml";

            Mock<IPersistence> mockPersistence = new();

            Mock<IDialogService> mockDialogService = new();
            mockDialogService.Setup(s => s.SaveFileDialog())
                .Returns(new DialogResult(true, fileName));

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .WithDialogService(mockDialogService.Object)
                .Build();
            viewModel.SaveDbAs();

            mockDialogService.Verify(s => s.SaveFileDialog(), Times.Once);
            mockPersistence.Verify(p => p.Save(fileName), Times.Once);
        }

        [Fact]
        public void StartGame_Always_RunsCorrectly()
        {
            GameViewModel gameViewModel = new(new Mock<IDialogService>().Object, new List<Statement>());

            Mock<IWindowManager> mockWindowManager = new();

            Mock<ViewModelFactory> mockViewModelFactory = new();
            mockViewModelFactory.Setup(b => b.CreateGameViewModel(It.IsAny<List<Statement>>()))
                .Returns(gameViewModel);

            MainViewModel viewModel = new MainViewModelBuilder(new Mock<Persistence>().Object)
                .WithWindowManager(mockWindowManager.Object)
                .WithViewModelFactory(mockViewModelFactory.Object)
                .Build();
            viewModel.StartGame();

            mockWindowManager.Verify(
                m => m.ShowDialogAsync(
                    It.Is<GameViewModel>(vm => vm == gameViewModel),
                    It.IsAny<object>(),
                    It.IsAny<Dictionary<string, object>>()
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task Cut_Always_RunsCorrectly()
        {
            string text = "Test";

            MainViewModel viewModel = new MainViewModelBuilder(new Mock<IPersistence>().Object)
                .Build();
            viewModel.CurrentStatement.Text = text;

            string clipboard = null;
            await ThreadHelper.StartStaTask(() =>
            {
                Clipboard.SetText(string.Empty);
                viewModel.Cut();
                clipboard = Clipboard.GetText();
            });

            Assert.Equal(text, clipboard);
            Assert.Empty(viewModel.CurrentStatement.Text);
        }

        [Fact]
        public async Task Copy_Always_RunsCorrectly()
        {
            string text = "Test";

            MainViewModel viewModel = new MainViewModelBuilder(new Mock<IPersistence>().Object)
                .Build();
            viewModel.CurrentStatement.Text = text;

            string clipboard = null;
            await ThreadHelper.StartStaTask(() =>
            {
                Clipboard.SetText(string.Empty);
                viewModel.Copy();
                clipboard = Clipboard.GetText();
            });

            Assert.Equal(text, clipboard);
        }

        [Fact]
        public async Task Paste_Always_RunsCorrectly()
        {
            string text = "Test";

            MainViewModel viewModel = new MainViewModelBuilder(new Mock<IPersistence>().Object)
                .Build();

            await ThreadHelper.StartStaTask(() =>
            {
                Clipboard.SetText(text);
                viewModel.Paste();
            });

            Assert.Equal(text, viewModel.CurrentStatement.Text);
        }

        [Fact]
        public void AddStatement_Always_RunsCorrectly()
        {
            int count = 1;
            Statement statement = new("Text", true);

            Mock<IPersistence> mockPersistence = new();
            mockPersistence.Setup(p => p.Count)
                .Returns(count);

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .Build();
            viewModel.CurrentStatement.Text = statement.Text;
            viewModel.CurrentStatement.IsTrue = statement.IsTrue;
            viewModel.AddStatement();

            mockPersistence.Verify(p => p.Add(It.Is<Statement>(s => s.HasEqualValues(statement))), Times.Once);
            Assert.Equal(count + 1, viewModel.CurrentNumber);
            Assert.True(Statement.Empty.HasEqualValues(viewModel.CurrentStatement));
        }

        [Fact]
        public void RemoveStatement_Always_RunsCorrectly()
        {
            Mock<IPersistence> mockPersistence = new();

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .Build();
            viewModel.CurrentStatement.Text = "Text";
            viewModel.RemoveStatement();

            mockPersistence.Verify(p => p.Remove(0), Times.Once);
            Assert.True(Statement.Empty.HasEqualValues(viewModel.CurrentStatement));
        }

        [Fact]
        public void SaveStatement_Always_RunsCorrectly()
        {
            Statement statement = new("Text", true);

            Mock<IPersistence> mockPersistence = new();

            MainViewModel viewModel = new MainViewModelBuilder(mockPersistence.Object)
                .Build();
            viewModel.CurrentStatement.Text = statement.Text;
            viewModel.CurrentStatement.IsTrue = statement.IsTrue;
            viewModel.SaveStatement();

            mockPersistence.Verify(p => p.Change(0, It.Is<Statement>(s => s.HasEqualValues(statement))), Times.Once);
            Assert.Equal(2, viewModel.CurrentNumber);
        }
    }
}
