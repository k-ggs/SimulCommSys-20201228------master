using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using System.IO.Ports;

namespace SimulCommSys
{
    public class ModbusOperate
    {



        #region Old
        //        IModbusMaster master;
        //        byte deviceAddr = 1;
        //        // ushort pvAddr = 0x1001;
        //        // ushort spAddr = 0x0000;
        //        /// <summary>
        //        /// modbus操作类  
        //        /// </summary>
        //        /// <param name="slaveId"></param>
        //        /// <param name="master"></param>
        //        //public ModbusOperate(byte deviceAddr, ModbusSerialMaster master)
        //        //{
        //        //    this.deviceAddr = deviceAddr;
        //        //    this.master = master;
        //        //}


        //        #region
        //        /// <summary>
        //        /// chen--
        //        /// </summary>
        //        /// <param name="pvAddr"></param>
        //        /// <param name="numberofpoint"></param>
        //        /// <returns></returns>
        //        public double GetCurrentValue(ushort pvAddr, ushort numberofpoint)
        //        {
        //            //ushort[] value = master.ReadHoldingRegisters(deviceAddr, pvAddr, numberofpoint);
        //            //double ret = value[0] / 10.0;

        //            //return ret * 1000 / 60.0;

        //            ushort[] value = master.ReadHoldingRegisters(deviceAddr, pvAddr, 1);
        //            double ret = value[0] / 10.0;

        //            return ret;

        //        }
        //        /// <summary>
        //        /// chen--
        //        /// </summary>
        //        /// <param name="spAddr"></param>
        //        /// <param name="numberofpoint"></param>
        //        /// <returns></returns>
        //        public double GetSetPointValue(ushort spAddr, ushort numberofpoint)
        //        {
        //            //ushort[] value = master.ReadHoldingRegisters(deviceAddr, spAddr, 1);
        //            //double ret = value[0] / 10.0;
        //            //return ret * 1000 / 60.0;

        //            ushort[] value = master.ReadHoldingRegisters(deviceAddr, spAddr, 1);
        //            double ret = value[0] / 10.0;
        //            return ret;
        //        }

        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="fvalue"></param>
        //        public void SetSetPointValued(ushort spAddr, float fvalue)
        //        {
        //            double setValue = fvalue * 60 / 1000.0;
        //            int value = (int)(setValue * 10);
        //            //int value = (int)(fvalue * 10);
        //            master.WriteSingleRegister(deviceAddr, spAddr, (ushort)value);


        //            //int value = (int)(fvalue * 10);
        //            //master.WriteSingleRegister(deviceAddr, spAddr, (ushort)value);
        //        }
        //        #endregion



        //        /// <summary>
        //        ///Modbus开关控制 05
        //        /// </summary>
        //        /// <param name="coiladress"></param>
        //        /// <param name="bol"></param>
        //        public void Switch(ushort coiladress, bool bol)
        //        {
        //            try
        //            {
        //                master.WriteSingleCoil(deviceAddr, coiladress, bol);
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }



        //        /* GlobalVariable.Master.Transport.Retries = 0;
        //             GlobalVariable.Master.Transport.ReadTimeout = 1000;


        //                    //读取单个线圈

        //                     GlobalVariable.coilsBuffer = GlobalVariable.Master.ReadCoils(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].LENTH.ToString()));

        //                   //读取输入线圈/离散量线圈
        //                     GlobalVariable.coilsBuffer = GlobalVariable.Master.ReadInputs(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].LENTH.ToString()));

        //                   //读取保持寄存器

        //                     GlobalVariable.registerBuffer = GlobalVariable.Master.ReadHoldingRegisters(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].LENTH.ToString()));

        //                    //读取输入寄存器

        //                     GlobalVariable.registerBuffer = GlobalVariable.Master.ReadInputRegisters(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].LENTH.ToString()));


        //                    //写单个线圈

        //                     await GlobalVariable.Master.WriteSingleCoilAsync(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), GlobalVariable.coilsBuffer[0]);

        //                    //写单个输入线圈/离散量线圈

        //                     await GlobalVariable.Master.WriteSingleRegisterAsync(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), GlobalVariable.registerBuffer[0]);

        //                    //写一组线圈

        //                     await GlobalVariable.Master.WriteMultipleCoilsAsync(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), GlobalVariable.coilsBuffer);

        //                   ://写一组保持寄存器

        //                     await GlobalVariable.Master.WriteMultipleRegistersAsync(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), GlobalVariable.registerBuffer);









        // //读线圈,参数:从站地址 (8位 ) 、起始地址 (16位 ) 、数量 (16位 ) ;返回布尔型数 组

        //bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints);

