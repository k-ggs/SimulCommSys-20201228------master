using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys
{
  public  class MVCGE
    {
        private int m_bDispMode = 0; //内部内嵌显示
        private string m_sFileLocation = ""; //存图路径,默认为当前路径
        private bool m_bSaveflag = false; //单帧存图标志位
        private bool m_bColor = false; //是否彩色相机

        #region 设备列表结构体
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MVCGE_DEVLISTEX
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]//将托管类型封装成非托管并设置数组大小
            public string DevName;//设备名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string DevIP;//设备IP地址
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string DevMAC;//设备MAC地址
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string DevSerialNum;//设备序列号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string DevSubnetMask;//设备子网掩码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string DevGateWay;//设备网关
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string DevManufacturerName;//设备工厂名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string DevGigEVersion;//GigeVision相机版本号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string DevMultiCastAddress;//设备Multicast地址

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string AdapterInfo;//网卡信息
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string AdapterMAC;//网卡MAC地址
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string AdapterIP;//网卡IP
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string AdapterSubnetMask;//网卡子网掩码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string AdapterGateway;//设备网关
            [MarshalAs(UnmanagedType.U4)]
            public uint mDeviceType; //网络连接类型
            [MarshalAs(UnmanagedType.Bool)]
            public bool DevEnable;//设备是否已连接
        }
        #endregion

        #region 图像信息的描述结构，描述一帧图像的相关信息结构体
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MVCFRAMEINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public uint FRAMEID;// 当前图像帧号(从开始采集到当前)
            [MarshalAs(UnmanagedType.U4)]
            public uint IBufSize;//图像的总的大小
            [MarshalAs(UnmanagedType.U4)]
            public uint Width;//图像宽度
            [MarshalAs(UnmanagedType.U4)]
            public uint Height;//图像高度
            [MarshalAs(UnmanagedType.U4)]
            public uint PixelID;//图像单个像素占的位数
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr IBufPtr;//图像数据的指针地址

        }
        #endregion

        #region 采集状态
        public struct MVCDEVSTATE
        {
            [MarshalAs(UnmanagedType.R8)]
            public double CAP_FPS;
            [MarshalAs(UnmanagedType.R8)]
            public double CAP_AVE_FPS;
            [MarshalAs(UnmanagedType.R8)]
            public double DISP_FPS;
            [MarshalAs(UnmanagedType.R8)]
            public double DISP_AVE_FPS;
            [MarshalAs(UnmanagedType.U4)]
            public uint CAP_ImageCount;
        }
        #endregion

        #region 矩形结构体
        public struct RECT
        {
            [MarshalAs(UnmanagedType.U4)]
            public int Left;
            [MarshalAs(UnmanagedType.U4)]
            public int Top;
            [MarshalAs(UnmanagedType.U4)]
            public int Right;
            [MarshalAs(UnmanagedType.U4)]
            public int Bottom;
        }
        #endregion

        #region 返回错误信息
        public enum CYRESULT
        {
            CY_RESULT_OK = 0,
            CY_RESULT_ABORTED = 1,
            CY_RESULT_ARGUMENT_TOO_LONG = 2,
            CY_RESULT_BAD_ADDRESS = 3,
            CY_RESULT_BUFFER_TOO_SMALL = 4,
            CY_RESULT_CANNOT_CREATE_EVENT = 5,
            CY_RESULT_CANNOT_OPEN_FILE = 6,
            CY_RESULT_CANNOT_SET_EVENT = 7,
            CY_RESULT_CONSTRUCTOR_FAILED = 8,
            CY_RESULT_CORRUPTED_FILE = 9,
            CY_RESULT_D3D_ERROR = 10,
            CY_RESULT_DD_ERROR = 11,
            CY_RESULT_DEVICE_ERROR = 12,
            CY_RESULT_DEVICE_RESET = 13,
            CY_RESULT_DRIVER_ERROR = 14,
            CY_RESULT_EMPTY_SEQUENCE = 15,
            CY_RESULT_FILE_ERROR = 16,
            CY_RESULT_INTERNAL_ERROR = 17,
            CY_RESULT_INVALID_ARGUMENT = 18,
            CY_RESULT_NETWORK_ERROR = 19,
            CY_RESULT_NO_AVAILABLE_DATA = 20,
            CY_RESULT_NO_MORE_ITEM = 21,
            CY_RESULT_NO_SELECTED_ITEM = 22,
            CY_RESULT_NOT_CONNECTED = 23,
            CY_RESULT_NOT_ENOUGH_MEMORY = 24,
            CY_RESULT_NOT_FOUND = 25,
            CY_RESULT_NOT_SUPPORTED = 26,
            CY_RESULT_OVERFLOW = 27,
            CY_RESULT_STATE_ERROR = 28,
            CY_RESULT_THREAD_ERROR = 29,
            CY_RESULT_TIMEOUT = 30,
            CY_RESULT_UNDERFLOW = 31,
            CY_RESULT_UNEXPECTED_ERROR = 32,
            CY_RESULT_UNEXPECTED_EXCEPTION = 33,
            CY_RESULT_UNKNOWN_ERROR = 34,
            CY_RESULT_UNSUPPORTED_CONVERSION = 35,
            CY_RESULT_OPERATION_PENDING = 36,
            CY_RESULT_IMAGE_ERROR = 37,
            CY_RESULT_CORRUPTED_IMAGE = 38,
            CY_RESULT_MISSING_PACKETS = 39,
            CY_RESULT_NETWORK_LINK_DOWN = 40,
            CY_RESULT_MAX = 41,
            CY_RESULT_DEVICE_OPEN_ERROR = 100,
            CY_RESULT_DEVICE_NO_OPEN = 101,
            CY_RESULT_DEVICE_LOST = 102,
            CY_RESULT_OTHER_ERROR = 200,
        }
        #endregion

        #region 设备信息
        public enum DEVICE_INFO
        {
            DEVICE_IP = 0,
            DEVICE_NAME = 1,
        }
        #endregion

        #region 文件存储格式枚举
        public enum FILE_FORMT
        {
            FILE_BMP = 0,//bmp格式图像文件
            FILE_RAW = 1,//原始图像数据
            FILE_JPG = 2,//JPEG格式图像文件
        }
        #endregion

        #region 图像显示支持的颜色格式枚举
        public enum DISPCOLORFMT
        {
            DISP_Y8 = 0,//黑白图像
            DISP_RGB15 = 1,//RGB15
            DISP_RGB16 = 2,
            DISP_RGB24 = 3,
            DISP_RGB32 = 4,
            DISP_YUYV = 5,
            DISP_UYVY = 6,
            DISP_YV12 = 7
        }
        #endregion

        #region 显示函数支持的显示模式
        public enum DISPMethod
        {
            DISP_OFFSCREEN = 0,//DirectX模式(一般只支持RGB15/RGB16/RGB32)
            DISP_OVERLAY = 1,//DirectX模式(一般支持RGB15/RGB16/RGB32)
            DISP_GDIFUNCTION = 2,//GDI模式(支持Y8/RGB15/RGB16/RGB32)
            DISP_AUTO = 3
        }
        #endregion

        #region 图像采集位数定义枚举
        public enum PIXCEL_BITMODE
        {
            PIXCEL_BIT10 = 0,//10bit输出采集10bit
            PIXCEL_BIT10_H8 = 1,//10bit输出采集高8bit
            PIXCEL_BIT8 = 2,//8bit或10bit输出采集低8bit或12bit输出采集低8bit或14bit输出采集低8bit或16bit输出采集低8bit
            PIXCEL_BIT12 = 3,//12bit输出采集12bit
            PIXCEL_BIT12_H10 = 4,//12bit输出采集高10bit
            PIXCEL_BIT12_H8 = 5,//12bit输出采集高8bit
            PIXCEL_BIT14 = 6,//14bit输出采集14bit
            PIXCEL_BIT14_H12 = 7,//14bit输出采集高12bit
            PIXCEL_BIT14_H10 = 8,//14bit输出采集高10位
            PIXCEL_BIT14_H8 = 9,//14bit输出采集高8bit
            PIXCEL_BIT16 = 10,//16bit输出采集16bit
            PIXCEL_BIT16_H14 = 11,//16bit输出采集高14bit
            PIXCEL_BIT16_H12 = 12,//16bit输出采集高12bit
            PIXCEL_BIT16_H10 = 13,//16bit输出采集高10bit
        }
        #endregion

        #region SDK支持设备
        public enum MVC_TYPE
        {
            MVC300SAM_GE100 = 64,   //NOT SUPPORT
            MVC300SAM_GE50 = 65,   //NOT SUPPORT
            MVC300SAC_GE200 = 60,   //SUPPORT FWVer4.0.0.0 or higher
            MVC300SAM_GE200 = 61,   //SUPPORT FWVer4.0.0.0 or higher

            MVC360SAM_GE60 = 62,   //SUPPORT FWVer3.0.0.1 or higher
            MVC360SAC_GE60 = 63,   //SUPPORT FWVer3.0.0.1 or higher
            MVC360SAM_GE60_MINI = 124,  //NOT SUPPORT

            MVC1000SAC_GE30 = 20,   //SUPPORT HWVer5.0 or higher
            MVC1000SAM_GE30 = 21,   //SUPPORT HWVer5.0 or higher

            MVC1280SAC_GE30 = 10,
            MVC1280SAM_GE30 = 11,
            MVC1280SANR_GE30 = 12,

            MVC2000SAC_GE20 = 30,   //SUPPORT HWVer5.0 or higher
            MVC3000SAC_GE12 = 40,   //SUPPORT HWVer5.0 or higher
            MVC5000SAC_GE12 = 41,   //SUPPORT HWVer2.0 or higher
            MVC5001SAC_GE14 = 52,   //SUPPORT HWVer2.0.0.2 or higher
            MVC5001SAM_GE14 = 53,   //SUPPORT HWVer2.0.0.2 or higher
            MVC6600SAC_GE5 = 50,
            MVC6600SAM_GE5 = 51,

            //CVT Series
            MVCL2GE_B = 70,
            MVLV2GE_S = 71,
            MVDIG2GE_S = 72,

            //ST Series
            MVC360SAM_GE60ST = 118,  //NOT SUPPORT
            MVC360SAC_GE60ST = 119,  //NOT SUPPORT
            MVC360SAM_GE60ST3 = 121,  //NOT SUPPORT
            MVC360SAC_GE60ST3 = 122,  //NOT SUPPORT
            MVC360SAM_GE60ST4 = 127,  //NOT SUPPORT
            MVC360SAC_GE60ST4 = 128,  //NOT SUPPORT

            MVC1000SAC_GE30ST = 112,  //NOT SUPPORT
            MVC1000SAM_GE30ST = 113,  //NOT SUPPORT
            MVC1000SAM_GE30ST3 = 123,  //NOT SUPPORT
            MVC1000SAM_GE30ST4 = 129,  //NOT SUPPORT
            MVC1000SAC_GE30ST4 = 130,  //NOT SUPPORT

            MVC2000SAC_GE20ST = 114,  //NOT SUPPORT
            MVC2000SAC_GE20ST4 = 131,  //NOT SUPPORT

            MVC3000SAC_GE12ST = 115,  //NOT SUPPORT
            MVC3000SAC_GE12ST4 = 132,  //NOT SUPPORT

            MVCSDV_GEST4 = 209,
            MVCCL_GEST4 = 213,
            MVCCL_GEST2_N = 223,
            MVCPOCL_GEST2_N = 226,
            MVCPOCL_GEST4_N = 229,

            MVC360SAM_GE60ST2 = 133,
            MVC360SAC_GE60ST2 = 134,
            MVC360SAM_GE60ST2_N = 216,
            MVC360SAC_GE60ST2_N = 217,
            MVC360SAM_GE60ST4_N = 211,
            MVC360SAC_GE60ST4_N = 212,

            MVC1000SAM_GE30ST2 = 135,
            MVC1000SAM_GE30ST2_N = 218,
            MVC1000SAC_GE30ST2_N = 220,
            MVC1000SAM_GE20ST4_N = 224,

            MVC3000SAC_GE8ST4_N = 225,
            MVC3000SAC_GE12ST2_N = 219,

            //Line Scan Series
            MVC512DLM_GE60 = 210,
            MVC512DLM_CL60 = 303,

            MVC1024DLM_GE35 = 205,
            MVC1024DLM_CL35 = 302,

            MVC2048DLM_GE15 = 90,
            MVC2048DLM_GE19 = 111,
            MVC2048DLM_CL19 = 301,

            //CCD Series
            MVC380DAM_GE60 = 208,
            MVC380DAC_GE60 = 207,

            MVC600DAM_GE60 = 102,
            MVC600DAC_GE60 = 117,
            MVC600DAS_GE60 = 126,

            MVC610DAM_GE110 = 92,
            MVC610DAC_GE110 = 91,
            MVC610DAS_GE110 = 136,

            MVC680DAM_GE60 = 110,
            MVC680DAM_GE60_12 = 96,
            MVC680DAC_GE60 = 85,
            MVC680DAS_GE60 = 125,

            MVC685DAM_GE110 = 94,
            MVC685DAC_GE110 = 79,
            MVC685DAS_GE110 = 106,

            MVC800DAM_GE45 = 83,
            MVC800DAM_GE25 = 137,
            MVC800DAC_GE45 = 104,

            MVC900DAM_GE15 = 84,
            MVC900DAM_GE30 = 101,
            MVC900DAC_GE15 = 81,
            MVC900DAC_GE30 = 97,
            MVC900DAS_GE15 = 116,

            MVC930DAM_GE25 = 78,
            MVC930DAM_GE30 = 80,
            MVC930DAC_GE30 = 105,
            MVC930DAM_GE30_12 = 89,

            MVC1000DAM_GE1000 = 109,

            MVC1450DAM_GE15 = 201,
            MVC1450DAC_GE15 = 200,

            MVC1800DAM_GE15 = 87,
            MVC1800DAM_GE30 = 108,
            MVC1800DAC_GE15 = 82,
            MVC1800DAC_GE30 = 99,
            MVC1800DAS_GE15 = 107,

            MVC1830DAM_GE30 = 103,
            MVC1830DAM_GE30_12 = 86,
            MVC1830DAC_GE30 = 98,
            MVC1830DAC_GE30_12 = 120,
            MVC1830DAS_GE30 = 100,
            MVC1830DAS_GE30_12 = 88,

            MVC2000DAC_GE16 = 93,
            MVC2000DAM_GE16 = 95,

            MVC2010DAM_GE12 = 206,
            MVC2010DAC_GE12 = 204,
            MVC2010DAC_GE6 = 215,

            MVC2011DAM_GE15_N = 227,
            MVC2011DAC_GE15_N = 228,

            MVC2900DAC_GE15 = 202,
            MVC2900DAM_GE15 = 203,
            MVC2900DAC_GE8 = 214,

            VCC_F60FV19_GE15 = 401,
            VCC_F60FU29_GE26 = 402,
            VCC_G60FV11_GE15 = 403,
            VCC_G60U21_GE30 = 404,

            //CMOS Series
            MVC361SAM_GE60_N = 221,
            MVC361SAC_GE60_N = 222,

            MVC500SAM_GE60_N = 531,
            MVC500SAC_GE60_N = 532,

            MVC660SAM_GE120_N = 571,

            MVC1300SAM_GE60_N = 501,
            MVC1300SAC_GE60_N = 502,
            MVC1300SAM_GE30 = 503,//Deprecated
            MVC1300SAC_GE30 = 504,//Deprecated
            MVC1301SAM_GE60_N = 514,
            MVC1310SAM_GE60 = 510,//Deprecated
            MVC1320SAC_GE60 = 511,//Deprecated
            MVC1330SAM_GE60 = 507,//Deprecated
            MVC1330SAC_GE60 = 508,
            MVC1340SAM_GE60_N = 512,
            MVC1340SAC_GE60_N = 513,//Deprecated
            MVC1340SAM_GE60 = 506,
            MVC1370SAM_GE30 = 509,//Deprecated
            MVC1371SAM_GE30 = 505,

            MVC1400SAM_GE60_N = 541,
            MVC1400SAC_GE60_N = 542,
            MVC1400SANR_GE60_N = 552,
            MVC1400SAV_GE60_N = 553,
            MVC1410SAM_GE60 = 543,
            MVC1440SAM_GE60 = 531,
            MVC1450SAM_GE60_N = 532,
            MVC1450SAC_GE60_N = 533,
            MVC1470SAM_GE30 = 550,
            MVC1471SAM_GE30 = 551,
            MVC1472SAM_GE30 = 305,
            MVC1480SAM_GE60_N = 537,
            MVC1480SAC_GE60_N = 538,

            MVC1500SAM_GE60_N = 561,
            MVC1500SAC_GE60_N = 562,
            MVC1500SANR_GE60_N = 563,
            MVC1500SANR_GE6_N = 564,
            MVC1500SAV_GE60_N = 565,
            MVC1510SAM_GE60 = 568,
            MVC1531SAM_GE60 = 569,
            MVC1531SAC_GE60 = 570,
            MVC1550SAM_GE60_N = 571,
            MVC1550SAC_GE60_N = 572,
            MVC1570SAM_GE30 = 573,
            MVC1571SAM_GE30 = 574,
            MVC1580SAM_GE60_N = 575,
            MVC1580SAC_GE60_N = 576,

            MVC10KSAC_GE7_N = 601,
            MVC10KSAM_GE7_N = 602,
            MVC1200SAM_GE45_N = 611,
            MVC1200SAC_GE45_N = 612,
            MVC14KSAC_GE6_N = 603,

        }
        #endregion

        #region 参数枚举
        public enum MVC_PARAMTER
        {
            //Image Grabber Param
            MVCADJ_WIDTH = 0, //Capture Width
            MVCADJ_HEIGHT = 1, //Capture Height
            MVCADJ_XOFFSET = 2, //Capture Offset
            MVCADJ_YOFFSET = 3, //Capture Offset
            MVCADJ_BITMODE = 14, //Capture PixelType(See: PIXEL_BITMODE)

            MVCADJ_UNDEFINED_WIDTH = 8, //Use LVAL(Line Valid signal from the camera) to detect Width
            MVCADJ_UNDEFINED_HEIGHT = 9, //Use FVAL(Frame Valid signal from the camera) to detect Height
            MVCADJ_COLORMODE = 100, //Bayer Color(0 NoBayer ,1 BayerColor, 2 3*3 BayerColor)
            MVCADJ_PIXELCONVERT = 101, //Invert Pixel
            MVCADJ_RGBMODE = 102, //Capture RGB Color(0 NoRGB24,1 RGB24，2 BGR24)

            //Camera Param 
            MVCADJ_XBINNING = 4, //X Binning
            MVCADJ_YBINNING = 5, //Y Binning
            MVCADJ_SHUTTERTYPE = 6, //Shutter Mode(0: TriggerMode 1: ContinueMode)
            MVCADJ_SNAPSHUTTER = 7, //SnapShutter Time

            MVCADJ_GAMMA = 10, //Camera Gamma Gain(0:Disable 1:Enable)
            MVCADJ_DACGAIN = 11, //Camera DAC Gain
            MVCADJ_OUTGAIN = 12, //Camera Out Gain
            MVCADJ_INTTIME = 13, //Camera Exposure Time
            MVCADJ_INTNUM = 15, //Camera Exposure Number

            MVCADJ_REDGAIN = 16, //Camera Red Gain
            MVCADJ_GREENGAIN = 17, //Camera Green Gain
            MVCADJ_BLUEGAIN = 18, //Camera Blue Gain

            MVCADJ_OFFSET = 19,
            MVCADJ_MASTER = 20, //Camera Work Mode(Master/Slave)

            MVCADJ_MIRROR = 30, //Camera left right Flip
            MVCADJ_COLFLIP = 34, //Camera up down Flip

            MVCADJ_AUTOAGC = 35, //Camera Auto Gain Control
            MVCADJ_AUTOAEC = 36, //Camera Auto Exposure Control

            MVCADJ_BITLUT = 38, //Camera Pixel Out Lut

            //Trigger Flash Param
            MVCADJ_TRIGPICNUM = 21, //One Trigger Capture Frame Number
            MVCADJ_TRIGGERDELAY = 22, //Interval Of The Two Trigger
            MVCADJ_PULSEWIDTH = 23, //Trigger Effective Pulse Width

            MVCADJ_FLASHDELAY = 24, //Flash Pulse Delay
            MVCADJ_FLASHWIDTH = 25, //Flash Pulse Width
            MVCADJ_SOFTTRIGGER = 26, //Software Trigger

            MVCADJ_STROBEOUT = 27, //Flash Out(Enable/Disable)

            MVCADJ_TRIGGER_DELAY = 28, //Trigger Delay
            MVCADJ_TRIGGER_PLUSEWIDTH = 29, //Trigger Effective Pulse Width

            MVCADJ_FLASH_POLARITY = 608, //active when the signal is low(FALSE)
            MVCADJ_TRIGGER_POLARITY = 609, //active when the signal is low(FALSE)
            MVCADJ_TRIGGER_SOURCE = 610, //Input Trigger Source Select
            MVCADJ_FLASH_SOURCE = 611, //Output Flash Source Select

            MVCL_QUAD_DEBOUNCER = 700, //Trigger Effective Pulse Width(= MVCADJ_TRIGGER_PLUSEWIDTH)

            //Long Time Integration Param
            MVCADJ_LT_INT_DELAY = 31,
            MVCADJ_LT_INT_MODE = 32,
            MVCADJ_LT_INT_TIME = 33,

            //MultiHead Camera Special Param
            MVCADJ_CONCAMNUM = 39,
            MVCADJ_SELECTCAM = 40,
            MVCADJ_CAMERASYNC = 614,

            //MVC1450/2900/2010 Special Param
            MVCADJ_CCD_CLAMPLEVEL = 41,
            MVCADJ_CCD_CDSGAIN = 42,

            //Param Save Load
            MVCADJ_LOADDEFAULT = 50, //Load UserSet X Param
            MVCADJ_SAVEDEFAULT = 51, //Save UserSet X Param
            MVCADJ_POWERON_LOAD = 53, //Set PowerOn Use UserSet X Param

            //MVC2048DLM_GE15/GE19 Camera Special Param
            MVCADJ_LINETIME = 52, //(Example: MVC2048DLM_GE15/GE19)
            MVCADJ_LINETRIGGER_DIVIDER = 54, //Line Trigger Divider
            MVCADJ_FFC_STATUS = 55, //Enable/Disable the Flat Field Correction
            MVCADJ_FFC_PARAM_SAVE = 56, //Save Flat Field Correction Param
            MVCADJ_FFC_PARAM_LOAD = 57, //Load Flat Field Correction Param

            //WatchDog
            MVCADJ_WATCHDOGENABLE = 60,
            MVCADJ_WATCHDOGDISABLE = 61,

            //Sensor Width/Height/Offset
            MVCADJ_SENSORWIDTH = 70,
            MVCADJ_SENSORHEIGHT = 71,
            MVCADJ_SENSORXOFFSET = 72,
            MVCADJ_SENSORYOFFSET = 73,

            MVCADJ_SENSORRESET = 74,

            //Display Param
            MVCDISP_POSX = 201,
            MVCDISP_POSY = 202,
            MVCDISP_WIDTH = 203,
            MVCDISP_HEIGHT = 204,
            MVCDISP_ISDISP = 205,
            MVCDISP_HWND = 206,
            MVCDISP_RGBMODE = 207, //Display RGB/BGR Mode Select
            MVCDISP_MEMMODE = 208, //Video Memory/System Memory
            MVCDISP_VSYNC = 209,

            //
            MVCCAP_BUFQUEUESIZE = 300, //available frame queue
            MVCCAP_RGBMODE = 301, //GBRG/GRBG/RGGB/BGGR Bayer Mode Select
            MVCRGBMODE = 302, //RGB/BGR Mode Select

            //Soft White Balance Param
            MVCCAP_REDGAIN = 303, //Soft Red Gain
            MVCCAP_GREENGAIN = 304, //Soft Green Gain
            MVCCAP_BLUEGAIN = 305, //Soft Blue Gain

            MVCCAP_REDOFFSET = 333, //Soft Red Offset
            MVCCAP_GREENOFFSET = 334, //Soft Green Offset
            MVCCAP_BLUEOFFSET = 335, //Soft Blue Offset

            //Only Support MVCSDV_GEST4 Special
            MVCADJ_HWIDTHMEM = 308, //MVCSDV_GEST4 Capture Width Memory
            MVCADJ_VHEIGHTMEM = 309, //MVCSDV_GEST4 Capture Height Memory

            //Soft Bright/Contrast Param
            MVCCAP_SBRIGHTNESS = 310,
            MVCCAP_SCONTRAST = 311,

            //Engine Param Adjust
            MVCADJ_DATAPORT = 37,
            MVCCAP_FRAMESKIP = 306,
            MVCCAP_PIPECOUNT = 307,

            MVCCAP_DECIMATION_X = 320,
            MVCCAP_DECIMATION_Y = 321,
            MVCCAP_DECIMATION_BLOCK_X = 322,
            MVCCAP_DECIMATION_BLOCK_Y = 323,

            //ST TimeStamp Param
            MVCCON_TIMESTAMPCLEAR = 324,
            MVCCON_TIMESTAMPMODE = 325,
            MVCCON_TIMESTAMPPARAM = 326,

            MVCCAP_BANDWIDTH = 327,
            MVCCAP_LOSTPACKET = 328,
            MVCCAP_RESEND = 329,

            MVCCAP_IMGOUTMODE = 330,

            //Traffic Control
            MVCCAP_ENSPEED = 400,
            MVCCAP_EMSPEEDMIN = 401,
            MVCCAP_EMSPEEDMAX = 402,
            MVCCAP_EMSPEEDDIST = 403,
            MVCCAP_ENTEMP = 411,
            MVCCAP_ENLIGHT = 412,
            MVCCAP_ENFRAMENO = 413,
            MVCCAP_ENBRIGHT = 414,
            MVCCAP_ENAREA = 420,
            MVCCAP_EMAREAHSTART = 421,
            MVCCAP_EMAREAHEND = 422,
            MVCCAP_EMAREAVSTART = 423,
            MVCCAP_EMAREAVEND = 424,
            MVCCAP_WISHBRIGHTMIN = 430,
            MVCCAP_WISHBRIGHTMAX = 431,
            MVCCAP_AECMIN = 432,
            MVCCAP_AECMAX = 433,
            MVCCAP_AGCMIN = 434,
            MVCCAP_AGCMAX = 435,

            //
            MVCL_CAMTYPE = 500, //Camera Type(AreaScan、LineScan)

            MVCL_DATAVALID_ENABLE = 501, //Use DVAL signal to grab pixels(FALSE)
            MVCL_DATAVALID_POLARITY = 502, //assume DVAL to be active when the signal is low(FALSE)
            MVCL_LINEVALID_POLARITY = 503, //assume LVAL to be active when the signal is low(FALSE)
            MVCL_FRAMEVALID_POLARITY = 504, //assume FVAL to be active when the signal is low(FALSE)
            MVCL_LINEVALID_EDGE = 505, //start grabbing a line on the rising edge of the signal and not on the level.(FALSE)
            MVCL_FRAMEVALID_EDGE = 506, //start grabbing a frame on the rising edge of the signal and not on the level.(FALSE)
            MVCL_FRAMEVALID_SELECT = 507, //Select FVAL Signal (FVAL from Camera)
            MVCL_LINEVALID_SELECT = 508, //Select LVAL Signal (LVAL from Camera)
            MVCADJ_INTERPACKETDELAY = 509,
            MVCADJ_SHOWCONFIGDIALOG = 510, //Show Config Dialog
            MVCL_CLOCKPRESENT = 511, //Connect Camera Pixel Clock Status
            MVCL_TAP_QUANTITY = 512, //Connect Camera Tap Number
            MVCL_FVAL_FUNCTION_SELECT = 513,
            MVCL_LVAL_FUNCTION_SELECT = 514,
            MVCL_TAP_OUTPUTMODE = 515, //Select Two Tap OutputMode(DUALTAPOUTPUTMODE_TYPE)

            //MVC360 High dynamic range Special param
            MVCADJ_EN_HIGH_DY_RANGE = 601,
            MVCADJ_HIGH_DY_RANGEV1 = 602,
            MVCADJ_HIGH_DY_RANGEV2 = 603,
            MVCADJ_HIGH_DY_RANGEV3 = 604,
            MVCADJ_HIGH_DY_RANGEV4 = 605,

            //Special param
            MVCADJ_FRAMERATEMODE = 606,
            MVCADJ_AUOTIRIS1 = 612,
            MVCADJ_AUOTIRIS2 = 613,
            MVCADJ_RPTRIGERDELAY = 615,
            MVCADJ_SPECIFICFLASH = 616,

            //Line Scan Camera Special Param(MVC2048DLM_GE19\MVC1024DLM_GE35)
            MVCADJ_VIRTUALFRAMEHEIGHT = 617, //FrameTriggerMode Virtual FrameHeight
            MVCADJ_EXPOSUREMODE = 618, //Line Exposure Mode
            MVCADJ_FRAMETRIGGER_POLARITY = 619, //FRAMETRIGGER POLARITY
            MVCADJ_DTPIXELDISCREPANCY = 620,
            MVCADJ_DTIMAGEBKMEAN = 621,
            MVCADJ_DTTHRESHOLD = 622,
            MVCADJ_DTSKIPHEIGHT = 623,
            MVCADJ_LEDCONTROL = 624, //MVC2010DAC(Ver:3XBB、3XBC) (30C6)

            //Special param
            MVCADJ_BOARDPASS = 600, //CAMERA PASS
            MVCDEV_CONNECTSTATE = 607,
            MVCGET_FIRMWAREVER = 701,
            MVCGET_ISCAPTURE = 702,
            MVCGET_ISHOOKCAP = 703,

            MVCADJ_AUTOINTERNALRETRIGGER = 731,

            //Special param 20101227	
            MVCADJ_FRAMEINTERVAL = 800, //帧间隔时间
            MVCADJ_TRIGGER_MODESELECT = 801, //多路触发模式选择
            MVCADJ_HOFFSETMEM = 802, //SDRAM水平偏移
            MVCADJ_VOFFSETMEM = 803, //SDRAM垂直偏移

            MVCADJ_SHOWSERIALDIALOG = 900, //Show Serial Dialog

            //Trig FPS 20110608
            MVCADJ_TRIGPICFPS = 901,	//Trig FPS
            MVCADJ_INTERPULSEFREQ = 902,	//盒子内部产生脉冲的频率

            //MVC1300 PARAMS
            MVCADJ_HDR = 903, //高动态使能
            MVCADJ_BLACKLEVEL = 904, //黑电平校正使能
            MVCADJ_NOISEREMOVE = 905, //坏点屏蔽使能
            MVCADJ_FPSMAX = 906, //前端最高帧率
            MVCADJ_XSUBSAMPLING = 907, //水平方向抽点 X Sub-sampling
            MVCADJ_YSUBSAMPLING = 908, //垂直方向抽点 Y Sub-sampling
            MVCADJ_SHUTTERMODE = 909, //0:Global Shutter 1:Rolling Shutter

        }
        # endregion

        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_ReScanDevice();
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_SetNetPacketSize(uint DevNo, uint PacketSize);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_AutoIPConfig(uint DevNo);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_GetDeviceInfoEx(uint DevNo, ref MVCGE_DEVLISTEX DevList);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_GetDeviceNumber();
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_GetDeviceStat(uint DevNo, ref MVCDEVSTATE DevStat);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_OpenDevice(uint DevNo);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_CloseDevice(uint DevNo);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_EnableCapture(uint DevNo);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_DisableCapture(uint DevNo);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_SetDeviceInfo(uint DevNo, DEVICE_INFO DevInfo, string Infostr);
        [DllImport("MVC_API64.dll")]
        public static extern MVC_TYPE MVC_GetCameraType(uint DevNo);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_SetParameter(uint DevNo, MVC_PARAMTER Oper, uint Val);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_GetParameterEx(uint DevNo, MVC_PARAMTER Oper, ref uint Val);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_SetStreamHOOK(uint DevNo, CallbackFun StreamFun, IntPtr pUserData);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_SaveFile(uint DevNo, FILE_FORMT FTYPE, ref MVCFRAMEINFO FrameInfo, string FileName, bool IsUpDown, bool ColorNot, uint Quality, bool m_bRGB15TO24Mode);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_AutoWhiteBalance(uint DevNo);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_SubDisplayInit(uint CardNo, uint CamNo, RECT rcDest, IntPtr hWnd, int Bayer3x3);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_SubDisplayDisp(uint CardNo, uint CamNo, IntPtr buffer, uint ImgWid, uint ImgHei);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_SubDisplayClose(uint CardNo, uint CamNo);
        [DllImport("MVC_API64.dll")]
        public static extern uint MVC_PixelConverter(uint CardNo, ref MVCFRAMEINFO RAWFrameInfo, ref MVCFRAMEINFO RGBFrameInfo, bool ConverterFlag);

        /*-------------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------API接口函数二次封装------------------------------------------------*/
        /*-------------------------------------------------------------------------------------------------------------------*/
        //查看设备数
        public uint GetDeviceNum()
        {
            uint DevNum;
            DevNum = MVC_GetDeviceNumber();
            return DevNum;
        }

        //获取设备信息
        public void GetDevInfo(uint DevNo, ref MVCGE_DEVLISTEX temp)
        {
            MVC_GetDeviceInfoEx(DevNo, ref temp);
        }

        //设置设备IP
        public void SetDevInfo(uint DevNo, DEVICE_INFO DevInfo, string InfoStr)
        {
            MVC_SetDeviceInfo(DevNo, DevInfo, InfoStr);
        }

        //重新检测系统设备
        public void RescanDevice()
        {
            MVC_ReScanDevice();
        }

        //判断是否彩色相机
        public bool DetectColor(uint DevNo)
        {
            uint lBColor = GetDevParameter(DevNo, MVC_PARAMTER.MVCADJ_COLORMODE);
            m_bColor = lBColor > 0;
            return m_bColor;
        }

        //内部显示使能
        public void InnerDispEnable(uint DevNo, uint lFlag)
        {
            MVC_SetParameter(DevNo, MVC_PARAMTER.MVCDISP_ISDISP, lFlag);
        }

        //内部显示设置窗体句柄
        public void InnerDispHwnd(uint DevNo, IntPtr lHwnd)
        {
            MVC_SetParameter(DevNo, MVC_PARAMTER.MVCDISP_HWND, (uint)lHwnd);
        }

        //内部显示宽度
        public void InnerDispWidth(uint DevNo, uint lWidth)
        {
            MVC_SetParameter(DevNo, MVC_PARAMTER.MVCDISP_WIDTH, lWidth);
        }

        //内部显示宽度
        public void InnerDispHeight(uint DevNo, uint lHeight)
        {
            MVC_SetParameter(DevNo, MVC_PARAMTER.MVCDISP_HEIGHT, lHeight);
        }

        //内部显示X起始位置
        public void InnerDispPosX(uint DevNo, uint lPosX)
        {
            MVC_SetParameter(DevNo, MVC_PARAMTER.MVCDISP_POSX, lPosX);
        }

        //内部显示Y起始位置
        public void InnerDispPosY(uint DevNo, uint lPosY)
        {
            MVC_SetParameter(DevNo, MVC_PARAMTER.MVCDISP_POSY, lPosY);
        }

        //自动配置IP
        public void AutoIPConfig(uint DevNo)
        {
            MVC_AutoIPConfig(DevNo);
        }

        //获取设备类型
        public MVC_TYPE GetCameraType(uint DevNo)
        {
            MVC_TYPE DEV_TYPE;
            DEV_TYPE = MVC_GetCameraType(DevNo);
            return DEV_TYPE;
        }

        // 打开设备
        public uint OpenDev(uint DevNo)
        {
            uint state;
            state = MVC_OpenDevice(DevNo);
            return state;
        }

        //关闭设备
        public void CloseDev(uint DevNo)
        {
            MVC_CloseDevice(DevNo);
        }

        //采集打开
        public void EnableCapture(uint DevNo)
        {
            MVC_EnableCapture(DevNo);
        }

        //采集关闭
        public void DisableCapture(uint DevNo)
        {
            MVC_DisableCapture(DevNo);
        }

        //设置设备参数
        public void SetDevParameter(uint DevNo, MVC_PARAMTER Oper, uint Val)
        {
            MVC_SetParameter(DevNo, Oper, Val);
        }

        //获取设备参数
        public uint GetDevParameter(uint DevNo, MVC_PARAMTER Oper)
        {
            uint lRes = 0;
            uint lVal = 0;
            lRes = MVC_GetParameterEx(DevNo, Oper, ref lVal);
            if (lRes == 0)
            {
                return lVal;
            }
            else
            {
                return 0;
            }
        }

        //自动白平衡
        public void AutoBalance(uint DevNo)
        {
            MVC_AutoWhiteBalance(DevNo);
        }

        //设置巨帧
        public void SetJumbo(uint DevNo, bool lJumbo)
        {
            MVC_SetNetPacketSize(DevNo, (uint)(lJumbo ? 8000 : 1440));
        }

        //回调显示初始化
        public void DisplayInit(uint DevNo, uint CamNo, RECT rect, IntPtr lHwnd)
        {
            MVC_SubDisplayInit(DevNo, CamNo, rect, lHwnd, 0);
        }

        //回调显示显示
        public void DisplayDisp(uint DevNo, uint CamNo, IntPtr buffer, uint Width, uint Height)
        {
            MVC_SubDisplayDisp(DevNo, CamNo, buffer, Width, Height);
        }

        //回调显示关闭
        public void DisplayClose(uint DevNo, uint CamNo)
        {
            MVC_SubDisplayClose(DevNo, CamNo);
        }

        //注册回调函数
        public void CapCallBack(uint DevNo, CallbackFun callback, IntPtr pUserData)
        {
            MVC_SetStreamHOOK(DevNo, callback, pUserData);
        }

        //保存图像数据
        public void SaveFrameInfo(uint DevNo, ref MVCFRAMEINFO FrameInfo, FILE_FORMT FileForm, string FileName)
        {
            MVC_SaveFile(DevNo, FileForm, ref FrameInfo, FileName, false, true, 100, false);
        }

        //转换图像数据
        public void ConvertFrameInfo(uint DevNo, ref MVCFRAMEINFO InFrameInfo, ref MVCFRAMEINFO OutFrameInfo)
        {
            MVC_PixelConverter(DevNo, ref InFrameInfo, ref OutFrameInfo, true);
        }

        //获取采集状态
        public double GetFPS(uint DevNo)
        {
            MVCDEVSTATE lState = new MVCDEVSTATE();
            MVC_GetDeviceStat(DevNo, ref lState);
            return lState.CAP_FPS;
        }

        //设置存图路径
        public void SetSavePath(string lPath)
        {
            m_sFileLocation = lPath + "\\";
        }

        //获取存图路径
        public string GetSavePath()
        {
            return m_sFileLocation;
        }

        //设置显示模式
        public void SetDisplayMode(int lMode)
        {
            m_bDispMode = lMode;
        }

        //获取显示模式
        public int GetDisplayMode()
        {
            return m_bDispMode;
        }

        //单帧存图
        public void SaveImgOnce()
        {
            m_bSaveflag = true;
        }

        //声明回调托管类型
        public delegate void CallbackFun(uint DevNo, MVCFRAMEINFO m_FrameInfo, IntPtr pUserData);

        //实例化回调对象
        public static CallbackFun framecallback = new CallbackFun(StreamFun);

        //定义回调函数实体
        public static void StreamFun(uint DevNo, MVCFRAMEINFO m_FrameInfo, IntPtr pUserData)
        {
            //转换实例
            GCHandle gch = GCHandle.FromIntPtr(pUserData);
            MVCGE lGEV = (MVCGE)gch.Target;

            //图像显示
            if (lGEV.GetDisplayMode() == 2 || lGEV.GetDisplayMode() == 3)
            {
                lGEV.DisplayDisp(DevNo, 0, m_FrameInfo.IBufPtr, m_FrameInfo.Width, m_FrameInfo.Height);
            }
            //图像保存
            if (lGEV.m_bSaveflag)
            {
                Debug.WriteLine("Come to Save a Image!");
                if (lGEV.m_bColor)
                {
                    MVCFRAMEINFO lRgbFrame = new MVCFRAMEINFO();
                    lRgbFrame.PixelID = 24;
                    lGEV.ConvertFrameInfo(DevNo, ref m_FrameInfo, ref lRgbFrame);
                    lGEV.SaveFrameInfo(DevNo, ref lRgbFrame, FILE_FORMT.FILE_BMP, lGEV.m_sFileLocation + "DEV#" + DevNo + "_RGB_" + m_FrameInfo.FRAMEID + ".bmp");
                    lGEV.m_bSaveflag = false;
                }
                else
                {
                    lGEV.SaveFrameInfo(DevNo, ref m_FrameInfo, FILE_FORMT.FILE_BMP, lGEV.m_sFileLocation + "DEV#" + DevNo + "_BW_" + m_FrameInfo.FRAMEID + ".bmp");
                    lGEV.m_bSaveflag = false;
                }
            }
        }
    }
}
