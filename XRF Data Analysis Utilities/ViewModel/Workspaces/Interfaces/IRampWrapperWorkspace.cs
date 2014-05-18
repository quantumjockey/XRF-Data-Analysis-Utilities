///////////////////////////////////////
#region Namespace Directives

using TheseColorsDontRun.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Interfaces
{
    public interface IRampWrapperWorkspace
    {
        // Property signatures
        IColorRampWorkspaceViewModel ColorRamp { get; set; }
    }
}
