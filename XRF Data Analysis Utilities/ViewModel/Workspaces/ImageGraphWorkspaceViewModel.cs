///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model.Structures;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class ImageGraphWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        protected pixel[][] _imageData;

        // data context
        private Point _origin;
        private Point _xMax;
        private Point _yMax;

        // for zoom
        private double _zoom;

        #endregion

        ////////////////////////////////////////
        #region Image Metadata

        // For color ramp slider
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        // for image zoom
        public Point Origin
        {
            get
            {
                return _origin;
            }
            set
            {
                _origin = value;
                OnPropertyChanged("Origin");
            }
        }

        public Point Xmax
        {
            get
            {
                return _xMax;
            }
            set
            {
                _xMax = value;
                OnPropertyChanged("Xmax");
            }
        }

        public Point Ymax
        {
            get
            {
                return _yMax;
            }
            set
            {
                _yMax = value;
                OnPropertyChanged("Ymax");
            }
        }

        public double Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
                OnPropertyChanged("Zoom");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public ImageGraphWorkspaceViewModel(pixel[][] _data)
        {
            _imageData = _data;
            GetDataMaxima();
            GetReferenceCoordinates();
            Zoom = 1.0;
        }

        #endregion

        ////////////////////////////////////////
        #region Public Methods

        public void ZoomIn()
        {

        }

        public void ZoomOut()
        {

        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void GetDataMaxima()
        {
            for (int i = 0; i < _imageData.Length; i++)
            {
                for (int j = 0; j < _imageData[0].Length; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        MaxValue = MinValue = _imageData[i][j].Counts;
                    }
                    else
                    {
                        int counts = _imageData[i][j].Counts;

                        if (counts > MaxValue)
                        {
                            MaxValue = counts;
                        }

                        if (counts < MinValue)
                        {
                            MinValue = counts;
                        }
                    }
                }
            }
        }


        private void GetReferenceCoordinates()
        {
            int yBound = _imageData.Length - 1;
            int xBound = _imageData[0].Length - 1;
            Ymax = _imageData[0][0].Goal;
            Origin = _imageData[yBound][0].Goal;
            Xmax = _imageData[yBound][xBound].Goal;
        }

        #endregion
    }
}
