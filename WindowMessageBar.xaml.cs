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
using System.Windows.Shapes;

namespace Planning_Script_V1
{
    /// <summary>
    /// Interaction logic for WindowMessageBar.xaml
    /// </summary>
    public partial class WindowMessageBar : Window
    {
        public WindowMessageBar()
        {
            InitializeComponent();
        }

        private void PbStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
