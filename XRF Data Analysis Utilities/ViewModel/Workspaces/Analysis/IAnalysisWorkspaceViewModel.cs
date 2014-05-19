///////////////////////////////////////
#region Namespace Directives

using LookinSharp.WPF.ViewModel;
using System.Collections.ObjectModel;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Interfaces;

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
        IImageDataWorkspace GetSelectedData();
        ImageGraphViewModel GetSelectedImage();
    }
}
