using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys
{/// <summary>
/// 读取配置文件
/// </summary>
    public class Inifile : Component
    {
        // Fields
        private IContainer components;
        public string inipath;

        // Methods
        public Inifile()
        {
            this.components = null;
            this.InitializeComponent();
        }

        public Inifile(IContainer container)
        {
            this.components = null;
            container.Add(this);
            this.InitializeComponent();
        }



        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        private void InitializeComponent()
        {
            this.components = new Container();
        }

        public string ReadIniFile(string sFile, string sSection, string sKey, string sDefault)
        {
            StringBuilder retVal = new StringBuilder(500);
            int num = GetPrivateProfileString(sSection, sKey, sDefault, retVal, 500, sFile);
            return retVal.ToString();
        }

        public void WriteIniFile(string sFile, string sSection, string sKey, string sValue)
        {
            WritePrivateProfileString(sSection, sKey, sValue, sFile);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}
