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
using VaughnApp.ViewModels;

namespace VaughnApp.Views
{
    /// <summary>
    /// Interaction logic for Arduino.xaml
    /// </summary>
    public partial class Arduino : UserControl
    {
        public Arduino()
        {
            InitializeComponent();

            Unloaded += Arduino_Unloaded;
        }

        private void Arduino_Unloaded(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ArduinoViewModel;
            vm?.CleanUp();
        }
    }
}
