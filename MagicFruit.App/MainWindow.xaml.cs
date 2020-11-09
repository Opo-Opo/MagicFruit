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
        private readonly GameViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new GameViewModel();
        }

        private void GameInstances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ComboBox gameInstances)) return;

            _viewModel.SelectInstance(gameInstances.SelectedItem as Process);

            gameInstances.IsEnabled = false;
        }
    }
}
