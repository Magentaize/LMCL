using System.Windows;

namespace Magentaize.Net.LMCL.Helper
{
    public static class CustomMessageBox
    {
        public static void ShowError(string str)
        {
            MessageBox.Show(str, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowInfo(string str)
        {
            MessageBox.Show(str, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
