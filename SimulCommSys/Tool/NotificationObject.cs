using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimulCommSys
{
    /// <summary>
    /// 属性变化监视
    /// </summary>
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {

            if (this.PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }


        }

      
        private bool _max;
        public bool max
        {
            get { return this._max; }
            set { this._max = value;
                RaisePropertyChanged("max");
            }

        }

        #region 小车/飞行器
        public static float Car_maxmum = 3;
        public static float Car_minmum = 1;

        public static float Car_increment = 1;
        #endregion


        #region 模型
        public static float Model_maxmum = 3;
        public  static float Model_minmum = 1;

        public  static float Model_increment = 1;
        #endregion


        #region 红外温度

        public static float _Temper = 60.00f;
        public static float _Maxtemper = 300.00f;
        public static float _Mintemper = 50.00f;
        public static float _Increment = 5.0f;
        #endregion


        public static double camertemper = 51f;


        public static String CAMERA_LITTER= "193300025";
    }



}
