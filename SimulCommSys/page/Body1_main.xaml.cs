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
using static SimulCommSys.globalvariabel;

namespace SimulCommSys
{
    /// <summary>
    /// Body1_main.xaml 的交互逻辑
    /// </summary>
    public partial class Body1_main : Page
    {
        public Body1_main()
        {
            InitializeComponent();

       

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //  globalvariabel.aforge_Usbcamera.StartCamera();



            #region 偏振相机初始化
            //    ArenaManager Arena;

            this.img1_litterlgt.DataContext = globalvariabel.camera_Litter;
            globalvariabel.camera_Litter.StartStream();


            #endregion


        }


        #region 界面放大缩小

        private void Button_bing_Click(object sender, RoutedEventArgs e)
        {
            var colum = 1;
            var row = 1;

            var max_colum = this.Grid_.ColumnDefinitions.Count();
            var max_row = this.Grid_.RowDefinitions.Count();

            if (Grid.GetColumnSpan(this.border_bing) < max_colum)
            {
                Grid.SetRow(this.border_bing, 0);
                Grid.SetColumn(this.border_bing, 0);
                Grid.SetRowSpan(this.border_bing, max_row);
                Grid.SetColumnSpan(this.border_bing, max_colum);
                Panel.SetZIndex(this.border_bing, 2);
            }
            else
            {
                Grid.SetRow(this.border_bing, row);
                Grid.SetColumn(this.border_bing, colum);
                Grid.SetRowSpan(this.border_bing, 1);
                Grid.SetColumnSpan(this.border_bing, 1);
                Panel.SetZIndex(this.border_bing, 1);

            }
        }

        private void Button_litter_Click(object sender, RoutedEventArgs e)
        {
            var colum =0;
            var row = 0;

            var max_colum = this.Grid_.ColumnDefinitions.Count();
            var max_row = this.Grid_.RowDefinitions.Count();
     


            if (Grid.GetColumnSpan(this.border_litter) < max_colum)
            {
                Grid.SetRow(this.border_litter, 0);
                Grid.SetColumn(this.border_litter, 0);
                Grid.SetRowSpan(this.border_litter, max_row);
                Grid.SetColumnSpan(this.border_litter, max_colum);
                Panel.SetZIndex(this.border_litter, 2);
            }
            else
            {
                Grid.SetRow(this.border_litter, row);
                Grid.SetColumn(this.border_litter, colum);
                Grid.SetRowSpan(this.border_litter, 1);
                Grid.SetColumnSpan(this.border_litter, 1);
             
                Panel.SetZIndex(this.border_litter, 1);

                
            }
            Image sf = img1_litterlgt as Image;
            //ScaleTransform st = new ScaleTransform();
            //st.ScaleX = 1;
            //st.ScaleY = 1;
            //sf.RenderTransform = st;
        }

        private void Button_normal_Click(object sender, RoutedEventArgs e)
        {
            var colum = 1;
            var row = 0;
            var max_colum = this.Grid_.ColumnDefinitions.Count();
            var max_row = this.Grid_.RowDefinitions.Count();

          
            if (Grid.GetColumnSpan(this.border_normal) < max_colum)
            {
                Grid.SetRow(this.border_normal, 0);
                Grid.SetColumn(this.border_normal, 0);
                Grid.SetRowSpan(this.border_normal, max_row);
                Grid.SetColumnSpan(this.border_normal, max_colum);
                Panel.SetZIndex(this.border_normal, 2);
            }
            else
            {

                Grid.SetRow(this.border_normal, row);
                Grid.SetColumn(this.border_normal, colum);
                Grid.SetRowSpan(this.border_normal, 1);
                Grid.SetColumnSpan(this.border_normal, 1);
                Panel.SetZIndex(this.border_normal, 1);
              
            }
        }

        private void Button_red_Click(object sender, RoutedEventArgs e)
        {
            var colum = 0;
            var row = 1;
            var max_colum = this.Grid_.ColumnDefinitions.Count();
            var max_row = this.Grid_.RowDefinitions.Count();

         
            if (Grid.GetColumnSpan(this.border_red) < max_colum)
            {
                Grid.SetRow(this.border_red, 0);
                Grid.SetColumn(this.border_red, 0);
                Grid.SetRowSpan(this.border_red, max_row);
                Grid.SetColumnSpan(this.border_red, max_colum);
                Panel.SetZIndex(this.border_red,2);
            }
            else
            {
                Grid.SetRow(this.border_red, row);
                Grid.SetColumn(this.border_red, colum);
                Grid.SetRowSpan(this.border_red, 1);
                Grid.SetColumnSpan(this.border_red, 1);
                Panel.SetZIndex(this.border_red, 1);

            }
        }

        #endregion
        #region img1
        private bool mouseDown1;
        private Point mouseXY1;


