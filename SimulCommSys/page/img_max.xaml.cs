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

namespace SimulCommSys
{
    /// <summary>
    /// img.xaml 的交互逻辑
    /// </summary>
    public partial class img_max : Page
    {
        public img_max()
        {
            InitializeComponent();


            this.img_all.DataContext = globalvariabel.aforge_Usbcamera;
        }
    }
}
