using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys.Tool
{
    //soundPlay.Stop();    soundPlay.PlaySound(Application.StartupPath + @"\ERR.WAV");
    /// <summary>
    /// 播放声音
    /// </summary>
    public class SoundPlay
    {
        [DllImport("winmm.DLL")]
        public static extern long sndPlaySound(string strSound, long dwFlat);

        public static int SND_SYNC = 0;
        public static int SND_ASYNC = 1;
        public static int SND_MEMORY = 4;
        public static int SND_LOOP = 8;
        public static int SND_NOSTOP = 10;

        public SoundPlay()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public long sndPlay(string strSound, long dwFlat)
        {
            return sndPlaySound(strSound, dwFlat);
        }
        public long Stop()
        {
            PlaySound(null);
            return 0;
        }
        public void PlaySound(string FileName)
        {
            this.sndPlay(FileName, SND_ASYNC);
        }
    }
}
