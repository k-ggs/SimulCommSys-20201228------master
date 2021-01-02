using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys.Tool
{
    public partial class Serial
    {
        private SerialPort comm = new SerialPort();
        public StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。接收到的数据
        private long receive_count = 0;//接收计数
        private long send_count = 0;//发送计数
        private bool saveflag = false;//保存报文标志
        public bool HexFlag = false;
        private byte[] buffer = new byte[1024];
        //private string[] ports;
        //private int receive
        public Serial()
        {
            //初始化SerialPort对象
            comm.NewLine = "\r\n";
            comm.RtsEnable = true;//根据实际情况吧。
            receive_count = 0;
            send_count = 0;
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            //添加事件注册
            comm.DataReceived += comm_DataReceived;
        }

        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
            receive_count += n;
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
            receive_count += n;//增加接收计数
            comm.Read(buf, 0, n);//读取缓冲数据
                                 //builder.Clear();//清除字符串构造器的内容
            if (HexFlag)
            {
                foreach (byte b in buf)
                {
                    //依次的拼接出16进制字符串
                    builder.Append(b.ToString("X2") + "");
                    //Console.WriteLine(b.ToString("X2") + "");
                }
            }
            else
            {
                //直接按ASCII规则转换成字符串
                builder.Append(Encoding.ASCII.GetString(buf));
            }
        }

        public void removestr(int len)
        {
            builder.Remove(0, len);
        }

        public void Close()
        {
            //根据当前串口对象，来判断操作
            if (comm.IsOpen)
            {
                //打开时点击，则关闭串口
                comm.Close();
            }
        }
        public void Open(string PortName, string BaudRate, int STOPBIT, int PARITY)
        {
            comm.PortName = PortName;
            comm.BaudRate = int.Parse(BaudRate);
            comm.StopBits = (StopBits)STOPBIT;
            comm.Parity = (Parity)PARITY;
            try
            {
                if (!comm.IsOpen)
                {
                    comm.Open();
                }
            }
            catch (Exception ex)
            {
                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                //comm = new SerialPort();
                //现实异常信息给客户。
                //writeLogFile(ex.Message);
            }
        }

        public int Send(string sendstr, bool HexFlag)
        {
            int n = 0;
            if (!comm.IsOpen)
                return 0;
            byte[] buf = new byte[sendstr.Length / 2];
            if (HexFlag)
            {
                buf = HexStringToByteArray(sendstr);
                //转换列表为数组后发送
                comm.Write(buf, 0, buf.Length);
                //记录发送的字节数
                n = buf.Length;
            }
            else
            {
                comm.Write(sendstr);
                n = sendstr.Length;
                /*
                byte[] buff = new byte[sendstr.Length];//
                buff = Encoding.ASCII.GetBytes(sendstr);
                comm.Write(buff, 0, buff.Length);
                n = sendstr.Length;
                */
            }

            return n;
        }




        public async void sendasync(string sendstr, bool HexFlag)
        {
            int n = 0;
            if (!comm.IsOpen)
                return;
            byte[] buf = new byte[sendstr.Length / 2];
            if (HexFlag)
            {
                // buf = HexStringToByteArray(sendstr);
                buf = GetWriteCommandByte(sendstr);
                //转换列表为数组后发送
                //  comm.Write(buf, 0, buf.Length);
                await new TaskFactory().StartNew(() => { comm.Write(buf, 0, buf.Length); });
                //记录发送的字节数
                n = buf.Length;
            }
            else
            {
                await new TaskFactory().StartNew(() => { comm.Write(sendstr); });
                n = sendstr.Length;
                /*
                byte[] buff = new byte[sendstr.Length];//
                buff = Encoding.ASCII.GetBytes(sendstr);
                comm.Write(buff, 0, buff.Length);
                n = sendstr.Length;
                */
            }

            // return n;
        }
        private byte[] HexStringToByteArray(string strHexString)
        {
            string s = strHexString.Replace(" ", "");
            int len = s.Length;
            if ((len % 2) != 0)

            { return null; }   //writeLogFile("");
            int byteLen = len / 2;
            byte[] bytes = new byte[byteLen];
            for (int i = 0; i < byteLen; i++)
            {
                bytes[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
            }
            return bytes;
        }





        /// <summary>
        /// 将字符串表示的16进制数装换为十六进制数
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 直接将字符串装换为16进制数
        /// </summary>
        /// <param name="DATA"></param>
        /// <returns></returns>
        public static byte[] stringToHexByte(string str)
        {
            List<byte> bt = new List<byte>();
            for (int i = 0; i < str.Length; i++)
            {
                bt.Add(Convert.ToByte(str[i]));
            }
            return bt.ToArray();
        }

        public static long GetModBusCRC(string DATA)
        {
            long functionReturnValue = 0;

            long i = 0;
            long J = 0;
            byte[] v = null;
            v = strToToHexByte(DATA);

            //1.预置1个16位的寄存器为十六进制FFFF（即全为1）：称此寄存器为CRC寄存器；
            long CRC = 0;
            CRC = 0xffffL;
            for (i = 0; i <= (v).Length - 1; i++)
            {
                //2.把第一个8位二进制数据（既通讯信息帧的第一个字节）与16位的CRC寄存器的低8位相异或，把结果放于CRC寄存器；
                CRC = (CRC / 256) * 256L + (CRC % 256L) ^ v[i];
                for (J = 0; J <= 7; J++)
                {
                    //3.把CRC寄存器的内容右移一位（朝低位）用0填补最高位，并检查最低位；
                    //4.如果最低位为0：重复第3步（再次右移一位）；
                    // 如果最低位为1：CRC寄存器与多项式A001（1010 0000 0000 0001）进行异或；
                    //5.重复步骤3和4，直到右移8次，这样整个8位数据全部进行了处理；
                    long d0 = 0;
                    d0 = CRC & 1L;
                    CRC = CRC / 2;
                    if (d0 == 1)
                        CRC = CRC ^ 0xa001L;

                }

                //6.重复步骤2到步骤5，进行通讯信息帧下一字节的处理；
            }

            //7.最后得到的CRC寄存器内容即为：CRC码。
            CRC = CRC % 65536;
            functionReturnValue = CRC;
            return functionReturnValue;
        }

        public static long GetModBusCRC(byte[] bData)
        {
            long functionReturnValue = 0;

            long i = 0;
            long J = 0;
            byte[] v = null;
            v = bData;

            //1.预置1个16位的寄存器为十六进制FFFF（即全为1）：称此寄存器为CRC寄存器；
            long CRC = 0;
            CRC = 0xffffL;
            for (i = 0; i <= (v).Length - 1; i++)
            {
                //2.把第一个8位二进制数据（既通讯信息帧的第一个字节）与16位的CRC寄存器的低8位相异或，把结果放于CRC寄存器；
                CRC = (CRC / 256) * 256L + (CRC % 256L) ^ v[i];
                for (J = 0; J <= 7; J++)
                {
                    //3.把CRC寄存器的内容右移一位（朝低位）用0填补最高位，并检查最低位；
                    //4.如果最低位为0：重复第3步（再次右移一位）；
                    // 如果最低位为1：CRC寄存器与多项式A001（1010 0000 0000 0001）进行异或；
                    //5.重复步骤3和4，直到右移8次，这样整个8位数据全部进行了处理；
                    long d0 = 0;
                    d0 = CRC & 1L;
                    CRC = CRC / 2;
                    if (d0 == 1)
                        CRC = CRC ^ 0xa001L;

                }

                //6.重复步骤2到步骤5，进行通讯信息帧下一字节的处理；
            }

            //7.最后得到的CRC寄存器内容即为：CRC码。
            CRC = CRC % 65536;
            functionReturnValue = CRC;
            return functionReturnValue;
        }

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 获取modbus命令（增加CRC校验码）
        /// </summary>
        /// <param name="comand">modbus命令</param>
        /// <returns>modbus命令+RC校验码</returns>
        public static string GetWriteCommandString(string comand)
        {
            string fullCmd = comand;
            string crcHigh, crcLow;
            //获取CRC校验
            long crc = GetModBusCRC(fullCmd);
            string strCRC = crc.ToString("X4");
            crcHigh = strCRC.Substring(0, 2);
            crcLow = strCRC.Substring(2, 2);
            fullCmd = fullCmd + crcLow + crcHigh;
            return fullCmd;
        }

        /// <summary>
        /// 获取modbus命令（增加CRC校验码）
        /// </summary>
        /// <param name="command">modbus命令</param>
        /// <returns>modbus命令+RC校验码</returns>
        public static byte[] GetWriteCommandByte(string command)
        {
            string strCMD = GetWriteCommandString(command);
            return strToToHexByte(strCMD);
        }

        /// <summary>
        /// 对接收到的数据进行校验，判断是否出现传输错误
        /// </summary>
        /// <param name="receive">接收到的数据</param>
        /// <returns>是否出现错误</returns>
        public static bool CheckReceive(string receive)
        {
            //截取modbus命令和CRC校验码
            string cmd = receive.Substring(0, receive.Length - 4);
            string recCRC = receive.Substring(receive.Length - 4, 4);
            string CRC = GetModBusCRC(cmd).ToString("X4");
            //对比CRC校验码
            if (recCRC.Substring(0, 2) == CRC.Substring(2, 2) && recCRC.Substring(2, 2) == CRC.Substring(0, 2))
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        public static bool CheckReceive(byte[] receive)
        {
            //截取modbus命令和CRC校验码
            byte[] cmd = new byte[receive.Length - 2];
            Array.Copy(receive, cmd, receive.Length - 2);
            string recCRC;
            recCRC = GetModBusCRC(cmd).ToString("X4");
            //对比CRC校验码
            if (recCRC.Substring(0, 2) == receive[receive.Length - 1].ToString("X2") && recCRC.Substring(2, 2) == receive[receive.Length - 2].ToString("X2"))
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        #region 写log文件
        //public int writeLogFile(string str)
        //{
        //    string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\run.log";
        //    if (File.Exists(filePath))
        //    {
        //        FileInfo fileInfo = new FileInfo(filePath);
        //        if (fileInfo.Length > 2000000)
        //            File.Delete(filePath);
        //    }

        //    FileStream aFile = new FileStream(filePath, FileMode.OpenOrCreate | FileMode.Append);
        //    StreamWriter sw = new StreamWriter(aFile);
        //    DateTime tt = DateTime.Now;
        //    str = "[" + tt.ToString() + "] " + str;
        //    sw.WriteLine(str);
        //    sw.Close();
        //    aFile.Close();
        //    return 1;
        //}
        #endregion
    }
}
