///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class SingleElementAnalysisWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-specific
        private SingleElementWorkspaceViewModel _selectedElement;

        #endregion

        ////////////////////////////////////////
        #region Properties and Indexers

        public xrfSample SampleData
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

        public WorkspaceViewModelCollection AvailableElements
        {
            get;
            set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public SingleElementAnalysisWorkspaceViewModel(string header, ref xrfSample sample)
        {
            base.Header = header;
            AvailableElements = new WorkspaceViewModelCollection();
            SampleData = sample;
            PopulateImagesList(ref sample);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void PopulateImagesList(ref xrfSample sample)
        {
            if (AvailableElements.Count > 0)
            {
                AvailableElements.Clear();
            }

            List<string> labelData = new List<string>(sample.Labels);
            int startIndex = labelData.IndexOf("Deadtime (%)");
            int endIndex = labelData.IndexOf("Full Counts");

            List<string> elements = new List<string>();

            for (int i = startIndex + 1; i < endIndex; i++)
            {
                elements.Add(sample.Labels[i]);
            }

            foreach (string item in elements)
            {
                AvailableElements.Add(new SingleElementWorkspaceViewModel(item, ref sample));
            }

            if (AvailableElements.Count > 0)
            {
                SelectedElement = AvailableElements[0] as SingleElementWorkspaceViewModel;
            }
        }

        #endregion
    }
}
