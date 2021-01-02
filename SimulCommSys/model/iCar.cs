using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys
{
  public interface  iCar

    {
        /// <summary>
        /// 启停
        /// </summary>
        bool Move { get; set; }

       /// <summary>
       /// 向前
       /// </summary>
        bool Carforward { get; set; }
        /// <summary>
        /// 向前
        /// </summary>
        bool Carback { get; set; }


       /// <summary>
       /// 左旋转
       /// </summary>
        bool CarLeftRevolve { get; set; }
        /// <summary>
        /// 右旋转
        /// </summary>
        bool CarRightRevolve { get; set; }


       /// <summary>
       /// 向上
       /// </summary>
        bool Carup { get; set; }
        /// <summary>
        /// 向下
        /// </summary>
        bool CarDwn { get; set; }
        /// <summary>
        /// 俯冲
        /// </summary>
        bool CarLookdwn { get; set; }
        /// <summary>
        /// 仰冲
        /// </summary>
        bool CarLookup { get; set; }

        /// <summary>
        /// str速度
        /// </summary>
        string speed { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        float sp_flt { get; set; }
    }
}
