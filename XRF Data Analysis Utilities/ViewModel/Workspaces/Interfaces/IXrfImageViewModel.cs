///////////////////////////////////////
#region Namespace Directives

using LookinSharp.WPF.ViewModel;
using WpfHelper.ViewModel;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Interfaces
{
    public interface IXrfImageViewModel : IViewModel
    {
        // Property signatures
        ImageGraphViewModel ImageFrame { get; set; }
        ImageDataWorkspaceViewModel ImageSource { get; set; }
        RampWrapperWorkspaceViewModel RampContainer { get; set; }
    }
}
