///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.ObjectModel;
using XRF_Data_Analysis_Utilities.Model;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Analysis
{
    public class MultipleElementAnalysisWorkspaceViewModel : AnalysisWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Constructor

        public MultipleElementAnalysisWorkspaceViewModel(string header, ref xrfSample sample) : base(header, ref sample) { }

        #endregion
    }
}
