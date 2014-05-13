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
        DataRenderingWorkspaceViewModel ImageFrame { get; set; }
    }
}