        private void IMG1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.CaptureMouse();
            mouseDown1 = true;
            mouseXY1 = e.GetPosition(img);
        }
        private void IMG1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.ReleaseMouseCapture();
            mouseDown1 = false;
        }
        private void IMG1_MouseMove(object sender, MouseEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            if (mouseDown1)
            {
                Domousemove1(img, e);
            }
        }

     


        private void IMG1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            var point = e.GetPosition(img);
            var group = IMG1.FindResource("Imageview") as TransformGroup;
            var delta = e.Delta * 0.001;
            DowheelZoom1(group, point, delta);
        }
        private void DowheelZoom1(TransformGroup group, Point point, double delta)
        {
            var pointToContent = group.Inverse.Transform(point);
            var transform = group.Children[0] as ScaleTransform;
            if (transform.ScaleX + delta < 0.1) 
                return;
            transform.ScaleX += delta;
            transform.ScaleY += delta;
            var transform1 = group.Children[1] as TranslateTransform;
            transform1.X = -1 * ((pointToContent.X * transform.ScaleX) - point.X);
            transform1.Y = -1 * ((pointToContent.Y * transform.ScaleY) - point.Y);
        }
        private void Domousemove1(ContentControl img,  MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;

            }
            var group = IMG1.FindResource("Imageview") as TransformGroup;
            var transform = group.Children[1] as TranslateTransform;
            var position = e.GetPosition(img);
            transform.X -= mouseXY1.X - position.X;
            transform.Y -= mouseXY1.Y - position.Y;
            mouseXY1 = position;
        }


        #endregion
        #region img2
        private bool mouseDown2;
        private Point mouseXY2;


        private void IMG2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.CaptureMouse();
            mouseDown2 = true;
            mouseXY2 = e.GetPosition(img);
        }
        private void IMG2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.ReleaseMouseCapture();
            mouseDown2 = false;
        }
        private void IMG2_MouseMove(object sender, MouseEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            if (mouseDown2)
            {
                Domousemove2(img, e);
            }
        }




        private void IMG2_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            var point = e.GetPosition(img);
            var group = IMG2.FindResource("Imageview") as TransformGroup;
            var delta = e.Delta * 0.001;
            DowheelZoom2(group, point, delta);
        }
        private void DowheelZoom2(TransformGroup group, Point point, double delta)
        {
            var pointToContent = group.Inverse.Transform(point);
            var transform = group.Children[0] as ScaleTransform;
            if (transform.ScaleX + delta < 0.1)
                return;
            transform.ScaleX += delta;
            transform.ScaleY += delta;
            var transform1 = group.Children[1] as TranslateTransform;
            transform1.X = -1 * ((pointToContent.X * transform.ScaleX) - point.X);
            transform1.Y = -1 * ((pointToContent.Y * transform.ScaleY) - point.Y);
        }
        private void Domousemove2(ContentControl img, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;

            }
            var group = IMG2.FindResource("Imageview") as TransformGroup;
            var transform = group.Children[1] as TranslateTransform;
            var position = e.GetPosition(img);
            transform.X -= mouseXY1.X - position.X;
            transform.Y -= mouseXY1.Y - position.Y;
            mouseXY1 = position;
        }
        #endregion

        #region img3
        private bool mouseDown3;
        private Point mouseXY3;


        private void IMG3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.CaptureMouse();
            mouseDown3 = true;
            mouseXY3 = e.GetPosition(img);
        }
        private void IMG3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.ReleaseMouseCapture();
            mouseDown3 = false;
        }
        private void IMG3_MouseMove(object sender, MouseEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            if (mouseDown3)
            {
                Domousemove3(img, e);
            }
        }




        private void IMG3_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            var point = e.GetPosition(img);
            var group = IMG3.FindResource("Imageview") as TransformGroup;
            var delta = e.Delta * 0.001;
            DowheelZoom3(group, point, delta);
        }
        private void DowheelZoom3(TransformGroup group, Point point, double delta)
        {
            var pointToContent = group.Inverse.Transform(point);
            var transform = group.Children[0] as ScaleTransform;
            if (transform.ScaleX + delta < 0.1)
                return;
            transform.ScaleX += delta;
            transform.ScaleY += delta;
            var transform1 = group.Children[1] as TranslateTransform;
            transform1.X = -1 * ((pointToContent.X * transform.ScaleX) - point.X);
            transform1.Y = -1 * ((pointToContent.Y * transform.ScaleY) - point.Y);
        }
        private void Domousemove3(ContentControl img, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;

            }
            var group = IMG3.FindResource("Imageview") as TransformGroup;
            var transform = group.Children[1] as TranslateTransform;
            var position = e.GetPosition(img);
            transform.X -= mouseXY1.X - position.X;
            transform.Y -= mouseXY1.Y - position.Y;
            mouseXY1 = position;
        }
        #endregion

        #region img4
        private bool mouseDown4;
        private Point mouseXY4;


        private void IMG4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.CaptureMouse();
            mouseDown4 = true;
            mouseXY4 = e.GetPosition(img);
        }
        private void IMG4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.ReleaseMouseCapture();
            mouseDown4 = false;
        }
        private void IMG4_MouseMove(object sender, MouseEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            if (mouseDown4)
            {
                Domousemove4(img, e);
            }
        }




        private void IMG4_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            var point = e.GetPosition(img);
            var group = IMG4.FindResource("Imageview") as TransformGroup;
            var delta = e.Delta * 0.001;
            DowheelZoom4(group, point, delta);
        }
        private void DowheelZoom4(TransformGroup group, Point point, double delta)
        {
            var pointToContent = group.Inverse.Transform(point);
            var transform = group.Children[0] as ScaleTransform;
            if (transform.ScaleX + delta < 0.1)
                return;
            transform.ScaleX += delta;
            transform.ScaleY += delta;
            var transform1 = group.Children[1] as TranslateTransform;
            transform1.X = -1 * ((pointToContent.X * transform.ScaleX) - point.X);
            transform1.Y = -1 * ((pointToContent.Y * transform.ScaleY) - point.Y);
        }
        private void Domousemove4(ContentControl img, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;

            }
            var group = IMG4.FindResource("Imageview") as TransformGroup;
            var transform = group.Children[1] as TranslateTransform;
            var position = e.GetPosition(img);
            transform.X -= mouseXY1.X - position.X;
            transform.Y -= mouseXY1.Y - position.Y;
            mouseXY1 = position;
        }
        #endregion
    }
}
