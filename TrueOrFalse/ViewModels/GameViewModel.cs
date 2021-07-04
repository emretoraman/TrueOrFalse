using Caliburn.Micro;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrueOrFalse.Models;

namespace TrueOrFalse.ViewModels
{
    public class GameViewModel : Screen
    {
        private readonly List<Statement> _statements;
        private readonly IDialogService _dialogService;
        private int _statementNumber;
        private string _statementText;
        private int _score;

        public GameViewModel(List<Statement> statements, IDialogService dialogService)
        {
            _statements = statements;
            _dialogService = dialogService;
            NumberOfStatements = _statements.Count;

            ShowNext();
        }

        public int NumberOfStatements { get; }

        public int StatementNumber
        {
            get => _statementNumber;
            set
            {
                _statementNumber = value;
                NotifyOfPropertyChange();
            }
        }

        public string StatementText
        {
            get => _statementText;
            set
            {
                _statementText = value;
                NotifyOfPropertyChange();
            }
        }

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                NotifyOfPropertyChange();
            }
        }

        public Statement CurrentStatement => _statements[StatementNumber - 1];

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            DisplayName = "Game";

            return base.OnActivateAsync(cancellationToken);
        }

        public static void Exit()
        {
        }

        public void True()
        {
            ProcessAnswer(true);
        }

        public void False()
        {
            ProcessAnswer(false);
        }

        public bool EndOfGame()
        {
            return StatementNumber == _statements.Count;
        }

        public GameResult GetResult()
        {
            double result = Score * 100 / NumberOfStatements;
            return result > 70 ? GameResult.Win : GameResult.Loss;
        }

        private void ShowNext()
        {
            StatementNumber++;
            StatementText = _statements[StatementNumber - 1].Text;
        }

        private void ProcessAnswer(bool answer)
        {
            bool isCorrect = answer == CurrentStatement.IsTrue;
            if (isCorrect)
            {
                Score++;
            }

            if (EndOfGame())
            {
                GameResult result = GetResult();
                _dialogService.OpenInfoWindow("Result", result.ToString());

                TryCloseAsync();
            }
            else
            {
                ShowNext();
            }
        }

    }

    public enum GameResult
    {
        Win,
        Loss
    }
}
