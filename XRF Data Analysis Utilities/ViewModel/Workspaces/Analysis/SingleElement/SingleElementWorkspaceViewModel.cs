///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.ObjectModel;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.Model.Structures;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class SingleElementWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        private pixel _selectedPixel;

        #endregion

        ////////////////////////////////////////
        #region Properties

        public ObservableCollection<pixel> CompletePixelsList
        {
            get;
            set;
        }

        public elementData ElementData
        {
            get;
            set;
        }

        public pixel SelectedPixel
        {
            get
            {
                return _selectedPixel;
            }
            set
            {
                _selectedPixel = value;
                OnPropertyChanged("SelectedPixel");
            }
        }

        public XrfImageWorkspaceViewModel ImageGraph
        {
            get;
            set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public SingleElementWorkspaceViewModel(elementData _data)
        {
            ElementData = _data;
            ImageGraph = new XrfImageWorkspaceViewModel(ElementData.Name, ElementData.ImageGridData);
            ImageGraph.ImageFrame.PropertyChanged += ImageFrame_PropertyChanged;
            ListAllPixelObjectsForGridDisplay();
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void ListAllPixelObjectsForGridDisplay()
        {
            CompletePixelsList = new ObservableCollection<pixel>();
            foreach (pixel[] row in ElementData.ImageGridData)
            {
                foreach (pixel column in row)
                {
                    CompletePixelsList.Add(column);
                }
            }
        }

        protected void SelectPixelByTag(string tag)
        {
            for (int i = 0; i < CompletePixelsList.Count; i++)
            {
                if (tag == CompletePixelsList[i].Tag)
                {
                    SelectedPixel = CompletePixelsList[i];
                }
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        void ImageFrame_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string _tag = (sender as DataRenderingWorkspaceViewModel).SelectedPixelTag;
            if (!String.IsNullOrEmpty(_tag))
            {
                SelectPixelByTag(_tag);
            }
        }

        #endregion
    }
}
