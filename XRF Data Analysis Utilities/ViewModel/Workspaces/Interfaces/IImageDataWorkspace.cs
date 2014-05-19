///////////////////////////////////////
#region Namespace Directives

using WpfHelper.ViewModel;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model.Components;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Interfaces
{
    public interface IImageDataWorkspace : IWorkspaceViewModel, IViewModel
    {
        // Property signatures
        xrfPixel[][] ImageData { get; set; }
        int MaxValue { get; set; }
        int MinValue { get; set; }
        int Zoom { get; set; }

        // Method Signatures
        void ZoomIn(string _selectedTag);
        void ZoomOut(string _selectedTag);
    }
}
