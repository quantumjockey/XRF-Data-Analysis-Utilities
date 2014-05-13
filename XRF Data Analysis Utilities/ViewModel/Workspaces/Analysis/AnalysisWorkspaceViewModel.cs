///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Analysis
{
    public abstract class AnalysisWorkspaceViewModel : WorkspaceViewModel, IAnalysisWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Properties and Indexers

        public xrfSample SampleData
        {
            get;
            set;
        }

        public ObservableCollection<elementData> Elements
        {
            get;
            set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public AnalysisWorkspaceViewModel(string header, ref xrfSample sample)
        {
            base.Header = header;
            SampleData = sample;
            Elements = new ObservableCollection<elementData>();
            PopulateElementsList(ref sample);
        }

        #endregion

        ////////////////////////////////////////
        #region Public Methods

        public virtual IImageGraphWorkspace GetSelectedData()
        {
            //must be overriden within each workspace
            throw new NotImplementedException();
        }

        public virtual DataRenderingWorkspaceViewModel GetSelectedImage()
        {
            //must be overriden within each workspace
            throw new NotImplementedException();
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void PopulateElementsList(ref xrfSample sample)
        {
            List<string> labelData = new List<string>(sample.Labels);
            int startIndex = labelData.IndexOf("Deadtime (%)");
            int endIndex = labelData.IndexOf("Full Counts");

            for (int i = startIndex + 1; i < endIndex; i++)
            {
                Elements.Add(sample.GetElementData(sample.Labels[i]));
            }
        }

        #endregion
    }
}
