using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Windows;
using TrueOrFalse.ViewModels;

namespace TrueOrFalse.Views
{
    public class DialogService : IDialogService
    {
        public DialogResult SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new();
            bool? result = saveFileDialog.ShowDialog();

            return new DialogResult(result, result == true ? saveFileDialog.FileName : null);
        }

        public DialogResult OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new()
            { 
                InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) 
            };
            bool? result = openFileDialog.ShowDialog();

            return new DialogResult(result, result == true ? openFileDialog.FileName : null);
        }

        public void OpenInfoWindow(string caption, string text)
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
