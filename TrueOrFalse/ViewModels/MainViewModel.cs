using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TrueOrFalse.Models;

namespace TrueOrFalse.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly IPersistence _persistence;
        private readonly IDialogService _dialogService;
        private readonly IWindowManager _windowManager;
        private int _currentNumber;

        public MainViewModel(IPersistence persistence, IDialogService dialogService, IWindowManager windowManager)
        {
            _persistence = persistence;
            _dialogService = dialogService;
            _windowManager = windowManager;

            CurrentStatement = new Statement();
            CurrentNumber = 1;

            CurrentStatement.PropertyChanged += (sender, args) => UpdateButtonsState();
        }

        public Statement CurrentStatement { get; set; }

        public int CurrentNumber
        {
            get => _currentNumber;
            set
            {
                _currentNumber = value;
                SetCurrentState(GetCurrentIndex());

                NotifyOfPropertyChange();
            }
        }

        public bool CanAddStatement => !IsStatementEmpty && !_persistence.Exists(GetCurrentIndex());

        public bool CanRemoveStatement => _persistence.Exists(GetCurrentIndex());

        public bool CanSaveStatement => !IsStatementEmpty && _persistence.Exists(GetCurrentIndex());

        public bool IsStatementEmpty => string.IsNullOrWhiteSpace(CurrentStatement.Text);

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            DisplayName = "TrueOrFalse";

            return base.OnActivateAsync(cancellationToken);
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public void NewDb()
        {
            DialogResult dialogResult = _dialogService.SaveFileDialog();
            if (dialogResult.Result == true)
            {
                _persistence.FileName = dialogResult.FileName;
                _persistence.Save();

                CurrentNumber = 1;
                CurrentStatement.Text = string.Empty;
                CurrentStatement.IsTrue = false;
            }
        }

        public void OpenDb()
        {
            DialogResult dialogResult = _dialogService.OpenFileDialog();
            if (dialogResult.Result == true)
            {
                _persistence.FileName = dialogResult.FileName;
                _persistence.Load();

                CurrentNumber = 1;
            }
        }

        public void SaveDb()
        {
            _persistence.Save();
        }

        public void SaveDbAs()
        {
            DialogResult dialogResult = _dialogService.SaveFileDialog();
            if (dialogResult.Result == true)
            {
                _persistence.Save();
            }
        }

        public void StartGame()
        {
            _windowManager.ShowDialogAsync(new GameViewModel(_persistence.List, _dialogService));
        }

        public void Cut()
        {
            Clipboard.SetText(CurrentStatement.Text);
            CurrentStatement.Text = string.Empty;
        }

        public void Copy()
        {
            Clipboard.SetText(CurrentStatement.Text);
        }

        public void Paste()
        {
            CurrentStatement.Text = Clipboard.GetText();
        }

        public void AddStatement()
        {
            _persistence.Add(GetCurrentStatementState());
            CurrentNumber++;

            CurrentStatement.Text = string.Empty;
            CurrentStatement.IsTrue = false;

            NotifyOfPropertyChange(() => CurrentStatement);
        }

        public void RemoveStatement()
        {
            if (_persistence.Exists(GetCurrentIndex()))
            {
                _persistence.Remove(GetCurrentIndex());

                CurrentNumber = CurrentNumber > 1 ? CurrentNumber - 1 : 1;
            }
        }

        public void SaveStatement()
        {
            UpdateCurrentStatement();
            CurrentNumber++;
        }

        private Statement GetCurrentStatementState()
        {
            return new Statement(CurrentStatement.Text, CurrentStatement.IsTrue);
        }

        private void UpdateCurrentStatement()
        {
            if (_persistence.Exists(GetCurrentIndex()))
            {
                _persistence.Change(GetCurrentIndex(), GetCurrentStatementState());
            }
        }

        private void SetCurrentState(int index)
        {
            if (_persistence.Exists(index))
            {
                CurrentStatement.Text = _persistence[index].Text;
                CurrentStatement.IsTrue = _persistence[index].IsTrue;
            }
            else
            {
                CurrentStatement.Text = string.Empty;
                CurrentStatement.IsTrue = false;
            }

            UpdateButtonsState();
        }

        private void UpdateButtonsState()
        {
            NotifyOfPropertyChange(nameof(CanSaveStatement));
            NotifyOfPropertyChange(nameof(CanRemoveStatement));
            NotifyOfPropertyChange(nameof(CanAddStatement));
        }

        private int GetCurrentIndex()
        {
            return CurrentNumber == 0 ? 0 : CurrentNumber - 1;
        }

    }
}
