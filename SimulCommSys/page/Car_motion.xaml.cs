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
    /// Car_motion.xaml 的交互逻辑
    /// </summary>
    public partial class Car_motion : Page
    {
        public Car_motion()
        {
            InitializeComponent();
          
        }
        private void textBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //top 50 
            TextBox rectangle = sender as TextBox;
            Point point = rectangle.TranslatePoint(new Point(), Canvas_model);
            this.textBox.Text = point.X.ToString() + "Y:" + point.Y.ToString();


            Canvas.SetLeft(textBox, 400);
            Canvas.SetTop(textBox, point.Y);
        }

    }
}
