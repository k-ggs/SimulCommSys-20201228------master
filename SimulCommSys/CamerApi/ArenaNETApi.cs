using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimulCommSys
{
 
        // for each device in device list tree
        public class TreeItem
        {
            public string Title { get; set; } // string to display
            public string UId { get; set; } = ""; // serial number to identify device
        }

        public class ArenaManager
    {
        public ArenaNET.ISystem m_system = null;
        public ArenaManager()
            {
                m_system = ArenaNET.Arena.OpenSystem();
            }

      
    
    }

   


}