        ////读输入离散量,参数:从站地址 (8位 ) 、起始地址 (16位 ) 、数量 (16位 ) ;返回布 尔型数组

        //bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints);

        ////读保持寄存器,参数:从站地址 (8位 ) 、起始地址 (16位 ) 、数量 (16位 ) ;返回 16位整型数组

        //ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);

        ////读输入寄存器,参数:从站地址 (8位 ) 、起始地址 (16位 ) 、数量 (16位 ) ;返回 16位整型数组

        //ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);

        ////写单个线圈,参数:从站地址 (8位 ) ,线圈地址 (16位 ) ,线圈值 (布尔型 )
        //void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value);

        ////写单个寄存器,参数:从站地址 (8位 ) ,寄存器地址 (16位 ) ,寄存器值 (16位 )
        //void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value);

        ////写多个寄存器,参数:从站地址 (8位 ) ,起始地址 (16位 ) ,寄存器值 (16位整型 数组 )

        //void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data);

        ////写多个线圈,参数:从站地址 (8位 ) ,起始地址 (16位 ) ,线圈值 (布尔型数组 ) 
        //void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data);

        ////读写多个寄存器,参数:从站地址 (8位 ) ,读起始地址 (16位 ) ,数量 (16位 ) ,写 起始地址 (16位 ) ,写入值 (16位整型数组 ) ;返回 16位整型数组

        //ushort[] ReadWriteMultipleRegisters(byte slaveAddress, ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData);

        //6、使用 catch 语句捕捉异常:

        //如果执行没有出错,则不抛出异常,如果是执行读操作则能得到相应的 返回值。

        //响应超时会抛出 TimeoutException 类型的异常 ;

        //接收到从站的异常响应时会抛出 SlaveException 类型的异常, 这一类异常 包含 SlaveExceptionCode 属性,即异常码,通过判断异常码能得知出错的原因。






        //NModbus Sample Code







        // Simple Modbus serial RTU master write holding registers example.


        //using (SerialPort port = new SerialPort("COM1"))



        //{

        //// configure serial port



        //port.BaudRate = 9600;


        //port.DataBits = 8;

        //port.Parity = Parity.None;



        //port.StopBits = StopBits.One;

        //port.Open();

        //// create modbus master

        //IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

        //byte slaveID = 1;

        //ushort startAddress = 100;

        //ushort[] registers = new ushort[] { 1, 2, 3 };

        //// write three registers

        //master.WriteMultipleRegisters(slaveID, startAddress, registers);

        //}

        // Simple Modbus serial ASCII master read holding registers example.

        //using (SerialPort port = new SerialPort("COM1"))

        //{
        //// configure serial port

        //port.BaudRate = 9600;

        //port.DataBits = 8;

        //port.Parity = Parity.None;

        //port.StopBits = StopBits.One;

        //port.Open();

        //// create modbus master

        //IModbusSerialMaster master = ModbusSerialMaster.CreateAscii(port);

        //byte slaveID = 1;

        //ushort startAddress = 1;

        //ushort numRegisters = 5;

        //// read five registers

        //ushort[] registers = master.ReadHoldingRegisters(slaveID, startAddress, numRegisters);

        //for (int i = 0; i < numRegisters; i++)

        //Console.WriteLine("Register {0}={1}", startAddress + i, registers[i]);

        //}

        //// output: 

        //// Register 1=0

        //// Register 2=0

        //// Register 3=0

        //// Register 4=0

        //// Register 5=0

        // Simple Modbus serial USB RTU master write multiple coils example.

        //using (FtdUsbPort port = new FtdUsbPort(0))

        //{
        //// configure usb port

        //port.BaudRate = 9600;

        //port.DataBits = 8;

        //port.Parity = FtdParity.None;

        //port.StopBits = FtdStopBits.One;

        //port.Open();

        //// create modbus master

        //IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

        //byte slaveID = 1;

        //ushort startAddress = 1;

        //// write three coils

        //master.WriteMultipleCoils(slaveID, startAddress, new bool[] { true, false, true });

        //}






        // Simple Modbus serial USB ASCII master write multiple coils example.



        //using (FtdUsbPort port = new FtdUsbPort(0))


        //{


        //// configure usb port


        //port.BaudRate = 9600;



        //port.DataBits = 8;




        //port.Parity = FtdParity.None;

        //port.StopBits = FtdStopBits.One;

        //port.Open();

        //// create modbus master

        //IModbusSerialMaster master = ModbusSerialMaster.CreateAscii(port);

        //byte slaveID = 1;

        //ushort startAddress = 1;

        //// write three coils

        //master.WriteMultipleCoils(slaveID, startAddress, new bool[] { true, false, true });

        //}








        //           */






