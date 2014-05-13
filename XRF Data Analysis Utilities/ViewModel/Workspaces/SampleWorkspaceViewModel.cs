///////////////////////////////////////
#region Namespace Directives

using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Analysis;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class SampleWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // workspace-related
        private IAnalysisWorkspaceViewModel _selectedImageAnalysisWorkspace;

        #endregion

        ////////////////////////////////////////
        #region Properties and Indexers

        public xrfSample SampleData
        {
            get;
            set;
        }

        public IAnalysisWorkspaceViewModel SelectedImageAnalysisWorkspace
        {
            get
            {
                return _selectedImageAnalysisWorkspace;
            }
            set
            {
                _selectedImageAnalysisWorkspace = value;
                OnPropertyChanged("SelectedImageAnalysisWorkspace");
            }
        }

        public WorkspaceViewModelCollection ImageAnalysisWorkspaces
        {
            get;
            set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public SampleWorkspaceViewModel(string header, ref xrfSample sample)
        {
            base.Header = header;
            ImageAnalysisWorkspaces = new WorkspaceViewModelCollection();
            InitializeDataManipulationWorkspaces(ref sample);
            SampleData = sample;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private void InitializeDataManipulationWorkspaces(ref xrfSample _sample)
        {
            ImageAnalysisWorkspaces.Add(new SingleElementAnalysisWorkspaceViewModel("Single-Element Analysis", ref _sample));
            //ImageAnalysisWorkspaces.Add(new MultipleElementAnalysisWorkspaceViewModel("Multiple-Element Analysis", ref _sample));
            SelectedImageAnalysisWorkspace = ImageAnalysisWorkspaces[0] as IAnalysisWorkspaceViewModel;
        }

        #endregion
    }
}
