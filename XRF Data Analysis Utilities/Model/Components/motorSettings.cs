///////////////////////////////////////
#region Namespace Directives

// None

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model.Components
{
    public class motorSettings
    {
        ////////////////////////////////////////
        #region Members

        public double Increment
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public double Start
        {
            get;
            private set;
        }

        public double Stop
        {
            get;
            private set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_increment"></param>
        /// <param name="_name"></param>
        /// <param name="_start"></param>
        /// <param name="_stop"></param>
        public motorSettings(double _increment, string _name, double _start, double _stop)
        {
            this.Increment = _increment;
            this.Name = _name;
            this.Start = _start;
            this.Stop = _stop;
        }

        #endregion
    }
}
