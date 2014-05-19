﻿///////////////////////////////////////
#region Namespace Directives

using LookinSharp.WPF.Model;
using System;
using System.Windows;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model.Components
{
    public class pixel : pixelBase
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
            get
            {
                return base.Coordinate;
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Properties

        public int Counts
        {
            get;
            private set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor


        public pixel(double _xActual, double _yActual, double _xGoal, double _yGoal, int _counts, double _temperature)
            : base(_xGoal, _yGoal, _temperature, String.Empty)
        {
            this.Actual = new Point(_xActual, _yActual);
            this.Counts = _counts;
            this.Description = GenerateDescription();
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private string GenerateDescription()
        {
            return "Counts: " + this.Counts
                + Environment.NewLine + "xGoal: " + this.Goal.X + " | yGoal: " + this.Goal.Y
                + Environment.NewLine + "xActual: " + this.Actual.X + " | yActual: " + this.Actual.Y;
        }

        #endregion
    }
}
