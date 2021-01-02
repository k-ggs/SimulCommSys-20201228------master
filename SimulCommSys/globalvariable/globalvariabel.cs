using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Modbus.Device;
using System.IO.Ports;
using SimulCommSys.Tool;
using SimulCommSys.CamerApi;
using System.Threading;

namespace SimulCommSys
{



    /// <summary>
    /// 全局静态变量
    /// </summary>
    public static class globalvariabel
    {


        public static SerialPort port;

        /// <summary>
        /// modbus串口
        /// </summary>
        public static IModbusMaster SerialMaster;

        /// <summary>
        /// 端口初始化
        /// </summary>
        public struct ini_Port
        {
            public static string protName { get; set; }
            public static int baudRate { get; set; }
            public static Parity parity { get; set; }
            public static int dataBits { get; set; }
            public static StopBits stopBits { get; set; }
        }

        public static bool[] coilsBuffer { get; set; }
        public static ushort[] registerBuffer { get; set; }
        public static Record_except Record_except = new Record_except();

        public struct Modbus_addr
        {  //写线圈或写寄存器数组

            //功能码
            //public ModbusOperate.funtioncode functionCode { get; set; }
            //参数(分别为站号,起始地址,长度)
            public byte slaveAddress { get; set; }
            public ushort startAddress { get; set; }
            public ushort numberOfPoints { get; set; }
        }


        public static ModbusOperate ModbusOperate = new ModbusOperate();


        #region 模型/小车/飞行车/红外目标控制配置
        public static Car_red Car_red = new Car_red();
        public static Car_NormLight Car_NormLight = new Car_NormLight();
        public static Car_LitterLight Car_LitterLight = new Car_LitterLight();
        public static Car_Fly Car_Fly = new Car_Fly();

        public static Model_Person Model_Person = new Model_Person();
        public static Model_Tank Model_Tank = new Model_Tank();
        public static Model_BoreGUN Model_BoreGUN = new Model_BoreGUN();
        public static Model_CarryCar Model_CarryCar = new Model_CarryCar();
        public static Model_RocketGUN Model_RocketGUN = new Model_RocketGUN();



        public static infrared_mod1 infrared_mod1 = new infrared_mod1();
        public static infrared_mod2 infrared_mod2 = new infrared_mod2();
        public static infrared_mod3 infrared_mod3 = new infrared_mod3();
        public static infrared_mod4 infrared_mod4 = new infrared_mod4();

        #endregion


        public static NotificationObject NotificationObject = new NotificationObject();


        //  master.Transport.Retries = 3;
        //                master.Transport.ReadTimeout = 1000;

        public static int Retries = 0;
        public static int ReadTimeout = 1000;



        public static Aforge_usbcamera aforge_Usbcamera = new Aforge_usbcamera();
        public static Aforge_ipcamera aforge_Ipcamera = new Aforge_ipcamera();


        public static Body1_main Body1_main = new Body1_main();
        public static Chosepag chosepag = new Chosepag();
        public static Car_motion car_motion = new Car_motion();




        public static img_max img_max = new img_max();



     //   public static MVCGE mvcge = new MVCGE();

      
        public static camera_operate camera_Litter= new camera_operate();
        public static ArenaManager ArenaManager = new ArenaManager();
        // public static camera_normal camera_normal = new camera_normal();
        // public static camera_red camera_red = new camera_red();
        #region 偏振相机


        #endregion
    }
}
