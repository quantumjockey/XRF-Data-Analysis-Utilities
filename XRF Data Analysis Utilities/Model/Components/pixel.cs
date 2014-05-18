///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model.Components
{
    public class pixel
    {
        ////////////////////////////////////////
        #region Coordinate

        public Point Actual
        {
            get;
            private set;
        }

        public Point Goal
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
        {
            this.Actual = new Point(_xActual, _yActual);
            this.Goal = new Point(_xGoal, _yGoal);
            this.Counts = _counts;
            this.Temperature = _temperature;
            this.Tag = Guid.NewGuid().ToString();
        }

        #endregion
    }
}
