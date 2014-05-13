///////////////////////////////////////
#region Namespace Directives

using System.Windows;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model.Structures;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public abstract class ImageGraphWorkspaceViewModel : WorkspaceViewModel, IImageGraphWorkspace
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        private pixel[][] _baseImageData;

        // data context
        private Point _origin;
        private Point _xMax;
        private Point _yMax;

        // for zoom
        private int _zoom;

        #endregion

        ////////////////////////////////////////
        #region Image Metadata

        public pixel[][] ImageData
        {
            get;
            set;
        }

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

        public int Zoom
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
            _baseImageData = ImageData = _data;
            GetDataMaxima();
            GetReferenceCoordinates();
            Zoom = 1;
        }

        #endregion

        ////////////////////////////////////////
        #region Public Methods

        public void ZoomIn(string _selectedTag)
        {
            int input = (int)Zoom * 2;
            int maxWidth = _baseImageData[0].Length;
            if (input <= maxWidth)
            {
                Zoom = input;
                IsolateImageGridUnderZoom(_selectedTag);
                GetReferenceCoordinates();
            }
        }

        public void ZoomOut(string _selectedTag)
        {
            int input = (int)Zoom / 2;
            Zoom = (input >= 1) ? input : 1;
            IsolateImageGridUnderZoom(_selectedTag);
            GetReferenceCoordinates();
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void GetDataMaxima()
        {
            for (int i = 0; i < _baseImageData.Length; i++)
            {
                for (int j = 0; j < _baseImageData[i].Length; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        MaxValue = MinValue = _baseImageData[i][j].Counts;
                    }
                    else
                    {
                        int counts = _baseImageData[i][j].Counts;

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
            int yBound = ImageData.Length - 1;
            int xBound = ImageData[0].Length - 1;
            Ymax = ImageData[0][0].Goal;
            Origin = ImageData[yBound][0].Goal;
            Xmax = ImageData[yBound][xBound].Goal;
        }


        private void IsolateImageGridUnderZoom(string selectedTag)
        {
            int selectedCenterX, selectedCenterY;
            int height = _baseImageData.Length;
            int width = _baseImageData[0].Length;
            selectedCenterX = selectedCenterY = 0;

            // search for new approximate center indicated by zoom
            for (int i = 0; i < _baseImageData.Length; i++)
            {
                for (int j = 0; j < _baseImageData[i].Length; j++)
                {
                    if (selectedTag == _baseImageData[i][j].Tag)
                    {
                        selectedCenterY = i;
                        selectedCenterX = j;
                    }
                }
            }

            // get new dimensions
            int heightTemp = height / (int)Zoom;
            int widthTemp = width / (int)Zoom;
            int newHeight = (heightTemp >= 1) ? heightTemp : 1;
            int newWidth = (widthTemp >= 1) ? widthTemp : 1;

            ///////////////////////////////// NOTE TO SELF - REVIEW THIS (BELOW) MESS WHEN AWAKE //////////////////////////////
            // determine new center
            int tempX = newWidth / 2;
            int tempY = newHeight / 2;


            int newCenterX = 0;
            int newCenterY = 0;
            if (((selectedCenterX - tempX) >= 0) && ((selectedCenterX + tempX) < width))
            {
                newCenterX = selectedCenterX;
            }
            else
            {
                if ((selectedCenterX - tempX) < 0)
                {
                    newCenterX = tempX;
                }

                if ((selectedCenterX + tempX) >= width)
                {
                    newCenterX = width - tempX;
                }
            }

            if (((selectedCenterY - tempY) >= 0) && ((selectedCenterY + tempY) < height))
            {
                newCenterY = selectedCenterY;
            }
            else
            {
                if ((selectedCenterY - tempY) < 0)
                {
                    newCenterY = tempY;
                }

                if ((selectedCenterY + tempY) >= height)
                {
                    newCenterY = height - tempY;
                }
            }

            // create a new image data array
            ImageData = new pixel[newHeight][];

            // populate the array
            int tempTlx = newCenterX - tempX;
            int tempTly = newCenterY - tempY;
            int maxTlx = width - newWidth;
            int maxTly = height - newHeight;
            int topLeftX = (tempTlx <= maxTlx) ? tempTlx : maxTlx;
            int topLeftY = (tempTly <= maxTly) ? tempTly : maxTly;

            ///////////////////////////////// NOTE TO SELF - REVIEW THIS (ABOVE) MESS WHEN AWAKE //////////////////////////////

            for (int i = 0; i < newHeight; i++)
            {
                ImageData[i] = new pixel[newWidth];
                for (int j = 0; j < newWidth; j++)
                {
                    ImageData[i][j] = _baseImageData[topLeftY + i][topLeftX + j];
                }
            }

        }

        #endregion
    }
}
