using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys
{
    interface  iModel
    {
        /// <summary>
        /// 启动
        /// </summary>
        bool Move { get; set ; }

        /// <summary>
        /// 前进
        /// </summary>
        bool Move_forward { get; set; }

        /// <summary>
        /// 后退
        /// </summary>
        bool Move_Back{ get; set; }

        /// <summary>
        /// 运动速度
        /// </summary>
        float sp_flt { get; set; }
    }
}
