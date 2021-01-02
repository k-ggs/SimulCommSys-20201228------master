using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys
{
    public class Car_red : NotificationObject, iCar
    {

        public delegate void Chge(float val);
        public event Chge sp_flt_change;

        private string _speed;
        private float _sp_flt;

        private bool _Move;
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
        public bool Carforward { get; set; }
        public bool Carback { get; set; }
        public bool CarLeftRevolve { get; set; }
        public bool CarRightRevolve { get; set; }
        public bool Carup { get; set; }
        public bool CarDwn { get; set; }
        public bool CarLookdwn { get; set; }
        public bool CarLookup { get; set; }
        public string speed
        {
            get
            {
                return this._speed;

            }
            set
            {
                this._speed = value;
                RaisePropertyChanged("speed");
            }
        }
        public float sp_flt
        {
            get
            {
                return this._sp_flt;
            }
            set
            {
                this._sp_flt = value;
                this._speed = value.ToString();
                sp_flt_change(value);
                this.RaisePropertyChanged("sp_flt");
            }
        }

        #region 速度控制
        private float _maxmum = Car_maxmum;
        private float _minmum = Car_minmum;
        private float _increment = Car_increment;

        public float maxmum
        {
            get { return this._maxmum; }
            set { this._maxmum = value;this.RaisePropertyChanged("maxmum"); }
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
    public class Car_NormLight : NotificationObject, iCar
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;

        private bool _Move;
        private string _speed;
        private float _sp_flt ;
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
        public bool Carforward { get; set; }
        public bool Carback { get; set; }
        public bool CarLeftRevolve { get; set; }
        public bool CarRightRevolve { get; set; }
        public bool Carup { get; set; }
        public bool CarDwn { get; set; }
        public bool CarLookdwn { get; set; }
        public bool CarLookup { get; set; }
        public string speed
        {
            get
            {
                return this._speed;

            }
            set
            {
                this._speed = value;
                RaisePropertyChanged("speed");
            }
        }
        public float sp_flt
        {
            get
            {
                return this._sp_flt;
            }
            set
            {
                this._sp_flt = value;
                this._speed = value.ToString();
                this.sp_flt_change(value);
                this.RaisePropertyChanged("sp_flt");
            }
        }

        #region 速度控制
        private float _maxmum = Car_maxmum;
        private float _minmum = Car_minmum;
        private float _increment = Car_increment;

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
    public class Car_LitterLight : NotificationObject, iCar
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;

        private bool _Move;
        private string _speed;
        private float _sp_flt;
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
        public bool Carforward { get; set; }
        public bool Carback { get; set; }
        public bool CarLeftRevolve { get; set; }
        public bool CarRightRevolve { get; set; }
        public bool Carup { get; set; }
        public bool CarDwn { get; set; }
        public bool CarLookdwn { get; set; }
        public bool CarLookup { get; set; }
        public string speed
        {
            get
            {
                return this._speed;

            }
            set
            {
                this._speed = value;
                RaisePropertyChanged("speed");
            }
        }
        public float sp_flt
        {
            get
            {
                return this._sp_flt;
            }
            set
            {
                this._sp_flt = value;
                this._speed = value.ToString();
                this.sp_flt_change(value);
                this.RaisePropertyChanged("sp_flt");
            }
        }

        #region 速度控制
        private float _maxmum = Car_maxmum;
        private float _minmum = Car_minmum;
        private float _increment = Car_increment;

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


    public class Car_Fly : NotificationObject, iCar
    {

        public delegate void Chge(float val);
        public event Chge sp_flt_change;
        private bool _Move;
        private string _speed;
        private float _sp_flt ;
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
        public bool Carforward { get; set; }
        public bool Carback { get; set; }
        public bool CarLeftRevolve { get; set; }
        public bool CarRightRevolve { get; set; }
        public bool Carup { get; set; }
        public bool CarDwn { get; set; }
        public bool CarLookdwn { get; set; }
        public bool CarLookup { get; set; }
        public string speed
        {
            get
            {
                return this._speed;

            }
            set
            {
                this._speed = value;
                RaisePropertyChanged("speed");
            }
        }
        public float sp_flt
        {
            get
            {
                return this._sp_flt;
            }
            set
            {
                this._sp_flt = value;
                this._speed = value.ToString();
                this.sp_flt_change(value);
                this.RaisePropertyChanged("sp_flt");
            }
        }

        #region 速度控制
        private float _maxmum = Car_maxmum;
        private float _minmum = Car_minmum;
        private float _increment = Car_increment;

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
