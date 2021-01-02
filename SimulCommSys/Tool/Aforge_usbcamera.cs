using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimulCommSys.Tool
{
   public class Aforge_usbcamera:NotificationObject
    {
       
        

            #region Public properties

            public ObservableCollection<FilterInfo> VideoDevices { get; set; }

            public FilterInfo CurrentDevice
            {
                get { return _currentDevice; }
                set { _currentDevice = value; this.RaisePropertyChanged("CurrentDevice"); }
            }
            private FilterInfo _currentDevice;

            #endregion


            #region Private fields

            private IVideoSource _videoSource;

        #endregion


        private BitmapImage _BitmapImage;
        public BitmapImage BtmpImg
        {
            get { return _BitmapImage; }
            set { _BitmapImage = value; this.RaisePropertyChanged("BtmpImg"); }
        }







        public void video_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
            {
                try
                {
                    BitmapImage bi;
                    using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                    {
                        bi = bitmap.ToBitmapImage();
                    }
                    bi.Freeze(); // avoid cross thread operations and prevents leaks
                                 //  Dispatcher.BeginInvoke(new ThreadStart(delegate { videoPlayer.Source = bi; }));
                this.BtmpImg = bi;
                }
                catch (Exception exc)
                {
                globalvariabel.Record_except.Msg("Error on _videoSource_NewFrame:\n" + exc.Message, Record_except.msgtype.error);
                StopCamera();
                }
            }

        
            public void GetVideoDevices()
            {
                VideoDevices = new ObservableCollection<FilterInfo>();
                foreach (FilterInfo filterInfo in new FilterInfoCollection(FilterCategory.VideoInputDevice))
                {
                    VideoDevices.Add(filterInfo);
                }
                if (VideoDevices.Any())
                {
                    CurrentDevice = VideoDevices[0];
                }
                else
                {
                  //  MessageBox.Show("No video sources found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                globalvariabel.Record_except.Msg("No video sources found", Record_except.msgtype.error);
            }
            }

        public void StartCamera()
            {
            GetVideoDevices();
                if (CurrentDevice != null)
                {
                    _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                    _videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    _videoSource.Start();
                }
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
