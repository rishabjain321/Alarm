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

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model _model;
        public MainWindow()
        {
            _model = new Model();
            InitializeComponent();
            this.DataContext = _model;
        }

        private void SetTime_Click(object sender, RoutedEventArgs e)
        {
            _model.ValidTime(false);

        }

        private void SetAlarm_Click(object sender, RoutedEventArgs e)
        {
            _model.SetAlarm();

        }

        private void NowTime_Click(object sender, RoutedEventArgs e)
        {
            _model.NowTime();
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
            _model.Initialize();
        }
    }
}