        //        ////功能码
        //        //public string functionCode;
        //        ////参数(分别为站号,起始地址,长度)
        //        //public byte slaveAddress;
        //        //public ushort startAddress;
        //        //public ushort numberOfPoints;

        #endregion

        private IModbusMaster master;

        public enum funtioncode
        {
            ReadCoils_x01,
            ReadInputs02,
            ReadHoldingRegisters_x03,
            ReadInputRegisters_x04,
            WriteSingleCoilAsync_x05,
            WriteSingleRegisterAsync_x06,
            WriteMultipleCoilsAsync_x0F,

            WriteMultipleRegistersAsync_x10
        }

        /// <summary>
        ///异步方法
        /// </summary>
        /// <param name="functionCode"></param>
        /// <param name="slaveAddress"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        public async void ExecuteFunction(funtioncode functionCode, int slaveAddress, int startAddress, int numberOfPoints)
        {
            try
            {
                globalvariabel.Modbus_addr Modbus_addr = new globalvariabel.Modbus_addr
                {
                  //  functionCode = functionCode.ToString().PadLeft(2, '0'),
                    slaveAddress = byte.Parse(slaveAddress.ToString()),
                    startAddress = ushort.Parse(startAddress.ToString()),
                    numberOfPoints = ushort.Parse(numberOfPoints.ToString()),


                };


                master = ModbusSerialMaster.CreateRtu(globalvariabel.port);
               
                //每次操作是要开启串口 操作完成后需要关闭串口
                //目的是为了slave更换连接是不报错
                if (globalvariabel.port.IsOpen == false)
                {
                    #region 开启串口
                    try
                    {
                        Inifile ini = new Inifile();
                        globalvariabel.ini_Port.protName = ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\config.ini", "port", "protName", "COM1");
                        globalvariabel.ini_Port.baudRate = Convert.ToInt16(ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\config.ini", "port", "baudRate", "COM1"));


                        globalvariabel.ini_Port.parity = (Parity)Convert.ToInt16(ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\config.ini", "port", "parity", "COM1"));
                        globalvariabel.ini_Port.dataBits = Convert.ToInt16(ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\config.ini", "port", "dataBits", "COM1"));
                        globalvariabel.ini_Port.stopBits = (StopBits)Convert.ToInt16(ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\config.ini", "port", "stopBits", "COM1"));


                        globalvariabel.port = new SerialPort(globalvariabel.ini_Port.protName, globalvariabel.ini_Port.baudRate, globalvariabel.ini_Port.parity, globalvariabel.ini_Port.dataBits, globalvariabel.ini_Port.stopBits = StopBits.One);

                        globalvariabel.port.Open();
                        master = ModbusSerialMaster.CreateRtu(globalvariabel.port);
                     

                        globalvariabel.Record_except.Msg(globalvariabel.ini_Port.protName + "串口重连成功" , Record_except.msgtype.sucess);

                    }
                    catch (Exception ex)
                    {
                        globalvariabel.Record_except.Msg(globalvariabel.ini_Port.protName + "串口连接失败" + ex.Message, Record_except.msgtype.error);
                        

                    }
                    #endregion


                   
                }
                //重连次数
                master.Transport.Retries = globalvariabel.Retries;
                master.Transport.ReadTimeout = globalvariabel.ReadTimeout;

                switch (functionCode)
                    {
                        case funtioncode.ReadCoils_x01://读取单个线圈"01 Read Coils"

                            globalvariabel.coilsBuffer  = master.ReadCoils(Modbus_addr.slaveAddress, Modbus_addr.startAddress, Modbus_addr.numberOfPoints);

                         
                            break;
                        case funtioncode.ReadInputs02://读取输入线圈/离散量线圈"02 Read DisCrete Inputs"


                            globalvariabel.coilsBuffer = master.ReadInputs(Modbus_addr.slaveAddress, Modbus_addr.startAddress, Modbus_addr.numberOfPoints);
                        
                            break;
                        case funtioncode.ReadHoldingRegisters_x03://读取保持寄存器"03 Read Holding Registers"

                            globalvariabel.registerBuffer = master.ReadHoldingRegisters(Modbus_addr.slaveAddress, Modbus_addr.startAddress, Modbus_addr.numberOfPoints);
                        
                            break;
                        case funtioncode.ReadInputRegisters_x04://读取输入寄存器"04 Read Input Registers"

                            globalvariabel.registerBuffer = master.ReadInputRegisters(Modbus_addr.slaveAddress, Modbus_addr.startAddress, Modbus_addr.numberOfPoints);
                         
                           
                            break;
                        case funtioncode.WriteSingleCoilAsync_x05://写单个线圈"05 Write Single Coil"

                            await master.WriteSingleCoilAsync(Modbus_addr.slaveAddress, Modbus_addr.startAddress, globalvariabel.coilsBuffer[0]);
                            break;
                        case funtioncode.WriteSingleRegisterAsync_x06://写单个输入线圈/离散量线圈"06 Write Single Registers"

                            await master.WriteSingleRegisterAsync(Modbus_addr.slaveAddress, Modbus_addr.startAddress, globalvariabel.registerBuffer[0]);
                            break;
                        case funtioncode.WriteMultipleCoilsAsync_x0F://写一组线圈"0F Write Multiple Coils"

                            await master.WriteMultipleCoilsAsync(Modbus_addr.slaveAddress, Modbus_addr.startAddress, globalvariabel.coilsBuffer);
                            break;
                        case funtioncode.WriteMultipleRegistersAsync_x10://写一组保持寄存器 "10 Write Multiple Registers"

                            await master.WriteMultipleRegistersAsync(Modbus_addr.slaveAddress, Modbus_addr.startAddress, globalvariabel.registerBuffer);
                            break;
                        default:
                            break;
                    }

                
               
                
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.ToString(), Record_except.msgtype.error);

                if (globalvariabel.port != null&&globalvariabel.port.IsOpen)
                {
                    
                    globalvariabel.port.Close();
                    globalvariabel.port.Dispose();
                }
            }
        }





        /// <summary>
        /// 获取启停状态
        /// </summary>
        public bool GetSwitchSta(funtioncode functionCode, int slaveAddress, int startAddress, int numberOfPoints)
        {

            try
            {
                globalvariabel.ModbusOperate.ExecuteFunction(functionCode, slaveAddress, startAddress, numberOfPoints);
                return globalvariabel.coilsBuffer[0];
            }
            catch (Exception ex)
            {
                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                
                return false;
            }

          
        }




        /// <summary>
        /// 获取初始值
        /// </summary>
        /// <returns></returns>
        public bool Get_iniValue()
        {
            try
            {
               


                #region 开启串口
                try
                {
                    Inifile ini = new Inifile();
                    globalvariabel.ini_Port.protName = ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString()+@"\config.ini", "port", "protName","COM1");
                    globalvariabel.ini_Port.baudRate =Convert.ToInt16( ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\config.ini", "port", "baudRate", "COM1")) ;

                   
                    globalvariabel.ini_Port.parity = (Parity)Convert.ToInt16(ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\config.ini", "port", "parity", "COM1"));
                    globalvariabel.ini_Port.dataBits = Convert.ToInt16(ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\config.ini", "port", "dataBits", "COM1"));
                    globalvariabel.ini_Port.stopBits =(StopBits)Convert.ToInt16(ini.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\config.ini", "port", "stopBits", "COM1")) ;

                  
                    globalvariabel.port = new SerialPort(globalvariabel.ini_Port.protName, globalvariabel.ini_Port.baudRate, globalvariabel.ini_Port.parity, globalvariabel.ini_Port.dataBits, globalvariabel.ini_Port.stopBits = StopBits.One);

                    globalvariabel.port.Open();

                }
                catch (Exception ex)
                {
                    globalvariabel.Record_except.Msg(globalvariabel.ini_Port.protName + "串口连接失败" + ex.Message, Record_except.msgtype.error);
                    return false;

                }
                #endregion


                #region 获取开关状态量


                //globalvariabel.Model_Person.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 1, 0, 1);//人的启停状态
                //globalvariabel.Model_BoreGUN.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 2, 0, 1);//线膛炮
                //globalvariabel.Model_RocketGUN.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 3, 0, 1);//火箭炮
                //globalvariabel.Model_Tank.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 4, 0, 1);//坦克
                //globalvariabel.Model_CarryCar.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 5, 0, 1);//运输车

                //globalvariabel.Car_NormLight.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 6, 0, 1);//可见光车
                //globalvariabel.Car_red.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 7, 0, 1);//红外车
                //globalvariabel.Car_LitterLight.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 8, 0, 1);//微光车
                //globalvariabel.Car_Fly.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 9, 0, 1);//飞行器
                

                //globalvariabel.infrared_mod1.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 10, 0, 1);//红外1
                //globalvariabel.infrared_mod2.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 11, 0, 1);//红外2
                //globalvariabel.infrared_mod3.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 12, 0, 1);//红外3
                //globalvariabel.infrared_mod4.Move = globalvariabel.ModbusOperate.GetSwitchSta(funtioncode.ReadCoils_x01, 13, 0, 1);//红外4
               
              
                #endregion



                globalvariabel.Record_except.Msg(globalvariabel.ini_Port.protName.ToString() + "连接成功!..", Record_except.msgtype.sucess);
                return true;
            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.ToString(), Record_except.msgtype.error);
                return false;
            }

        }






      
    }
}
