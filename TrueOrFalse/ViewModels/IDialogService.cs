using System.Threading.Tasks;

namespace TrueOrFalse.ViewModels
{
    public interface IDialogService
    {
        DialogResult SaveFileDialog();
        DialogResult OpenFileDialog();
        Task OpenInfoWindow(string caption, string text);
    }
}
