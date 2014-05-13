///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows.Media;
using TheseColorsDontRun.ViewModel.Workspaces;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model.Structures;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class RampWrapperWorkspaceViewModel : WorkspaceViewModel, IRampWrapperWorkspace
    {
        ////////////////////////////////////////
        #region Generic Fields

        public IColorRampWorkspaceViewModel ColorRamp
        {
            get;
            set;
        }

        Action<object> RampChanged
        {
            get;
            set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public RampWrapperWorkspaceViewModel(Color[] _rampHues, Action<object> _rampChanged)
        {
            RampChanged = _rampChanged;
            InitializeDataMapping(_rampHues);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void InitializeDataMapping(Color[] rampColors)
        {
            ColorRamp = new ColorRampWorkspaceViewModel(true, rampColors);
            if (RampChanged != null)
            {
                ColorRamp.PropertyChanged += ColorRamp_PropertyChanged;
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Event Handling


        void ColorRamp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RampChanged(sender);
        }

        #endregion
    }
}
