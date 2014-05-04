///////////////////////////////////////
#region Namespace Directives

using System;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model.Structures
{
    public struct coordinate
    {
        ////////////////////////////////////////
        #region Properties

        public double X
        {
            get;
            private set;
        }

        public double Y
        {
            get;
            private set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor


        public coordinate(double _x, double _y)
            : this()
        {
            this.X = _x;
            this.Y = _y;
        }

        #endregion
    }
}
