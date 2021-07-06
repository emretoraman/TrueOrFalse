using Caliburn.Micro;
using TrueOrFalse.Models;
using TrueOrFalse.ViewModels;

namespace TrueOrFalse.Tests.UnitTests.ViewModels
{
    public class MainViewModelBuilder
    {
        private readonly IPersistence _persistence;
        private IDialogService _dialogService;
        private IWindowManager _windowManager;
        private ViewModelFactory _viewModelFactory;

        public MainViewModelBuilder(IPersistence persistence)
        {
            _persistence = persistence;
        }

        public MainViewModelBuilder WithDialogService(IDialogService dialogService)
        {
            _dialogService = dialogService;
            return this;
        }

        public MainViewModelBuilder WithWindowManager(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            return this;
        }

        public MainViewModelBuilder WithViewModelFactory(ViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            return this;
        }

        public MainViewModel Build()
        {
            return new(_persistence, _dialogService, _windowManager, _viewModelFactory);
        }
    }
}
