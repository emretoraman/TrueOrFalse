using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace TrueOrFalse.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private object _currentViewModel;

        public ShellViewModel()
        {
            CurrentViewModel = IoC.Get<MainViewModel>();
        }

        public object CurrentViewModel
        {
            get => _currentViewModel;
            private set 
            {
                _currentViewModel = value;
                NotifyOfPropertyChange();
            }
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await ActivateItemAsync(CurrentViewModel, cancellationToken);
            DisplayName = "TrueOrFalse";
        }
    }
}
