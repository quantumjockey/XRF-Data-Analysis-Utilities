///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Analysis
{
    public class SingleElementAnalysisWorkspaceViewModel : AnalysisWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-specific
        private SingleElementWorkspaceViewModel _selectedElement;

        #endregion

        ////////////////////////////////////////
        #region Properties and Indexers

        public WorkspaceViewModelCollection AvailableElements
        {
            get;
            set;
        }

        public SingleElementWorkspaceViewModel SelectedElement
        {
            get
            {
                return _selectedElement;
            }
            set
            {
                _selectedElement = value;
                OnPropertyChanged("SelectedElement");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor


        public SingleElementAnalysisWorkspaceViewModel(string header, ref xrfSample sample)
            : base(header, ref sample)
        {
            AvailableElements = new WorkspaceViewModelCollection();
            PopulateElementWorkspaces(ref sample);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void PopulateElementWorkspaces(ref xrfSample sample)
        {
            if (AvailableElements.Count > 0)
            {
                AvailableElements.Clear();
            }

            foreach (elementData item in Elements)
            {
                AvailableElements.Add(new SingleElementWorkspaceViewModel(item));
            }

            if (AvailableElements.Count > 0)
            {
                SelectedElement = AvailableElements[0] as SingleElementWorkspaceViewModel;
            }
        }

        #endregion
    }
}
