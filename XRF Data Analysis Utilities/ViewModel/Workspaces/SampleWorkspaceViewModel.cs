///////////////////////////////////////
#region Namespace Directives

using CompUhaul.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using WpfHelper.PropertyChanged;
using WpfHelper.ViewModel;
using WpfHelper.ViewModel.Controls;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.Files;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class SampleWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-specific
        private DataRenderingWorkspaceViewModel _selectedXRFImage;

        #endregion

        ////////////////////////////////////////
        #region Properties and Indexers

        public ObservableCollection<xrfPixel> CompletePixelsList
        {
            get;
            set;
        }

        public xrfSample SampleData
        {
            get;
            set;
        }

        public DataRenderingWorkspaceViewModel SelectedXRFImage
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
        #region Action-Oriented

        public CommandDrivenControlViewModel DeactivateWorkspace
        {
            get;
            set;
        }

        private void InitializeActions()
        {
            DeactivateWorkspace = new CommandDrivenControlViewModel((x) => MakeWorkspaceInactive(), "Close", "Close this sample.");
        }

        private void MakeWorkspaceInactive()
        {
            base.IsActive = false;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public SampleWorkspaceViewModel(string header, ref xrfSample sample)
        {
            base.Header = header;
            this.IsActive = true;
            //ListAllPixelsForGridDisplay(ref sample);
            ListAllPixelObjectsForGridDisplay(ref sample);
            XRFImages = new WorkspaceViewModelCollection();
            SampleData = sample;
            PopulateImagesList(ref sample);
            InitializeActions();
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        //private void ListAllPixelsForGridDisplay(ref xrfSample sample)
        //{
        //    CompletePixelsList = new ObservableCollection<double[]>();
        //    foreach (double[][] row in sample.RawPixelData)
        //    {
        //        foreach (double[] column in row)
        //        {
        //            CompletePixelsList.Add(column);
        //        }
        //    }
        //}


        private void ListAllPixelObjectsForGridDisplay(ref xrfSample sample)
        {
            CompletePixelsList = new ObservableCollection<xrfPixel>();
            foreach (xrfPixel[] row in sample.PixelData)
            {
                foreach (xrfPixel column in row)
                {
                    CompletePixelsList.Add(column);
                }
            }
        }


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
                XRFImages.Add(new DataRenderingWorkspaceViewModel(item, ref sample));
            }

            if (XRFImages.Count > 0)
            {
                SelectedXRFImage = XRFImages[0] as DataRenderingWorkspaceViewModel;
            }
        }

        #endregion
    }
}
