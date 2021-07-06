using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Windows;
using TrueOrFalse.Models;

namespace TrueOrFalse.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly IPersistence _persistence;
        private readonly IDialogService _dialogService;
        private readonly IWindowManager _windowManager;
        private readonly ViewModelFactory _viewModelFactory;
        private int _currentNumber;
        private Statement _currentStatement;

        public MainViewModel(IPersistence persistence,
            IDialogService dialogService,
            IWindowManager windowManager,
            ViewModelFactory viewModelFactory)
        {
            _persistence = persistence;
            _dialogService = dialogService;
            _windowManager = windowManager;
            _viewModelFactory = viewModelFactory;

            CurrentNumber = 1;
        }

        public int MaximumNumber => _persistence.Count + 1;

        public bool CanAddStatement => !_persistence.Exists(CurrentNumber - 1) && !IsStatementEmpty;

        public bool CanRemoveStatement => _persistence.Exists(CurrentNumber - 1);

        public bool CanSaveStatement => !IsStatementEmpty && _persistence.Exists(CurrentNumber - 1);

        public Statement CurrentStatement
        {
            get => _currentStatement;
            private set
            {
                _currentStatement = value;
                _currentStatement.PropertyChanged += CurrentStatement_PropertyChanged;

                NotifyOfPropertyChange();
            }
        }

        public int CurrentNumber
        {
            get => _currentNumber;
            set
            {
                _currentNumber = value;
                SetCurrentStatement();

                NotifyOfPropertyChange();
            }
        }

        private bool IsStatementEmpty => string.IsNullOrWhiteSpace(CurrentStatement.Text);

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public void NewDb()
        {
            _persistence.New();
            CurrentNumber = 1;
        }

        public void OpenDb()
        {
            DialogResult dialogResult = _dialogService.OpenFileDialog();
            if (dialogResult.Result == true)
            {
                _persistence.Load(dialogResult.FileName);
                CurrentNumber = 1;
            }
        }

        public void SaveDb()
        {
            if (string.IsNullOrWhiteSpace(_persistence.FileName))
            {
                SaveDbAs();
            }
            else
            {
                _persistence.Save(_persistence.FileName);
            }
        }

        public void SaveDbAs()
        {
            DialogResult dialogResult = _dialogService.SaveFileDialog();
            if (dialogResult.Result == true)
            {
                _persistence.Save(dialogResult.FileName);
            }
        }

        public void StartGame()
        {
            GameViewModel gameViewModel = _viewModelFactory.CreateGameViewModel(_persistence.List);
            _windowManager.ShowDialogAsync(gameViewModel);
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
            _persistence.Add(CurrentStatement);
            CurrentNumber = _persistence.Count + 1;
        }

        public void RemoveStatement()
        {
            _persistence.Remove(CurrentNumber - 1);
            SetCurrentStatement();
        }

        public void SaveStatement()
        {
            _persistence.Change(CurrentNumber - 1, CurrentStatement);
            CurrentNumber++;
        }

        private void CurrentStatement_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateButtonStatuses();
        }

        private void SetCurrentStatement()
        {
            Statement statement = _persistence.Exists(CurrentNumber - 1)
                ? _persistence[CurrentNumber - 1]
                : Statement.Empty;

            CurrentStatement = new Statement(statement.Text, statement.IsTrue);

            UpdateButtonStatuses();
            NotifyOfPropertyChange(() => MaximumNumber);
        }

        private void UpdateButtonStatuses()
        {
            NotifyOfPropertyChange(() => CanAddStatement);
            NotifyOfPropertyChange(() => CanRemoveStatement);
            NotifyOfPropertyChange(() => CanSaveStatement);
        }
    }
}
