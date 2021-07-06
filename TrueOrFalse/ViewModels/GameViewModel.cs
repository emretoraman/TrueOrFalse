using Caliburn.Micro;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrueOrFalse.Models;

namespace TrueOrFalse.ViewModels
{
    public class GameViewModel : Screen
    {
        private readonly IDialogService _dialogService;
        private readonly List<Statement> _statements;
        private int _statementNumber;
        private int _score;

        public GameViewModel(IDialogService dialogService, List<Statement> statements)
        {
            _dialogService = dialogService;
            _statements = statements;

            StatementNumber = 1;
            Score = 0;
        }

        public string StatementText => CurrentStatement.Text;

        public int NumberOfStatements => _statements.Count;

        public int StatementNumber 
        {
            get => _statementNumber;
            private set
            {
                _statementNumber = value;

                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => StatementText);
            }
        }

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;

                NotifyOfPropertyChange();
            }
        }

        private Statement CurrentStatement => _statements[StatementNumber - 1];

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            DisplayName = "Game";
            return base.OnActivateAsync(cancellationToken);
        }

        public void False()
        {
            _ = ProcessAnswer(false);
        }

        public void True()
        {
            _ = ProcessAnswer(true);
        }

        private async Task ProcessAnswer(bool answer)
        {
            if (answer == CurrentStatement.IsTrue)
            {
                Score++;
            }

            if (StatementNumber == NumberOfStatements)
            {
                await _dialogService.OpenInfoWindow("Result", GetResult().ToString());
                StatementNumber = 1;
                Score = 0;
            }
            else
            {
                StatementNumber++;
            }
        }

        private GameResult GetResult()
        {
            double score = (double)Score * 100 / NumberOfStatements;
            return score >= 70 ? GameResult.Win : GameResult.Loss;
        }
    }

    public enum GameResult
    {
        Win,
        Loss
    }
}
