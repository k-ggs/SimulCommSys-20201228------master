using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimulCommSys.CamerApi
{
  //public  class MVCGEoperate
  //  {

  //   MVCGE   mvcge=globalvariabel.mvcge;

  //      /// <summary>
  //      /// 获取相机列表
  //      /// </summary>
  //      /// <returns></returns>
  //      public IList<string> RefeshList()
  //      {
  //          uint devnum = mvcge.GetDeviceNum();
  //          List<string> list = new List<string>();
  //          Debug.WriteLine("Dev No is " + devnum);
  //          if (devnum > 0)
  //          {
  //              list.Clear();
  //              for (uint m = 0; m < devnum; m++)
  //              {
  //                  MVCGE.MVCGE_DEVLISTEX lDevInfo = new MVCGE.MVCGE_DEVLISTEX();
  //                  mvcge.GetDevInfo(m, ref lDevInfo);
  //                  string lDevInf = "DEV#" + m + " " + lDevInfo.DevName + " " + lDevInfo.DevIP + " " + lDevInfo.DevMAC;
  //                  list.Add(lDevInf);
  //              } 
  //              return list;
  //          }
  //          return list;
  //      }
  //      //private void btnCapOpen_Click(uint g_iDevIndex,Control ctl)
  //      //{
  //      //    //显示模式初始化
  //      //    if (mvcge.GetDisplayMode() == 0) //内部内嵌显示
  //      //    {
  //      //        Debug.WriteLine("DisplayMode 0");
  //      //        mvcge.InnerDispEnable(g_iDevIndex, 1); //打开内部显示
  //      //        mvcge.InnerDispHwnd(g_iDevIndex, intp);
  //      //        mvcge.InnerDispPosX(g_iDevIndex, 0);
  //      //        mvcge.InnerDispPosY(g_iDevIndex, 0);
  //      //        mvcge.InnerDispWidth(g_iDevIndex, (uint)picShow.Width);
  //      //        mvcge.InnerDispHeight(g_iDevIndex, (uint)picShow.Height);
  //      //    }
  //      //    else if (mvcge.GetDisplayMode() == 1) //内部独立显示
  //      //    {
  //      //        Debug.WriteLine("DisplayMode 1");
  //      //        mvcge.InnerDispEnable(g_iDevIndex, 1); //打开内部显示
  //      //        mvcge.InnerDispHwnd(g_iDevIndex, IntPtr.Zero);
  //      //        mvcge.InnerDispPosX(g_iDevIndex, (uint)(picShow.Left + this.Left));
  //      //        mvcge.InnerDispPosY(g_iDevIndex, (uint)(picShow.Top + this.Top + 30));
  //      //        mvcge.InnerDispWidth(g_iDevIndex, (uint)picShow.Width);
  //      //        mvcge.InnerDispHeight(g_iDevIndex, (uint)picShow.Height);
  //      //    }
  //      //    else if (mvcge.GetDisplayMode() == 2) //回调内嵌显示
  //      //    {
  //      //        Debug.WriteLine("DisplayMode 2");
  //      //        mvcge.InnerDispEnable(g_iDevIndex, 0); //关闭内部显示
  //      //        MVCGE.RECT rectShow = new MVCGE.RECT();
  //      //        rectShow.Left = 0;
  //      //        rectShow.Top = 0;
  //      //        rectShow.Right = 0;
  //      //        rectShow.Bottom = 0;
  //      //        mvcge.DisplayInit(g_iDevIndex, 0, rectShow, picShow.Handle);
  //      //    }
  //      //    else if (mvcge.GetDisplayMode() == 3) //回调独立显示
  //      //    {
  //      //        Debug.WriteLine("DisplayMode 3");
  //      //        mvcge.InnerDispEnable(g_iDevIndex, 0); //关闭内部显示
  //      //        MVCGE.RECT rectShow = new MVCGE.RECT();
  //      //        rectShow.Left = picShow.Left + this.Left;
  //      //        rectShow.Top = picShow.Top + this.Top + 30;
  //      //        rectShow.Right = rectShow.Left + picShow.Width;
  //      //        rectShow.Bottom = rectShow.Top + picShow.Height;
  //      //        mvcge.DisplayInit(g_iDevIndex, 0, rectShow, IntPtr.Zero);
  //      //    }

  //      //    //打开采集和显示
  //      //    mvcge.EnableCapture(g_iDevIndex);

  //      //    gtTimer.Elapsed += new ElapsedEventHandler(TimedEvent);
  //      //    gtTimer.Interval = 1000;
  //      //    gtTimer.Enabled = true;

  //      //    btnCapShut.Enabled = true;
  //      //    btnSaveImg.Enabled = true;
  //      //    btnAwb.Enabled = true;
  //      //    cmbDispMode.Enabled = false;

  //      //    //打开回调          
  //      //    GCHandle gch = GCHandle.Alloc(mvcge);
  //      //    mvcge.CapCallBack(g_iDevIndex, MVCGE.framecallback, GCHandle.ToIntPtr(gch));
  //      //}
  //  }
}
