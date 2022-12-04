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
using MahApps.Metro.Controls;
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

            Loaded += Arduino_Loaded;

            
        }

        private void Arduino_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
                window.Closing += (s, args) =>
                {
                    var vm = DataContext as ArduinoViewModel;
                    vm?.CleanUp();
                };
        }
    }
}
