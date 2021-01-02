using AForge.Video.DirectShow;
using SimulCommSys;
using SimulCommSys.Tool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimulCommSys
{

    public class camera_operate : NotificationObject
    {
        public string SN = "193300025";
        public ArenaNET.IDevice m_connectedDevice = null;
        public ArenaNET.IImage m_converted;

        public ArenaNET.IImage image = null;
        public ArenaNET.INodeMap nodeMap;
        public ArenaNET.INode node_TEMPERATURE;
        public IntPtr callback_TEMPERATURE;

        const int IMG_SLEEP = 20;
        // Width and height
        const UInt32 WIDTH = 4000;
        const UInt32 HEIGHT = 2000;

        // Pixel format
        const String PIXEL_FORMAT = "Mono8";

        Thread enumerationThread;
        public UInt32 g_subnetMask = 0;
        // mutex for threading
        private static Mutex g_deviceMutex = new Mutex();

        // Maximum packet size
        //    In order to grab images at the maximum packet size, the ethernet
        //    adapter must be configured appropriately: 'Jumbo packet' must be
        //    set to its maximum, 'UDP checksum offload' must be set to 'Rx & Tx
        //    Enabled', and 'Receive Buffers' must be set to its maximum.
        const Boolean MAX_PACKET_SIZE = true;

        // image timeout
        const UInt32 TIMEOUT = 2000;

        private List<ArenaNET.IDeviceInfo> m_deviceInfos;

        private BitmapImage _BitmapImage = new BitmapImage();
        public BitmapImage BtmpImg
        {
            get { return _BitmapImage; }
            set { _BitmapImage = value; this.RaisePropertyChanged("BtmpImg"); }
        }

        private double _cameraTemperature = camertemper;
        public double cameraTemperatu
        {
            get { return this._cameraTemperature; }
            set
            {
                this._cameraTemperature = value;
                this.RaisePropertyChanged("cameraTemperatu");
            }

        }

        public TreeItem TreeItem = new TreeItem();
        public Thread streamThread;
        public Boolean stream = false;
        public void StartStream()
        {
            try
            {
                if (!IsNetworkValid(SN))
                {
                    //ForceNetworkSettings();
                }
                List<String> UIds = GetDeviceUIds();
                //  start_streamcallback();
                ConnectDevice(SN);
                if (stream == false)
                {
                    StartStream_custom();
                    stream = true;

                    // start streaming thread for acquiring images

                    streamThread = new Thread(StreamThread);
                    streamThread.Start();


                    enumerationThread = new Thread(EnumerationThread);
                    enumerationThread.Start();
                    ConfigureCallbackToPollDeviceTemperature();
                }
            }
            catch (Exception ex)
            {
                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                reconn_perpare();
            }

        }


        // thread for grabbing images
        private void StreamThread()
        {
            while (stream)
            {  // sleep thread 33ms so that number of images grabbed per second ~ 30fps
                Thread.Sleep(IMG_SLEEP);

                if (!IsNetworkValid(SN))
                {
                    //ForceNetworkSettings();
                }
                Monitor.Enter(g_deviceMutex);
                // grab image
                try
                {
                    if (m_connectedDevice == null)
                    {
                        // Wait for reconnection if necessary
                        //    When the device has been disconnected, the
                        //    acquisition thread waits on the enumeration thread
                        //    to reconnect the device. This is done through a
                        //    conditional variable. This conditional variable has
                        //    also been set up to stop waiting if the application
                        //    has been terminated.
                        Monitor.Wait(g_deviceMutex);

                        // Restart stream if connected
                        //    Check that the device has been reconnected in case
                        //    the the notification has been sent to quit the
                        //    application. If the device has been reconnected,
                        //    restart the stream so that it can continue
                        //    acquiring images.
                        if (!m_connectedDevice.Equals(null))
                            m_connectedDevice.StartStream();
                    }
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        // if connected to selected tree view device
                        if (DeviceConnected())
                        {
                            nodeMap.Poll(1000);


                            //   ArenaNET.IImage converted = ArenaNET.ImageFactory.Convert(globalvariabel.ArenaManager.GetIImage(), (ArenaNET.EPfncFormat)0x02200017);





                            BtmpImg = GetIImage().Bitmap.ToBitmapImage();



                        }


                    });
                }
                catch (Exception ex)
                {
                    globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                    reconn_perpare();
                }
                Monitor.Exit(g_deviceMutex);

            }
        }

        // grab image from stream to display to window
        public void GetImage()
        {
            try
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    // if connected to selected tree view device
                    if (DeviceConnected())
                    {
                        nodeMap.Poll(1000);


                        //   ArenaNET.IImage converted = ArenaNET.ImageFactory.Convert(globalvariabel.ArenaManager.GetIImage(), (ArenaNET.EPfncFormat)0x02200017);




                        BtmpImg = GetIImage().Bitmap.ToBitmapImage();


                    }


                });
            }
            catch (Exception ex)
            {
                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                reconn_perpare();
            }
        }
        /// <summary>
        /// 清除设备为重连准备
        /// </summary>
        public void reconn_perpare()
        {
            if (m_connectedDevice != null)
            {
                globalvariabel.ArenaManager.m_system.DestroyDevice(m_connectedDevice);

            }
            m_connectedDevice = null;
        }
        /// <summary>
        /// clean up stream and disconnect from device
        /// </summary>
        public void CleanUpStream()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (DeviceConnected())
                {
                    // abort thread and wait for it to stop
                    streamThread.Abort();
                    streamThread.Join();
                    stream = false;


                    m_connectedDevice = null;

                    // set window image source to empty
                    BtmpImg = null;

                }

                // UpdateDeviceList( );
            });
        }

        // stop stream button handler
        public void StopStream()
        {
            if (stream == true)
            {


                // abort thread and wait for it to stop
                streamThread.Abort();
                streamThread.Join();

                // stop stream and set window image source to empty
                stream = false;
                BtmpImg = null;
                // Image.Source = null;
            }
        }


        public void Close()
        {
            // abort thread and wait for it to stop
            try
            {
                StopStream();
                //  streamThread.Abort();
                // streamThread.Join();
            }
            catch
            {
                // do nothing
            }
        }





        /// <summary>
        /// 获取温度
        /// </summary>
        /// <param name="device"></param>
        public void ConfigureCallbackToPollDeviceTemperature()
        {
            try
            {
                if (m_connectedDevice != null)
                {
                    nodeMap = m_connectedDevice.NodeMap;
                }
                else
                { throw new Exception(); }

                //  nodeMap = 
                node_TEMPERATURE = nodeMap.GetNode("DeviceTemperature");

                if (node_TEMPERATURE == null || !node_TEMPERATURE.IsReadable)
                    throw new ArenaNET.GenericException("Node not found/readable");

                // Register callback
                //    Callbacks are registered with a node and a function. This
                //    example demonstrates callbacks being invoked through polling.
                //    This happens when a node map is polled and the polling time is
                //    ready to poll again.


                // create delegate for registering callback
                ArenaNET.INode.del d = new ArenaNET.INode.del(PrintIntegerNodeValue);

                callback_TEMPERATURE = node_TEMPERATURE.RegisterCallback(d);


            }
            catch (Exception ex)
            {
                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                reconn_perpare();
            }



            //  node_TEMPERATURE.DeregisterCallback(callback_TEMPERATURE);
        }

        /// <summary>
        /// 温度
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public void PrintIntegerNodeValue(ArenaNET.INode node)
        {

            ArenaNET.IFloat deviceTemperatureNode = (ArenaNET.IFloat)node;
            cameraTemperatu = deviceTemperatureNode.Value;
            //  Console.Write("{0}  {1} Current device temperature: {2} 167 {3}\r", TAB3, g_count++.ToString().PadRight(2), deviceTemperatureNode.Value, deviceTemperatureNode.Unit);
        }



        public void start_streamcallback()
        {
            List<String> UIds = GetDeviceUIds();
            ConnectDevice(SN);
            ArenaNET.IDevice.del d = new ArenaNET.IDevice.del(CallbackFunction);

            m_connectedDevice.RegisterImageCallback(d);
            if (m_connectedDevice != null)
            {
                if ((m_connectedDevice.TLStreamNodeMap.GetNode("StreamIsGrabbing") as ArenaNET.IBoolean).Value == false)
                {
                    (m_connectedDevice.TLStreamNodeMap.GetNode("StreamBufferHandlingMode") as ArenaNET.IEnumeration).Symbolic = "NewestOnly";
                    //  m_connectedDevice.StartStream();
                }


                var acquisitionModeNode = (ArenaNET.IEnumeration)m_connectedDevice.NodeMap.GetNode("AcquisitionMode");
                String acquisitionModeInitial = acquisitionModeNode.Entry.Symbolic;




                acquisitionModeNode.FromString("Continuous");




                var streamBufferHandlingModeNode = (ArenaNET.IEnumeration)m_connectedDevice.TLStreamNodeMap.GetNode("StreamBufferHandlingMode");
                streamBufferHandlingModeNode.FromString("NewestOnly");



                var streamAutoNegotiatePacketSizeNode = (ArenaNET.IBoolean)m_connectedDevice.TLStreamNodeMap.GetNode("StreamAutoNegotiatePacketSize");
                streamAutoNegotiatePacketSizeNode.Value = true;
                //     var deviceStreamChannelPacketSizeNode =
                //m_connectedDevice.NodeMap.GetNode("DeviceStreamChannelPacketSize") as ArenaNET.IInteger;
                //  Int64 deviceStreamChannelPacketSizeInitial;
                // deviceStreamChannelPacketSizeNode.Value = deviceStreamChannelPacketSizeNode.Max;

                var streamPacketResendEnableNode = (ArenaNET.IBoolean)m_connectedDevice.TLStreamNodeMap.GetNode("StreamPacketResendEnable");
                streamPacketResendEnableNode.Value = true;


                m_connectedDevice.StartStream();
                //获取温度
                ConfigureCallbackToPollDeviceTemperature();
            }
        }


        /// <summary>
        /// 回调事件的形式
        /// </summary>
        /// <param name="image"></param>
        public void CallbackFunction(ArenaNET.IImage image)
        {


            try
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    // if connected to selected tree view device
                    if (DeviceConnected())
                    {
                        nodeMap.Poll(1000);


                        //   ArenaNET.IImage converted = ArenaNET.ImageFactory.Convert(globalvariabel.ArenaManager.GetIImage(), (ArenaNET.EPfncFormat)0x02200017);




                        BtmpImg = image.Bitmap.ToBitmapImage();

                        m_connectedDevice.RequeueBuffer(image);
                        //   ArenaNET.ImageFactory.Destroy(image);
                        //    globalvariabel.ArenaManager.m_connectedDevice.RequeueBuffer(converted);
                    }


                });
            }
            catch (Exception ex)
            {
                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                CleanUpStream();
            }



        }


        // connects to a device based on uid
        public void ConnectDevice(String UId)
        {
            try
            {
                if (m_connectedDevice == null)
                {
                    UpdateDevices();

                    for (int i = 0; i < m_deviceInfos.Count; i++)
                    {
                        if (m_deviceInfos[i].SerialNumber == UId)
                        {
                            m_connectedDevice = globalvariabel.ArenaManager.m_system.CreateDevice(m_deviceInfos[i]);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
            }
        }

        // disconnects for connected device
        public void DisconnectDevice()
        {
            globalvariabel.ArenaManager.m_system.DestroyDevice(m_connectedDevice);
            m_connectedDevice = null;
        }

        // returns if connected to a device
        public bool DeviceConnected()
        {
            return (m_connectedDevice != null);
        }

        // gets uid of connected device
        public String ConnectedDeviceUId()
        {
            return ((ArenaNET.IString)m_connectedDevice.NodeMap.GetNode("DeviceSerialNumber")).Value;
        }

        // gets list of uids of available devices
        public List<String> GetDeviceUIds()
        {
            UpdateDevices();

            List<String> uids = new List<String>();

            for (int i = 0; i < m_deviceInfos.Count; i++)
            {
                uids.Add(m_deviceInfos[i].SerialNumber);
            }

            return uids;
        }

        // gets name of device
        public String GetDeviceName(String UId, String node)
        {
            for (int i = 0; i < m_deviceInfos.Count; i++)
            {
                if (m_deviceInfos[i].SerialNumber == UId)
                {
                    if (node == "DeviceUserID" || node == "UserDefinedName")
                        return m_deviceInfos[i].UserDefinedName;
                    else if (node == "DeviceModelName" || node == "ModelName")
                        return m_deviceInfos[i].ModelName;
                }
            }

            return "Invalid argument";
        }

        // check if ip addresses between device and interface match based on their subnets
        public bool IsNetworkValid(String UId)
        {
            try
            {
                UpdateDevices();

                for (int i = 0; i < m_deviceInfos.Count; i++)
                {
                    if (m_deviceInfos[i].SerialNumber == UId)
                    {
                        UInt32 ip = (UInt32)m_deviceInfos[i].IpAddress;
                        UInt32 subnet = (UInt32)m_deviceInfos[i].SubnetMask;

                        ArenaNET.IInteger ifipNode = (ArenaNET.IInteger)globalvariabel.ArenaManager.m_system
                                .GetTLInterfaceNodeMap(m_deviceInfos[i]).GetNode("GevInterfaceSubnetIPAddress");
                        ArenaNET.IInteger ifsubnetNode = (ArenaNET.IInteger)globalvariabel.ArenaManager.m_system
                                .GetTLInterfaceNodeMap(m_deviceInfos[i]).GetNode("GevInterfaceSubnetMask");
                        UInt32 ifip = (UInt32)ifipNode.Value;
                        UInt32 ifsubnet = (UInt32)ifsubnetNode.Value;

                        if (subnet != ifsubnet)
                            return false;

                        if ((ip & subnet) != (ifip & ifsubnet))
                            return false;

                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                return false;
            }


        }

        // gets image as bitmap

        public System.Drawing.Bitmap GetImage(UInt32 timeout = 2000)
        {
            try
            {


                if (m_converted != null)
                {
                    ArenaNET.ImageFactory.Destroy(m_converted);
                    m_converted = null;
                }

                image = GetIImage(timeout);
                m_converted = ArenaNET.ImageFactory.Convert(image, (ArenaNET.EPfncFormat)0x02200017);

            }
            catch (Exception ex)
            {

                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                return null;
            }
            return m_converted.Bitmap;
        }

        // gets image as ArenaNET.IImage
        public ArenaNET.IImage GetIImage(UInt32 timeout = 2000)
        {
            try
            {
                if (m_converted != null)
                {
                    ArenaNET.ImageFactory.Destroy(m_converted);
                    m_converted = null;
                }

                if (image != null)
                {
                    m_connectedDevice.RequeueBuffer(image);
                    image = null;
                }

                image = m_connectedDevice.GetImage(timeout);
            }

            catch (ArenaNET.TimeoutException ex)
            {
                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                reconn_perpare();
                return null;

            }
            catch (Exception ex)
            {


                globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                reconn_perpare();
                return null;
            }
            return image;
        }
        public void sart_streamAcquireImagesRapidly()
        {
            // get node values that will be changed in order to return their
            // values at the end of the example
            //    var widthNode = (ArenaNET.IInteger)m_connectedDevice.NodeMap.GetNode("Width");
            //   Int64 widthInitial = widthNode.Value;

            //     var heightNode = (ArenaNET.IInteger)m_connectedDevice.NodeMap.GetNode("Height");
            //    Int64 heightInitial = heightNode.Value;

            var pixelFormatNode = (ArenaNET.IEnumeration)m_connectedDevice.NodeMap.GetNode("PixelFormat");
            String pixelFormatInitial = pixelFormatNode.Entry.Symbolic;

            var deviceStreamChannelPacketSizeNode =
                m_connectedDevice.NodeMap.GetNode("DeviceStreamChannelPacketSize") as ArenaNET.IInteger;
            Int64 deviceStreamChannelPacketSizeInitial;





            deviceStreamChannelPacketSizeInitial = deviceStreamChannelPacketSizeNode.Value;

            var exposureAutoNode = (ArenaNET.IEnumeration)m_connectedDevice.NodeMap.GetNode("ExposureAuto");
            String exposureAutoInitial = exposureAutoNode.Entry.Symbolic;

            var exposureTimeNode = m_connectedDevice.NodeMap.GetNode("ExposureTime") as ArenaNET.IFloat;
            double exposureTimeInitial = exposureTimeNode.Value;

            // Set low width and height
            //    Reducing the size of an image reduces the amount of bandwidth
            //    required for each image. The less bandwidth required per image,
            //    the more images can be sent over the same bandwidth.
            // Console.Write("{0}Set low width and height", TAB1);

            Int64 width = SetIntValue(m_connectedDevice.NodeMap, "Width", WIDTH);

            Int64 height = SetIntValue(m_connectedDevice.NodeMap, "Height", HEIGHT);



            // Set small pixel format
            //    Similar to reducing the ROI, reducing the number of bits per
            //    pixel also reduces the bandwidth required for each image. The
            //    smallest pixel formats are 8-bit bayer and 8-bit mono (i.e.
            //    BayerRG8 and Mono8).


            pixelFormatNode.Symbolic = PIXEL_FORMAT;

            // Set maximum stream channel packet size
            //    Maximizing packet size increases frame rate by reducing the
            //    amount of overhead required between images. This includes both
            //    extra header/trailer data per packet as well as extra time from
            //    intra-packet spacing (the time between packets). In order to
            //    grab images at the maximum packet size, the ethernet adapter
            //    must be configured appropriately: 'Jumbo packet' must be set to
            //    its maximum, 'UDP checksum offload' must be set to
            //    'Rx & Tx Enabled', and 'Received Buffers' must be set to its
            //    maximum.
            if (MAX_PACKET_SIZE)
            {


                if (deviceStreamChannelPacketSizeNode.Equals(null) || !deviceStreamChannelPacketSizeNode.IsReadable || !deviceStreamChannelPacketSizeNode.IsWritable)
                    throw new ArenaNET.GenericException("DeviceStreamChannelPacketSize node not found/readable/writable");



                deviceStreamChannelPacketSizeNode.Value = deviceStreamChannelPacketSizeNode.Max;
            }

            // Set low exposure time
            //    Decreasing exposure time can increase frame rate by reducing
            //    the amount of time it takes to grab an image. Reducing the
            //    exposure time past certain thresholds can cause problems
            //    related to not having enough light. In certain situations this
            //    can be mitigated by increasing gain and/or environmental light.

            //   exposureAutoNode.FromString("Continuous");
            exposureAutoNode.Symbolic = "Off";

            if (exposureTimeNode.Equals(null) || !exposureTimeNode.IsReadable || !exposureTimeNode.IsWritable)
                throw new ArenaNET.GenericException("ExposureTime node not found/readable/writable");
            if (!MAX_PACKET_SIZE)
            {
                // enable stream auto negotiate packet size
                var streamAutoNegotiatePacketSizeNode = (ArenaNET.IBoolean)m_connectedDevice.TLStreamNodeMap.GetNode("StreamAutoNegotiatePacketSize");
                streamAutoNegotiatePacketSizeNode.Value = true;
            }

            // enable stream packet resend
            var streamPacketResendEnableNode = (ArenaNET.IBoolean)m_connectedDevice.TLStreamNodeMap.GetNode("StreamPacketResendEnable");
            streamPacketResendEnableNode.Value = true;


            exposureTimeNode.Value = exposureTimeNode.Min;
            m_connectedDevice.StartStream();
            //if (!MAX_PACKET_SIZE)
            //{
            //    // enable stream auto negotiate packet size
            //    var streamAutoNegotiatePacketSizeNode = (ArenaNET.IBoolean)device.TLStreamNodeMap.GetNode("StreamAutoNegotiatePacketSize");
            //    streamAutoNegotiatePacketSizeNode.Value = true;
            //}

            // Start stream with high number of buffers
            //    Increasing the number of buffers can increase speeds by
            //    reducing the amount of time taken to requeue buffers. In this
            //    example, one buffer is used for each image. Of course, the
            //    amount of buffers that can be used is limited by the amount of
            //    space in memory.

            //// return nodes to their initial values
            //if (exposureAutoInitial == "Off")
            //    exposureTimeNode.Value = exposureTimeInitial;

            //exposureAutoNode.Symbolic = exposureAutoInitial;

            //if (MAX_PACKET_SIZE)
            //    deviceStreamChannelPacketSizeNode.Value = deviceStreamChannelPacketSizeInitial;

            //pixelFormatNode.Symbolic = pixelFormatInitial;
            //widthNode.Value = widthInitial;
            //heightNode.Value = heightInitial;
        }


        public void AcquireImagesRapidly()
        {
            // get node values that will be changed in order to return their
            // values at the end of the example
            var widthNode = (ArenaNET.IInteger)m_connectedDevice.NodeMap.GetNode("Width");
            Int64 widthInitial = widthNode.Value;

            var heightNode = (ArenaNET.IInteger)m_connectedDevice.NodeMap.GetNode("Height");
            Int64 heightInitial = heightNode.Value;

            var pixelFormatNode = (ArenaNET.IEnumeration)m_connectedDevice.NodeMap.GetNode("PixelFormat");
            String pixelFormatInitial = pixelFormatNode.Entry.Symbolic;

            var deviceStreamChannelPacketSizeNode =
                m_connectedDevice.NodeMap.GetNode("DeviceStreamChannelPacketSize") as ArenaNET.IInteger;
            Int64 deviceStreamChannelPacketSizeInitial;

            if (MAX_PACKET_SIZE)
                deviceStreamChannelPacketSizeInitial = deviceStreamChannelPacketSizeNode.Value;

            var exposureAutoNode = (ArenaNET.IEnumeration)m_connectedDevice.NodeMap.GetNode("ExposureAuto");
            String exposureAutoInitial = exposureAutoNode.Entry.Symbolic;

            var exposureTimeNode = m_connectedDevice.NodeMap.GetNode("ExposureTime") as ArenaNET.IFloat;
            double exposureTimeInitial = exposureTimeNode.Value;

            // Set low width and height
            //    Reducing the size of an image reduces the amount of bandwidth
            //    required for each image. The less bandwidth required per image,
            //    the more images can be sent over the same bandwidth.


            Int64 width = SetIntValue(m_connectedDevice.NodeMap, "Width", WIDTH);

            Int64 height = SetIntValue(m_connectedDevice.NodeMap, "Height", HEIGHT);



            // Set small pixel format
            //    Similar to reducing the ROI, reducing the number of bits per
            //    pixel also reduces the bandwidth required for each image. The
            //    smallest pixel formats are 8-bit bayer and 8-bit mono (i.e.
            //    BayerRG8 and Mono8).


            pixelFormatNode.Symbolic = PIXEL_FORMAT;

            // Set maximum stream channel packet size
            //    Maximizing packet size increases frame rate by reducing the
            //    amount of overhead required between images. This includes both
            //    extra header/trailer data per packet as well as extra time from
            //    intra-packet spacing (the time between packets). In order to
            //    grab images at the maximum packet size, the ethernet adapter
            //    must be configured appropriately: 'Jumbo packet' must be set to
            //    its maximum, 'UDP checksum offload' must be set to
            //    'Rx & Tx Enabled', and 'Received Buffers' must be set to its
            //    maximum.
            if (MAX_PACKET_SIZE)
            {

                if (deviceStreamChannelPacketSizeNode.Equals(null) || !deviceStreamChannelPacketSizeNode.IsReadable || !deviceStreamChannelPacketSizeNode.IsWritable)
                    throw new ArenaNET.GenericException("DeviceStreamChannelPacketSize node not found/readable/writable");



                deviceStreamChannelPacketSizeNode.Value = deviceStreamChannelPacketSizeNode.Max;
            }

            // Set low exposure time
            //    Decreasing exposure time can increase frame rate by reducing
            //    the amount of time it takes to grab an image. Reducing the
            //    exposure time past certain thresholds can cause problems
            //    related to not having enough light. In certain situations this
            //    can be mitigated by increasing gain and/or environmental light.


            exposureAutoNode.Symbolic = "Off";

            if (exposureTimeNode.Equals(null) || !exposureTimeNode.IsReadable || !exposureTimeNode.IsWritable)
                throw new ArenaNET.GenericException("ExposureTime node not found/readable/writable");

            Console.WriteLine(" ({0} {1})", exposureTimeNode.Min, exposureTimeNode.Unit);

            exposureTimeNode.Value = exposureTimeNode.Min;

            if (!MAX_PACKET_SIZE)
            {
                // enable stream auto negotiate packet size
                var streamAutoNegotiatePacketSizeNode = (ArenaNET.IBoolean)m_connectedDevice.TLStreamNodeMap.GetNode("StreamAutoNegotiatePacketSize");
                streamAutoNegotiatePacketSizeNode.Value = true;
            }

            // enable stream packet resend
            var streamPacketResendEnableNode = (ArenaNET.IBoolean)m_connectedDevice.TLStreamNodeMap.GetNode("StreamPacketResendEnable");
            streamPacketResendEnableNode.Value = true;

            // Start stream with high number of buffers
            //    Increasing the number of buffers can increase speeds by
            //    reducing the amount of time taken to requeue buffers. In this
            //    example, one buffer is used for each image. Of course, the
            //    amount of buffers that can be used is limited by the amount of
            //    space in memory.




            m_connectedDevice.StartStream();

            //for (int i = 1; i <= NUM_IMAGES; i++)
            //{
            //    // Get image
            //    //    By starting the stream with enough buffers for all images
            //    //    without requeuing, performance is improved. While
            //    //    effective, this method is even more restricted by the
            //    //    amount of available memory.
            //    Console.Write("{0}Get image {1} {2}", TAB2, i, (i % 250 == 0 && i != 0) || i == NUM_IMAGES ? "\n" : "\r");

            //    ArenaNET.IImage image = device.GetImage(TIMEOUT);
            //    images.Add(image);
            //}

            // requeue buffers
            //Console.WriteLine("{0}Requeue buffers", TAB1);

            //for (int i = 0; i < images.Count; i++)
            //{
            //    device.RequeueBuffer(images[i]);
            //}

            // stop stream
            //      Console.WriteLine("{0}Stop stream", TAB1);

            //      device.StopStream();

            // return nodes to their initial values
            //if (exposureAutoInitial == "Off")
            //    exposureTimeNode.Value = exposureTimeInitial;

            //exposureAutoNode.Symbolic = exposureAutoInitial;

            //if (MAX_PACKET_SIZE)
            //    deviceStreamChannelPacketSizeNode.Value = deviceStreamChannelPacketSizeInitial;

            //pixelFormatNode.Symbolic = pixelFormatInitial;
            //widthNode.Value = widthInitial;
            //heightNode.Value = heightInitial;
        }

        public Int64 SetIntValue(ArenaNET.INodeMap nodeMap, String nodeName, Int64 value)
        {
            // get node
            var integerNode = (ArenaNET.IInteger)nodeMap.GetNode(nodeName);

            // Ensure increment
            //    If a node has an increment (all integer nodes & some float
            //    nodes), only multiples of the increment can be set. Ensure this
            //    by dividing and then multiplying by the increment. If a value
            //    is between two increments, this will push it to the lower
            //    value. Most minimum values are divisible by the increment. If
            //    not, this must also be considered in the calculation.
            value = (((value - integerNode.Min) / integerNode.Inc) * integerNode.Inc) + integerNode.Min;

            // Check min/max values
            //    Values must not be less than the minimum or exceed the maximum
            //    value of a node. If a value does so, simply push it within
            //    range.
            if (value < integerNode.Min)
                value = integerNode.Min;

            if (value > integerNode.Max)
                value = integerNode.Max;

            // set value
            integerNode.Value = value;

            // return value for output
            return value;
        }
        // starts stream
        public void StartStream_custom()
        {
            if (m_connectedDevice != null)
            {
                if ((m_connectedDevice.TLStreamNodeMap.GetNode("StreamIsGrabbing") as ArenaNET.IBoolean).Value == false)
                {
                    (m_connectedDevice.TLStreamNodeMap.GetNode("StreamBufferHandlingMode") as ArenaNET.IEnumeration).Symbolic = "NewestOnly";
                    //  m_connectedDevice.StartStream();
                }
                Int64 width = SetIntValue(m_connectedDevice.NodeMap, "Width", WIDTH);

                Int64 height = SetIntValue(m_connectedDevice.NodeMap, "Height", HEIGHT);

                var acquisitionModeNode = (ArenaNET.IEnumeration)m_connectedDevice.NodeMap.GetNode("AcquisitionMode");
                String acquisitionModeInitial = acquisitionModeNode.Entry.Symbolic;




                acquisitionModeNode.FromString("Continuous");




                var streamBufferHandlingModeNode = (ArenaNET.IEnumeration)m_connectedDevice.TLStreamNodeMap.GetNode("StreamBufferHandlingMode");
                streamBufferHandlingModeNode.FromString("NewestOnly");



                var streamAutoNegotiatePacketSizeNode = (ArenaNET.IBoolean)m_connectedDevice.TLStreamNodeMap.GetNode("StreamAutoNegotiatePacketSize");
                streamAutoNegotiatePacketSizeNode.Value = true;
                //     var deviceStreamChannelPacketSizeNode =
                //m_connectedDevice.NodeMap.GetNode("DeviceStreamChannelPacketSize") as ArenaNET.IInteger;
                //  Int64 deviceStreamChannelPacketSizeInitial;
                // deviceStreamChannelPacketSizeNode.Value = deviceStreamChannelPacketSizeNode.Max;

                var streamPacketResendEnableNode = (ArenaNET.IBoolean)m_connectedDevice.TLStreamNodeMap.GetNode("StreamPacketResendEnable");
                streamPacketResendEnableNode.Value = true;


                m_connectedDevice.StartStream();
            }
        }



        // updates list of available devices
        private void UpdateDevices()
        {
            try
            {
                globalvariabel.ArenaManager.m_system.UpdateDevices(100);
                var t = globalvariabel.ArenaManager.m_system.Interfaces;
                m_deviceInfos = globalvariabel.ArenaManager.m_system.Devices;
                if(m_deviceInfos.Count<=0)
                { 
                    ForceNetworkSettings(); 
                }
            }
            catch (Exception EX)
            {
                globalvariabel.Record_except.Msg(EX.Message, Record_except.msgtype.error);
            }
        }








        // reconnects a device when disconnected
        // (1) On disconnection, activate
        // (2) Search for device
        // (3) Reconnect device
        // (4) Notify all on reconnection
        // (5) Notify all on exit
        //重启设备
        public void EnumerationThread()
        {
            while (stream)
            {
                while (stream && m_connectedDevice == null)
                {
                    // Search for device
                    //    When the device has been disconnected, this thread
                    //    waits for it to reconnect, constantly updating and
                    //    scanning the device list for the lost device by its
                    //    serial number
                    globalvariabel.ArenaManager.m_system.UpdateDevices(100);

                    m_deviceInfos = globalvariabel.ArenaManager.m_system.Devices;
                    var it = m_deviceInfos.FirstOrDefault(x => x.SerialNumber == SN);

                    if (it != null)
                    {
                        // Recreate device and notify other thread
                        //    Once the device has been found, recreate it and
                        //    notify the acquisition thread that it can stop
                        //    waiting and continue acquiring images.
                        //   Console.WriteLine("\r{0}Device reconnected", TAB4);
                        try
                        {
                            m_connectedDevice = globalvariabel.ArenaManager.m_system.CreateDevice(it);
                        }
                        catch (ArenaNET.InvalidArgumentException ex)
                        {
                            globalvariabel.Record_except.Msg(ex.Message, Record_except.msgtype.error);
                            ConnectDevice(SN);
                        }

                        // Ensure appropriate network settings
                        //    Check that the device is on the same subnet after
                        //    reconnecting the camera. If the camera/adapter are
                        //    on a non 169.254.*.* subnet but not using a
                        //    persistent IP or DHCP, the camera will
                        //    automatically be assigned an LLA and pick a
                        //    169.254.*.* IP/subnet after reconnecting, causing
                        //    the example to exit. There are several ways to fix
                        //    this issue: (1) by setting a static IP to the
                        //    ethernet port, (2) forcing an IP address whenever
                        //    the device is updated (see C_ForceIP), (3) running
                        //    ArenaConfig to configure the adapter settings, or
                        //    (4) setting up a persistent IP on the camera using
                        //    IPConfigUtility
                        UInt32 subnetMaskReconnect = (UInt32)it.SubnetMask;
                        if (subnetMaskReconnect != g_subnetMask)
                        {
                            //Console.WriteLine("{0}Error: Subnet has changed upon reconnecting", TAB2);
                            //Console.WriteLine("{0}Subnet at example start:   {1}", TAB3, g_subnetMask);
                            //Console.WriteLine("{0}Subnet after reconnection: {1}", TAB3, subnetMaskReconnect);
                            //Console.WriteLine("\n{0}Press enter to exit example", TAB1);

                            stream = false;
                            Monitor.Enter(g_deviceMutex);
                            Monitor.PulseAll(g_deviceMutex);
                            Monitor.Exit(g_deviceMutex);
                        }

                        // notify other thread to wake up and continue grabbing
                        // images
                        Monitor.Enter(g_deviceMutex);
                        Monitor.PulseAll(g_deviceMutex);
                        Monitor.Exit(g_deviceMutex);
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        ForceNetworkSettings();

                        Monitor.Enter(g_deviceMutex);
                        Monitor.PulseAll(g_deviceMutex);
                        Monitor.Exit(g_deviceMutex);
                    }
                }
            }

            // Notify other thread on exit
            //    If the device is disconnected at the time of exit, the other
            //    thread will be waiting for reconnection. Sending this
            //    notification allows the other thread to stop waiting and
            //    continue to exit.
            //Console.WriteLine("{0}Notify other thread on exit", TAB3);

            Monitor.Enter(g_deviceMutex);
            Monitor.PulseAll(g_deviceMutex);
            Monitor.Exit(g_deviceMutex);
        }



        // demonstrates forcing a new IP address
        // (1) discovers devices and gets information
        // (2) prepares IP address by adding 1 to the last octet
        // (3) forces new IP address
        // (4) discovers devices and gets information again
        public void ForceNetworkSettings()
        {


            // globalvariabel.ArenaManager.m_system.UpdateDevices(TIMEOUT);

            // Get device information
            //    Forcing the IP address requires a device's MAC address to
            //    specify the device. This example grabs the IP address, subnet
            //    mask, and default gateway as well to display changes and return
            //    the device to its original IP address.

            //   var it = m_deviceInfos.FirstOrDefault(x => x.SerialNumber == SN);
            //if (it != null)
            {
                //UInt64 macAddress = (UInt32)it.MacAddress;
                //UInt32 ipAddress = (UInt32)it.IpAddress;
                //UInt32 subnetMask = (UInt32)it.SubnetMask;
                //UInt32 defaultGateway = (UInt32)it.DefaultGateway;
                int _subnetMask = -256;
                 int _defaultGateway = -256;
                int _ipAddress = -1062701015;
                UInt64 macAddress = Convert.ToUInt64("30853696440099");
                UInt32 ipAddress =(UInt32)_ipAddress;
                UInt32 subnetMask = (UInt32)_subnetMask;
                UInt32 defaultGateway = (UInt32)_defaultGateway;

                // Add 1 to IP address
                //    The new IP address takes the current IP address and adds 1 to
                //    the final octet. If the final octet is 254, the final octet is
                //    set to 1 (to avoid 0 and 255).
                UInt32 ipAddressToSet;

               if ((ipAddress & 0x000000FF) == 0x000000FE)
                    ipAddressToSet = (UInt32)ipAddress & 0xFFFFFF01;
                else
                    ipAddressToSet = (UInt32)ipAddress + 0x00000001;



                // Force network settings
                //    Forcing the IP uses the MAC address to specify a device and
                //    forces the IP address, subnet mask, and default gateway. In
                //    this case, the IP address is being changed while the subnet
                //    mask and default gateway remain the same.


                globalvariabel.ArenaManager.m_system.ForceIp(macAddress, ipAddressToSet, subnetMask, defaultGateway);

                // Discover devices again
                //    Once a device has been forced, it needs to be rediscovered with
                //    its new network settings. This is especially important if
                //    moving on to configuration and acquisition.

                globalvariabel.ArenaManager.m_system.UpdateDevices(TIMEOUT);






              //  globalvariabel.Record_except.Msg(String.Format("{0}Prepare new IP address {1}.{2}.{3}.{4}", SN, (ipAddressToSet >> 24 & 0xFF), (ipAddressToSet >> 16 & 0xFF), (ipAddressToSet >> 8 & 0xFF), (ipAddressToSet & 0xFF)), Record_except.msgtype.sucess);
            }
        }

    }










}
