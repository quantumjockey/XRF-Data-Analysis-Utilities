///////////////////////////////////////
#region Namespace Directives

using System.Collections.ObjectModel;
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

        // Method signatures
        IImageGraphWorkspace GetSelectedData();
        DataRenderingWorkspaceViewModel GetSelectedImage();
    }
}
