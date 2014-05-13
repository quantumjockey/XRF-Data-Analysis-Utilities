///////////////////////////////////////
#region Namespace Directives

using TheseColorsDontRun.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model.Structures;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class ImageGraphWrapperWorkspaceViewModel : ImageGraphWorkspaceViewModel, IImageGraphWrapperWorkspace
    {
        ////////////////////////////////////////
        #region Generic Fields

        public IColorRampWorkspaceViewModel ColorRamp
        {
            get;
            set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public ImageGraphWrapperWorkspaceViewModel(pixel[][] _data) : base(_data) { }

        #endregion
    }
}
