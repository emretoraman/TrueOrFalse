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

        public GameViewModel(List<Statement> statements, IDialogService dialogService)
        {
            _statements = statements;
            _dialogService = dialogService;

            ShowNext();
        }

        public string StatementText { get; set; }

        public int StatementNumber { get; set; }

        public int NumberOfStatements { get; set; }
        
        public int Score { get; set; }

        public Statement CurrentStatement => _statements[StatementNumber - 1];

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            DisplayName = "Game";

            return base.OnActivateAsync(cancellationToken);
        }

        public static void Exit()
        {
        }

        public static GameResult GetResult(int scores, int numberOfStatements)
        {
            double result = (double)scores * 100 / numberOfStatements;
            return result > 70 ? GameResult.Win : GameResult.Loss;
        }

        public void False()
        {
            ProcessAnswer(false);
        }

        public void True()
        {
            ProcessAnswer(true);
        }

        public bool EndOfGame()
        {
            return StatementNumber == _statements.Count;
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
                GameResult result = GetResult(Score, NumberOfStatements);
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
