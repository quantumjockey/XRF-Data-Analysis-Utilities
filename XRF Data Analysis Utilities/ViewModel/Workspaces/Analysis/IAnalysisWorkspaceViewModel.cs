///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.ObjectModel;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Analysis
{
    public interface IAnalysisWorkspaceViewModel
    {
        // Property signatures
        xrfSample SampleData { get; set; }
        ObservableCollection<elementData> Elements { get; set; }
    }
}
