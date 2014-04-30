///////////////////////////////////////
#region Namespace Directives

using System;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model
{
    public struct pixel
    {
        ////////////////////////////////////////
        #region Coordinate

        public coordinate Actual
        {
            get;
            private set;
        }

        public coordinate Goal
        {
            get;
            private set;
        }

        #endregion

        ////////////////////////////////////////
        #region Properties

        public int Counts
        {
            get;
            private set;
        }

        public double Temperature
        {
            get;
            private set;
        }

        public string Tag
        {
            get;
            private set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public pixel(double _xActual, double _yActual, double _xGoal, double _yGoal, int _counts, double _temperature)
            : this()
        {
            this.Actual = new coordinate(_xActual, _yActual);
            this.Goal = new coordinate(_xGoal, _yGoal);
            this.Counts = _counts;
            this.Temperature = _temperature;
            this.Tag = Guid.NewGuid().ToString();
        }

        #endregion
    }
}
