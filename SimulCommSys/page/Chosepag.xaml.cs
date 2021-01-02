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
    /// Chosepag.xaml 的交互逻辑
    /// </summary>
    public partial class Chosepag : Page
    {
        public Chosepag()
        {
            InitializeComponent();
            this.list_model.IsSelected=true;
        

         
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            #region 小车/飞行器
            //可见光 
            if (globalvariabel.Car_NormLight != null)
            {
                this.GRdSEE_lgt.DataContext = globalvariabel.Car_NormLight;
            }

            //红外车
            this.GRdRedLightLight.DataContext = globalvariabel.Car_red;
            //微光小车
            this.GRdLitterLight.DataContext = globalvariabel.Car_LitterLight;
            //飞行器
            this.GrdFly.DataContext = globalvariabel.Car_Fly;
            #endregion


            #region 模型
            //人
            this.GrdBoxModel_People.DataContext = globalvariabel.Model_Person;
            //线膛炮
            this.GrdBoxModel_Boreline.DataContext = globalvariabel.Model_BoreGUN;
            //火箭炮
            this.GrdBoxModel_rocketgun.DataContext = globalvariabel.Model_RocketGUN;
            //坦克
            this.GrdBoxModel_tank.DataContext = globalvariabel.Model_Tank;
            //运输车
            this.GrdBoxModel_CarryCar.DataContext = globalvariabel.Model_CarryCar;
            #endregion


            #region 红外控制
            infrared_1_Txt.DataContext = globalvariabel.infrared_mod1;
            infrared_2_Txt.DataContext = globalvariabel.infrared_mod2;
            infrared_3_Txt.DataContext = globalvariabel.infrared_mod3;
            infrared_4_Txt.DataContext = globalvariabel.infrared_mod4;





            #endregion

            #region 相机绑定




            if (globalvariabel.camera_Litter != null)
            {
                this.camer_lit_tempret.DataContext = globalvariabel.camera_Litter;
            }
            //   globalvariabel.camera_Litter.cameraTemperatu = 666;
            #endregion
        }




        private void person_forward_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 1, 3, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void person_Back_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 1, 4, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void Bornlin_forward_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 2, 3, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Bornlin_Back_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 2, 4, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Rockt_forward_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 3, 3, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void Rockt_Back_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 3, 4, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Tank_forward_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 4, 3, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Tank_Back_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 4, 4, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Carry_forward_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 5, 3, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Carryk_Back_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 5, 4, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void SeeLgt_forward_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 3, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void SeeLgt_Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 4, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }
        private void SeeLgt_LeftRevolve_Click(object sender, RoutedEventArgs e)
        {
            {
                globalvariabel.coilsBuffer = new bool[] { true };

                globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 5, 1);
            }

        }
        private void SeeLgt_RightRevolve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 6, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }



        private void SeeLgt_up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 7, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void SeeLgt_down_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 8, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void SeeLgt_Look_down_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 9, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void SeeLgt_Look_up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 0xa, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void LitterLight_forward_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 3, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void LitterLight_Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 4, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }
        private void LitterLight_LeftRevolve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 5, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }
        private void LitterLight_RightRevolve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 6, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }



        private void LitterLight_up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 7, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void LitterLight_down_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 8, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void LitterLight_Look_down_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 9, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void LitterLight_Look_up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 0xa, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void RedLight_forward_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 3, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void RedLight_Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 4, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }
        private void RedLight_LeftRevolve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 5, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }
        private void RedLight_RightRevolve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 6, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }



        private void RedLight_up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 7, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void RedLight_down_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 8, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void RedLight_Look_down_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 9, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void RedLight_Look_up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 0xa, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Fly_forword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 9, 3, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Fly_back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 9, 4, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Fly_Lookdwn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 9, 9, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void Fly_Lookup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { true };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 9, 0xa, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void Preson_Back_btstop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 1, 1, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Bornlin_Back_btstop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 2, 1, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Rockt_Back_btstop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 3, 1, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Tank_Back_btstop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 4, 1, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void Carry_Back_btstop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 5, 1, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        #region 可见光的点击控制
        private void SeeLgt_forbacstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 0xb, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void SeeLgt_RightRevolvestp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 0x0C, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void SeeLgt_upstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 0x0D, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void SeeLgt_Look_downstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 6, 0x0E, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }
        #endregion
        #region 微光的电机控制
        private void LitterLight_forwardstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 0x0B, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void LitterLight_RightRevolvestp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 0x0C, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void LitterLight_upstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 0x0D, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        private void LitterLight_Look_downstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 7, 0x0E, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }
        #endregion
        #region 红外线的电机控制
        private void RedLight_forwardstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 0x0B, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void RedLight_RightRevolvestp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 0x0C, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void RedLight_upstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 0x0D, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void RedLight_Look_upstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 8, 0x0E, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }
        #endregion
        #region 飞行器的电机控制
        private void Fly_forwordstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 9, 0x0B, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }

        private void Fly_Lookdwnstp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    globalvariabel.coilsBuffer = new bool[] { false };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleCoilAsync_x05, 9, 0x0E, 1);
                }
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }

        }




        #endregion
        #region 红外开关
        private void infrared_1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)infrared_1.IsChecked)
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)0xf1 };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xa, 0x0E, 1);

                }
                else
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)0xf2 };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xa, 0x0E, 1);



                }
            }
            catch (Exception ex)
            { globalvariabel.Record_except.Msg(ex.ToString(), Record_except.msgtype.error); }

        }

        private void infrared_2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)infrared_2.IsChecked)
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)0xf1 };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xb, 0x0E, 1);

                }
                else
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)0xf2 };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xb, 0x0E, 1);



                }
            }
            catch (Exception ex)
            { globalvariabel.Record_except.Msg(ex.ToString(), Record_except.msgtype.error); }
        }

        private void infrared_3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)infrared_3.IsChecked)
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)0xf1 };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xc, 0x0E, 1);

                }
                else
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)0xf2 };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xc, 0x0E, 1);



                }
            }
            catch (Exception ex)
            { globalvariabel.Record_except.Msg(ex.ToString(), Record_except.msgtype.error); }
        }

        private void infrared_4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)infrared_4.IsChecked)
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)0xf1 };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xd, 0x0E, 1);

                }
                else
                {
                    globalvariabel.registerBuffer = new ushort[1] { (ushort)0xf2 };

                    globalvariabel.ModbusOperate.ExecuteFunction(ModbusOperate.funtioncode.WriteSingleRegisterAsync_x06, 0xd, 0x0E, 1);



                }
            }
            catch (Exception ex)
            { globalvariabel.Record_except.Msg(ex.ToString(), Record_except.msgtype.error); }

        }
       
        #endregion
        private void list_model_Selected(object sender, RoutedEventArgs e)
        {
            this.grid_mod.Visibility = Visibility.Visible;
            this.grid_car.Visibility = Visibility.Collapsed;
            this.grid_carmer.Visibility= Visibility.Collapsed;
        }

        private void list_car_Selected(object sender, RoutedEventArgs e)
        {
            this.grid_mod.Visibility = Visibility.Collapsed;
            this.grid_car.Visibility = Visibility.Visible;
            this.grid_carmer.Visibility = Visibility.Collapsed;
        }

        private void list_carmaer_Selected(object sender, RoutedEventArgs e)
        {
            this.grid_mod.Visibility = Visibility.Collapsed;
            this.grid_car.Visibility = Visibility.Collapsed;
            this.grid_carmer.Visibility = Visibility.Visible;

        }

        private void camera2_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                globalvariabel.camera_Litter.cameraTemperatu = 77;
            }
        }

    }
}
