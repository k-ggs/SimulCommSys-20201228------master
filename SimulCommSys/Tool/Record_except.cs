using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace SimulCommSys
{
  public  class Record_except:NotificationObject
    {
        private string _Msg = string.Empty;

        public string msg
        {
            get { return this._Msg; }
            set
            { this._Msg = value;
                RaisePropertyChanged("msg");
            }
        }


        private int _flag =1;

        public int flag
        {
            get { return this._flag; }
            set
            {
                this._flag = value;
                RaisePropertyChanged("flag");
            }
        }
       public enum msgtype
        {
            error,
            sucess,
            warning
        }

        public  void Msg(string ex,msgtype mstype)
        {
            try
            {
                switch (mstype)
                {
                    case msgtype.error:

                        _flag = 0;
                        break;
                    case msgtype.sucess:
                        _flag = 1;
                        break;
                    case msgtype.warning:
                        _flag = 2;
                        break;
                    default:
                        _flag = 0;
                        break;

                }
                App.Current.Dispatcher.Invoke(() =>
                {
                    FileStream stream;
                    StreamWriter writer;

                    stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\excepect_txt\excepect_log.txt", FileMode.Append);//fileMode指定是读取还是写入
                    writer = new StreamWriter(stream);
                    this._Msg = DateTime.Now.ToString() + "\t" + ex.ToString();

                    writer.WriteLine(this._Msg);//写入一行，写完后会自动换行

                });


            }
             
            catch (Exception )
            { 
            }

        }
    }
}
