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

namespace Alarm_Front_End
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private model _model;

        public MainWindow()
        {
            _model = new model();
            InitializeComponent();
            this.DataContext = _model;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(!_model.initialize())
                Close();

            // create observable collections. 
            SevenSegmentLED.ItemsSource = _model.Segments;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _model.cleanup();
        }
    }
}
