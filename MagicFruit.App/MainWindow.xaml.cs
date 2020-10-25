using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MagicFruit.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new GameViewModel();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(((Process) ((ComboBox) sender).SelectedItem).MainWindowTitle);
        }

        private void GameInstances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gameInstances = sender as ComboBox;
            (DataContext as GameViewModel).SelectInstance((gameInstances.SelectedItem as Process));
            gameInstances.IsEnabled = false;
        }
    }
}
