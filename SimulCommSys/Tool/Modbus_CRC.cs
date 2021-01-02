using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys.Tool
{
  public  class Modbus_CRC
    {
       
        
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
        }
    
}
