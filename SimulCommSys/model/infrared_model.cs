using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys
{
   public class infrared_mod1:NotificationObject
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;
        private float _temper = _Temper;
        private float _maxtemper =_Maxtemper;
        private float _mintemper =_Mintemper;
        private float _increment = _Increment;

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

        public float temper
        {
            get { return this._temper; }
            set
            {
                this._temper = value;
                sp_flt_change(value);
                RaisePropertyChanged("temper");
            }

        }

        public float maxtemper
        {
            get { return this._maxtemper; }

            set { this._maxtemper = value;
                RaisePropertyChanged("maxtemper");
            }
        }

        public float mintemper
        {
            get { return this._mintemper; }
            set { this._mintemper = value;
                RaisePropertyChanged("mintemper");
            }

        }

        public float increment
        {
            get { return this._increment; }

            set { this._increment = value;
                RaisePropertyChanged("increment");
            }
        }

    }

    public class infrared_mod2 : NotificationObject
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;

        private float _temper = _Temper;
        private float _maxtemper = _Maxtemper;
        private float _mintemper = _Mintemper;
        private float _increment = _Increment;

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

        public float temper
        {
            get { return this._temper; }
            set
            {
                this._temper = value;
                sp_flt_change(value);
                RaisePropertyChanged("temper");
            }

        }

        public float maxtemper
        {
            get { return this._maxtemper; }

            set
            {
                this._maxtemper = value;
                RaisePropertyChanged("maxtemper");
            }
        }

        public float mintemper
        {
            get { return this._mintemper; }
            set
            {
                this._mintemper = value;
                RaisePropertyChanged("mintemper");
            }

        }

        public float increment
        {
            get { return this._increment; }

            set
            {
                this._increment = value;
                RaisePropertyChanged("increment");
            }
        }

    }

    public class infrared_mod3 : NotificationObject
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;
        private float _temper = _Temper;
        private float _maxtemper = _Maxtemper;
        private float _mintemper = _Mintemper;
        private float _increment = _Increment;

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

        public float temper
        {
            get { return this._temper; }
            set
            {
                this._temper = value;
                sp_flt_change(value);
                RaisePropertyChanged("temper");
            }

        }

        public float maxtemper
        {
            get { return this._maxtemper; }

            set
            {
                this._maxtemper = value;
                RaisePropertyChanged("maxtemper");
            }
        }

        public float mintemper
        {
            get { return this._mintemper; }
            set
            {
                this._mintemper = value;
                RaisePropertyChanged("mintemper");
            }

        }

        public float increment
        {
            get { return this._increment; }

            set
            {
                this._increment = value;
                RaisePropertyChanged("increment");
            }
        }

    }

    public class infrared_mod4 : NotificationObject
    {
        public delegate void Chge(float val);
        public event Chge sp_flt_change;
        private float _temper = _Temper;
        private float _maxtemper = _Maxtemper;
        private float _mintemper = _Mintemper;
        private float _increment = _Increment;


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
        public float temper
        {
            get { return this._temper; }
            set
            {
                this._temper = value;
                this.sp_flt_change(value);
                RaisePropertyChanged("temper");
            }

        }

        public float maxtemper
        {
            get { return this._maxtemper; }

            set
            {
                this._maxtemper = value;
                RaisePropertyChanged("maxtemper");
            }
        }

        public float mintemper
        {
            get { return this._mintemper; }
            set
            {
                this._mintemper = value;
                RaisePropertyChanged("mintemper");
            }

        }

        public float increment
        {
            get { return this._increment; }

            set
            {
                this._increment = value;
                RaisePropertyChanged("increment");
            }
        }

    }
}
