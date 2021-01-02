using System;
using System.CodeDom;
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
using System.IO.Ports;
using MahApps.Metro.Controls;

namespace SimulCommSys
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region 开启串口(旧)
            //try
            //{
            //    globalvariabel.ini_Port.protName = "COM3";
            //    globalvariabel.ini_Port.baudRate = 9600;
            //    globalvariabel.ini_Port.parity = Parity.Even;
            //    globalvariabel.ini_Port.dataBits = 8;
            //    globalvariabel.ini_Port.stopBits = StopBits.One;
            //    globalvariabel.port = new SerialPort(globalvariabel.ini_Port.protName, globalvariabel.ini_Port.baudRate, globalvariabel.ini_Port.parity, globalvariabel.ini_Port.dataBits, globalvariabel.ini_Port.stopBits = StopBits.One);

            //    globalvariabel.port.Open();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("串口连接失败!...."+ex.Message);

            //}
            //#endregion


            //#region 获取开关状态量
            //// globalvariabel.ModbusOperate.GetSwitchSta(1, 1, 0, 1)==true ?:

            #endregion

            #region 数据绑定


            #region 消息绑定
            this.Msg.DataContext = globalvariabel.Record_except;
            #endregion
       
            #endregion

            #region 开启串口
            this.INIValue += new iniValue(globalvariabel.ModbusOperate.Get_iniValue);
            INIValue();
            #endregion
            #region 模型的速度绑定
            globalvariabel.Model_Person.sp_flt_change += Model_Person_sp_flt_change;
            globalvariabel.Model_RocketGUN.sp_flt_change += Model_RocketGUN_sp_flt_change;
            globalvariabel.Model_BoreGUN.sp_flt_change += Model_BoreGUN_sp_flt_change;
            globalvariabel.Model_CarryCar.sp_flt_change += Model_CarryCar_sp_flt_change;
            globalvariabel.Model_Tank.sp_flt_change += Model_Tank_sp_flt_change;
            #endregion
            #region 小车飞行器绑定
            globalvariabel.Car_Fly.sp_flt_change += Car_Fly_sp_flt_change;
            globalvariabel.Car_red.sp_flt_change += Car_red_sp_flt_change;
            globalvariabel.Car_LitterLight.sp_flt_change += Car_LitterLight_sp_flt_change;
            globalvariabel.Car_NormLight.sp_flt_change += Car_NormLight_sp_flt_change;
            #endregion


            #region 红外温度的
            globalvariabel.infrared_mod1.sp_flt_change += Infrared_mod1_sp_flt_change;
            globalvariabel.infrared_mod2.sp_flt_change += Infrared_mod2_sp_flt_change;
            globalvariabel.infrared_mod3.sp_flt_change += Infrared_mod3_sp_flt_change;
            globalvariabel.infrared_mod4.sp_flt_change += Infrared_mod4_sp_flt_change;
            #endregion



           
            this.Closed += MainWindow_Closed;

            //this.img4_bing.DataContext = globalvariabel.aforge_Usbcamera;
            //this.img1_litterlgt.DataContext = globalvariabel.aforge_Usbcamera;
            //this.img2_redlgt.DataContext = globalvariabel.aforge_Usbcamera;
            //this.img3_normallgt.DataContext = globalvariabel.aforge_Usbcamera;
            //Frame_.DataContext= globalvariabel.aforge_Usbcamera;
            //globalvariabel.aforge_Usbcamera.StartCamera();

           

            globalvariabel.Body1_main.img4_bing.DataContext = globalvariabel.aforge_Usbcamera;
            globalvariabel.Body1_main.DataContext = globalvariabel.aforge_Usbcamera;
            globalvariabel.Body1_main.DataContext = globalvariabel.aforge_Usbcamera;
            globalvariabel.Body1_main.DataContext = globalvariabel.aforge_Usbcamera;

            this.Frame_.Content = globalvariabel.Body1_main;
            this.Frame_chosepage.Content = globalvariabel.chosepag;
            this.Fram_carmotion.Content = globalvariabel.car_motion;



          
         

        }
        public delegate void close_re();
        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        public event close_re closed_realse;
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.closed_realse += new close_re(globalvariabel.aforge_Usbcamera.StopCamera);
            this.closed_realse += new close_re(globalvariabel.aforge_Ipcamera.StopCamera);
            this.closed_realse();



            globalvariabel.camera_Litter.Close();
           
        }

       

        private void Infrared_mod4_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xd, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Infrared_mod3_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xc, 0X0F, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Infrared_mod2_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xb, 0X0F, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Infrared_mod1_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xa, 0X0F, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }
        #region car和飞行器速度触发
        private void Car_NormLight_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 6, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Car_LitterLight_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 7, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Car_red_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 8, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void Car_Fly_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 9, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }
        #endregion


        #region  模型的触发
        private void Model_Tank_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] {(ushort) val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 5, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void Model_CarryCar_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val  };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 4, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Model_BoreGUN_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val  };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 3, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void Model_RocketGUN_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val   };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 2, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Model_Person_sp_flt_change(float val)
        {
            try
            {
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)val };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 1, 2, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }
        #endregion

     
        public delegate bool iniValue();
        public event iniValue INIValue;
        
     

       

        private void CAR_RUN()
        {



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (globalvariabel.port != null && globalvariabel.port.IsOpen )
            {
                globalvariabel.port.Close();
                globalvariabel.port.Dispose();
            }
            this.Close();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            this.WindowState = WindowState.Minimized;
        }

       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {if
                (this.WindowState != WindowState.Normal)
            {

                this.WindowState = WindowState.Normal;
                globalvariabel.NotificationObject.max = false;
            }
            else
            { this.WindowState = WindowState.Maximized;
                globalvariabel.NotificationObject.max = true;
            }
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            }
        }

     

       

    
    }
}
