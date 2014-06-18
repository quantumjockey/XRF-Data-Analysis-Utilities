///////////////////////////////////////
#region Namespace Directives

using LookinSharp.WPF.ViewModel;
using System.Collections.ObjectModel;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.Model.Components;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Interfaces;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Analysis.MultipleElement
{
    public class MEAnalysisWorkspaceViewModel : AnalysisWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        private ObservableCollection<ElementMarkerViewModel> _selectedElements;
        private xrfPixel _selectedPixel;

        #endregion

        ////////////////////////////////////////
        #region Properties and Indexers

        public xrfPixel SelectedPixel
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


        public WorkspaceViewModelCollection AvailableElements
        {
            get;
            set;
        }

        public ObservableCollection<ElementMarkerViewModel> SelectedElements
        {
            get
            {
                return _selectedElements;
            }
            set
            {
                _selectedElements = value;
                OnPropertyChanged("SelectedElements");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public MEAnalysisWorkspaceViewModel(string header, ref xrfSample sample)
            : base(header, ref sample)
        {
            SelectedElements = new ObservableCollection<ElementMarkerViewModel>();
        }

        #endregion

        ////////////////////////////////////////
        #region Public Methods

        public override IImageDataWorkspace GetSelectedData()
        {
            return this.ImageGraph.ImageSource;
        }

        public override ImageGraphViewModel GetSelectedImage()
        {
            return this.ImageGraph.ImageFrame;
        }

        #endregion
    }
}
