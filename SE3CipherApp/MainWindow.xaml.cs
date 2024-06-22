using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SE3CipherApp.Views;

namespace SE3CipherApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Encrypt();
            OutputTextBox.Text = viewModel.OutputText;
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Decrypt();
            OutputTextBox.Text = viewModel.OutputText;
        }
    }
}
