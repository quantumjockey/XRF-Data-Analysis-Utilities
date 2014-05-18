///////////////////////////////////////
#region Namespace Directives

using WpfHelper.ViewModel;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public interface IXrfImageViewModel : IViewModel
    {
        // Property signatures
        DataRenderingViewModel ImageFrame { get; set; }
        ImageDataWorkspaceViewModel ImageSource { get; set; }
        RampWrapperWorkspaceViewModel RampContainer { get; set; }
    }
}
