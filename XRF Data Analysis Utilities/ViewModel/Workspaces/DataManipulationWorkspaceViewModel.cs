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
    public class DataManipulationWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-specific
        private XrfImageWorkspaceViewModel _selectedXRFImage;

        #endregion

        ////////////////////////////////////////
        #region Properties and Indexers

        public xrfSample SampleData
        {
            get;
            set;
        }

        public XrfImageWorkspaceViewModel SelectedXRFImage
        {
            get
            {
                return _selectedXRFImage;
            }
            set
            {
                _selectedXRFImage = value;
                OnPropertyChanged("SelectedXRFImage");
            }
        }

        public WorkspaceViewModelCollection XRFImages
        {
            get;
            set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public DataManipulationWorkspaceViewModel(string header, ref xrfSample sample)
        {
            base.Header = header;
            XRFImages = new WorkspaceViewModelCollection();
            SampleData = sample;
            PopulateImagesList(ref sample);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void PopulateImagesList(ref xrfSample sample)
        {
            if (XRFImages.Count > 0)
            {
                XRFImages.Clear();
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
                XRFImages.Add(new XrfImageWorkspaceViewModel(item, ref sample));
            }

            if (XRFImages.Count > 0)
            {
                SelectedXRFImage = XRFImages[0] as XrfImageWorkspaceViewModel;
            }
        }

        #endregion
    }
}
