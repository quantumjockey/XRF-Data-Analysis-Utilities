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

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public SingleElementWorkspaceViewModel(string _elementName, ref xrfSample _sample)
        {
            ElementData = _sample.GetElementData(_elementName);
            ListAllPixelObjectsForGridDisplay(ref _sample);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void ListAllPixelObjectsForGridDisplay(ref xrfSample sample)
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
    }
}
