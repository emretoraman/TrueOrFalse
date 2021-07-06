using Caliburn.Micro;
using System.Collections.Generic;
using TrueOrFalse.Models;

namespace TrueOrFalse.ViewModels
{
    public class ViewModelFactory
    {
        public virtual GameViewModel CreateGameViewModel(List<Statement> statements)
        {
            return new GameViewModel(IoC.Get<IDialogService>(), statements);
        }
    }
}
