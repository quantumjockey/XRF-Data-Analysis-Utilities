///////////////////////////////////////
#region Namespace Directives

using System.Collections.Generic;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model.Components
{
    public class motorGroup
    {
        ////////////////////////////////////////
        #region Members

        public double DelayAfterMovement
        {
            get;
            set;
        }

        public List<motorSettings> Devices
        {
            get;
            set;
        }

        public string Pattern
        {
            get;
            set;
        }

        public double StayAtEnd
        {
            get;
            set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public motorGroup()
        {
            Devices = new List<motorSettings>();
        }

        #endregion
    }
}
