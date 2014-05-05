///////////////////////////////////////
#region Namespace Directives

using System;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class SampleWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // workspace-related
        private IWorkspaceViewModel _selectedImageWorkspace;

        #endregion

        ////////////////////////////////////////
        #region Properties and Indexers

        public xrfSample SampleData
        {
            get;
            set;
        }

        public IWorkspaceViewModel SelectedImageWorkspace
        {
            get
            {
                return _selectedImageWorkspace;
            }
            set
            {
                _selectedImageWorkspace = value;
                OnPropertyChanged("SelectedImageWorkspace");
            }
        }

        public WorkspaceViewModelCollection ImageWorkspaces
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
            ImageWorkspaces = new WorkspaceViewModelCollection();
            InitializeDataManipulationWorkspaces(ref sample);
            SampleData = sample;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private void InitializeDataManipulationWorkspaces(ref xrfSample _sample)
        {
            ImageWorkspaces.Add(new DataManipulationWorkspaceViewModel("Single-Element Analysis", ref _sample));
            SelectedImageWorkspace = ImageWorkspaces[0];
        }

        #endregion
    }
}
