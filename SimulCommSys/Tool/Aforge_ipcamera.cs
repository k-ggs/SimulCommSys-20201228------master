using AForge.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SimulCommSys.Tool
{
    static class BitmapHelpers
    {
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            BitmapImage bi = new BitmapImage();
            MemoryStream ms = new MemoryStream();
            try
            {




                if (bitmap != null)
                {
                    bi.BeginInit();
                    bitmap.Save(ms, ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = ms;
                    bi.EndInit();
                }
                else
                { return null; }
                return bi;
            }

            catch (Exception)
            {

                throw;
            }
            finally { ms.Dispose(); }
        }
    }
    public class Aforge_ipcamera :NotificationObject
    {
      
     
            #region Public properties

            public string ConnectionString
            {
                get { return _connectionString; }
                set { _connectionString = value; this.RaisePropertyChanged("ConnectionString"); }
            }

            public bool UseMjpegStream
            {
                get { return _useMJPEGStream; }
                set { _useMJPEGStream = value; this.RaisePropertyChanged("UseMjpegStream"); }
            }

            public bool UseJpegStream
            {
                get { return _useJPEGStream; }
                set { _useJPEGStream = value; this.RaisePropertyChanged("UseJpegStream"); }
            }

        #endregion
        private BitmapImage _BitmapImage;
        public  BitmapImage BtmpImg
        {
            get { return _BitmapImage; }
            set { _BitmapImage = value; this.RaisePropertyChanged("BtmpImg"); }
        }
        #region Private fields

        private string _connectionString="http://<axis_camera_ip>/axis-cgi/jpg/image.cgi";
            private bool _useMJPEGStream;
            private bool _useJPEGStream = true;
            private IVideoSource _videoSource;

        #endregion


     

        // timestamp of previous ImageCallback
        static UInt64 lastTimestamp = 0;



        public void IPcamera_strat()
            {

                // create JPEG video source
                if (UseJpegStream)
                {
                    _videoSource = new JPEGStream(ConnectionString);
                }
                else // UseMJpegStream
                {
                    _videoSource = new MJPEGStream(ConnectionString);
                }
                _videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                _videoSource.Start();
            }


        public void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
            {
                try
                {
                    BitmapImage bi;
                    using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                    {
                        bi = bitmap.ToBitmapImage();
                    }
                    bi.Freeze(); // avoid cross thread operations and prevents leaks
                                 //Dispatcher.BeginInvoke(new ThreadStart(delegate { videoPlayer.Source = bi; }));
                this.BtmpImg = bi;
                }
                catch (Exception exc)
                {
                    globalvariabel.Record_except.Msg("Error on _videoSource_NewFrame:\n" + exc.Message,Record_except.msgtype.error);
                    StopCamera();
                }
            }

        private void stop_ipcamera()
            {
                _videoSource.SignalToStop();
            }

        public void StopCamera()
            {
                if (_videoSource != null && _videoSource.IsRunning)
                {
                    _videoSource.SignalToStop();
                    _videoSource.NewFrame -= new NewFrameEventHandler(video_NewFrame);
                }
            }







    }
    }

