using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace SimulCommSys
{
  
    public class Model_Person : NotificationObject,iModel
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;
        private bool _Move = false;

        private bool _Move_Back = false;
        private bool _Move_forward = false;
        private float _sp_flt ;
        /// <summary>
        /// 启停
        /// </summary>
        public bool Move
        {
            get
            {
                return this._Move;
            }

            set
            {
                this._Move = value;
                this.RaisePropertyChanged("Move");
            }
        }
        /// <summary>
        /// 前进
        /// </summary>
        public bool Move_forward
        {
            get
            {
                return this._Move_forward;
            }

            set
            {
                this._Move_forward = value;
                this.RaisePropertyChanged("PersonMove_forward");
            }
        }
        /// <summary>
        /// 后退
        /// </summary>
        public bool Move_Back
        {
            get
            {
                return this._Move_Back;
            }

            set
            {
                this._Move_Back = value;
                this.RaisePropertyChanged("Move_Back");
            }
        }


        /// <summary>
        /// 速度
        /// </summary>
        public float sp_flt
        {
            get
            {
                return this._sp_flt;
            }

            set
            {
                this._sp_flt = value;
                sp_flt_change(value);
                this.RaisePropertyChanged("sp_flt");
            }
        }


        #region 速度控制
        private float _maxmum = Model_maxmum;
        private float _minmum = Model_minmum;
        private float _increment = Model_increment;

        public float maxmum
        {
            get { return this._maxmum; }
            set { this._maxmum = value; this.RaisePropertyChanged("maxmum"); }
        }
        public float minmum
        { get { return this._minmum; } set { this._minmum = value; RaisePropertyChanged("minmum"); } }
        public float increment
        {
            get { return this._increment; }
            set { this._increment = value; }
        }
        #endregion

    }


    /// <summary>
    /// 线膛炮
    /// </summary>
    public class Model_BoreGUN : NotificationObject, iModel
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;
        private bool _Move = false;

        private bool _Move_Back = false;
        private bool _Move_forward = false;
        private float _sp_flt ;
        /// <summary>
        /// 启停
        /// </summary>
        public bool Move
        {
            get
            {
                return this._Move;
            }

            set
            {
                this._Move = value;
                this.RaisePropertyChanged("Move");
            }
        }
        /// <summary>
        /// 前进
        /// </summary>
        public bool Move_forward
        {
            get
            {
                return this._Move_forward;
            }

            set
            {
                this._Move_forward = value;
                this.RaisePropertyChanged("PersonMove_forward");
            }
        }
        /// <summary>
        /// 后退
        /// </summary>
        public bool Move_Back
        {
            get
            {
                return this._Move_Back;
            }

            set
            {
                this._Move_Back = value;
                this.RaisePropertyChanged("Move_Back");
            }
        }


        /// <summary>
        /// 速度
        /// </summary>
        public float sp_flt
        {
            get
            {
                return this._sp_flt;
            }

            set
            {
                this._sp_flt = value;
                sp_flt_change(value);
                this.RaisePropertyChanged("sp_flt");
            }
        }


        #region 速度控制
        private float _maxmum = Model_maxmum;
        private float _minmum = Model_minmum;
        private float _increment = Model_increment;

        public float maxmum
        {
            get { return this._maxmum; }
            set { this._maxmum = value; this.RaisePropertyChanged("maxmum"); }
        }
        public float minmum
        { get { return this._minmum; } set { this._minmum = value; RaisePropertyChanged("minmum"); } }
        public float increment
        {
            get { return this._increment; }
            set { this._increment = value; }
        }
        #endregion
    }

    /// <summary>
    /// 火箭炮
    /// </summary>
    public class Model_RocketGUN : NotificationObject, iModel
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;
        private bool _Move = false;

        private bool _Move_Back = false;
        private bool _Move_forward = false;
        private float _sp_flt ;
        /// <summary>
        /// 启停
        /// </summary>
        public bool Move
        {
            get
            {
                return this._Move;
            }

            set
            {
                this._Move = value;
                this.RaisePropertyChanged("Move");
            }
        }
        /// <summary>
        /// 前进
        /// </summary>
        public bool Move_forward
        {
            get
            {
                return this._Move_forward;
            }

            set
            {
                this._Move_forward = value;
                this.RaisePropertyChanged("PersonMove_forward");
            }
        }
        /// <summary>
        /// 后退
        /// </summary>
        public bool Move_Back
        {
            get
            {
                return this._Move_Back;
            }

            set
            {
                this._Move_Back = value;
                this.RaisePropertyChanged("Move_Back");
            }
        }


        /// <summary>
        /// 速度
        /// </summary>
        public float sp_flt
        {
            get
            {
                return this._sp_flt;
            }

            set
            {
                this._sp_flt = value;
                sp_flt_change(value);
                this.RaisePropertyChanged("sp_flt");
            }
        }


        #region 速度控制
        private float _maxmum = Model_maxmum;
        private float _minmum = Model_minmum;
        private float _increment = Model_increment;

        public float maxmum
        {
            get { return this._maxmum; }
            set { this._maxmum = value; this.RaisePropertyChanged("maxmum"); }
        }
        public float minmum
        { get { return this._minmum; } set { this._minmum = value; RaisePropertyChanged("minmum"); } }
        public float increment
        {
            get { return this._increment; }
            set { this._increment = value; }
        }
        #endregion
    }
  

    public class Model_Tank : NotificationObject, iModel
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;
        private bool _Move = false;

        private bool _Move_Back = false;
        private bool _Move_forward = false;
        private float _sp_flt ;
        /// <summary>
        /// 启停
        /// </summary>
        public bool Move
        {
            get
            {
                return this._Move;
            }

            set
            {
                this._Move = value;
                this.RaisePropertyChanged("Move");
            }
        }
        /// <summary>
        /// 前进
        /// </summary>
        public bool Move_forward
        {
            get
            {
                return this._Move_forward;
            }

            set
            {
                this._Move_forward = value;
                this.RaisePropertyChanged("PersonMove_forward");
            }
        }
        /// <summary>
        /// 后退
        /// </summary>
        public bool Move_Back
        {
            get
            {
                return this._Move_Back;
            }

            set
            {
                this._Move_Back = value;
                this.RaisePropertyChanged("Move_Back");
            }
        }


        /// <summary>
        /// 速度
        /// </summary>
        public float sp_flt
        {
            get
            {
                return this._sp_flt;
            }

            set
            {
                this._sp_flt = value;
                sp_flt_change(value);
                this.RaisePropertyChanged("sp_flt");
            }
        }


        #region 速度控制
        private float _maxmum = Model_maxmum;
        private float _minmum = Model_minmum;
        private float _increment = Model_increment;

        public float maxmum
        {
            get { return this._maxmum; }
            set { this._maxmum = value; this.RaisePropertyChanged("maxmum"); }
        }
        public float minmum
        { get { return this._minmum; } set { this._minmum = value; RaisePropertyChanged("minmum"); } }
        public float increment
        {
            get { return this._increment; }
            set { this._increment = value; }
        }
        #endregion
    }

    /// <summary>
    /// 运输车
    /// </summary>
    public class Model_CarryCar : NotificationObject, iModel
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;
        private bool _Move = false;

        private bool _Move_Back = false;
        private bool _Move_forward = false;
        private float _sp_flt ;
        /// <summary>
        /// 启停
        /// </summary>
        public bool Move
        {
            get
            {
                return this._Move;
            }

            set
            {
                this._Move = value;
                this.RaisePropertyChanged("Move");
            }
        }
        /// <summary>
        /// 前进
        /// </summary>
        public bool Move_forward
        {
            get
            {
                return this._Move_forward;
            }

            set
            {
                this._Move_forward = value;
                this.RaisePropertyChanged("PersonMove_forward");
            }
        }
        /// <summary>
        /// 后退
        /// </summary>
        public bool Move_Back
        {
            get
            {
                return this._Move_Back;
            }

            set
            {
                this._Move_Back = value;
                this.RaisePropertyChanged("Move_Back");
            }
        }


      
        /// <summary>
        /// 速度
        /// </summary>
        public float sp_flt
        {
            get
            {
                return this._sp_flt;
            }

            set
            {
                this._sp_flt = value;
                sp_flt_change(value);
                this.RaisePropertyChanged("sp_flt");
            }
        }


        #region 速度控制
        private float _maxmum = Model_maxmum;
        private float _minmum = Model_minmum;
        private float _increment = Model_increment;

        public float maxmum
        {
            get { return this._maxmum; }
            set { this._maxmum = value; this.RaisePropertyChanged("maxmum"); }
        }
        public float minmum
        { get { return this._minmum; } set { this._minmum = value; RaisePropertyChanged("minmum"); } }
        public float increment
        {
            get { return this._increment; }
            set { this._increment = value; }
        }
        #endregion
    }

}
