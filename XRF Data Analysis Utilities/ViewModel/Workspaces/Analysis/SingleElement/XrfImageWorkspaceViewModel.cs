///////////////////////////////////////
#region Namespace Directives

using System.Windows.Media;
using System.Windows.Shapes;
using TheseColorsDontRun.Extensions;
using TheseColorsDontRun.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model.Structures;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class XrfImageWorkspaceViewModel : ImageGraphWrapperWorkspaceViewModel, IXrfImageViewModel
    {
        ////////////////////////////////////////
        #region Constants

        const int _imageSize = 480;
        const int _maxChannelValue = 255;

        #endregion

        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        private DataRenderingWorkspaceViewModel _imageFrame;
        private string _selectedPixelTag;

        #endregion

        ////////////////////////////////////////
        #region Properties

        public DataRenderingWorkspaceViewModel ImageFrame
        {
            get
            {
                return _imageFrame;
            }
            set
            {
                _imageFrame = value;
                OnPropertyChanged("ImageFrame");
            }
        }

        public string SelectedPixelTag
        {
            get
            {
                return _selectedPixelTag;
            }
            set
            {
                _selectedPixelTag = value;
                OnPropertyChanged("SelectedPixelTag");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public XrfImageWorkspaceViewModel(string _elementName, pixel[][] _data)
            : base(_data)
        {
            Header = _elementName;
            InitializeDataMapping(new Color[] { Colors.White, Colors.Blue, Colors.Red, Colors.Yellow });
            ImageFrame = new DataRenderingWorkspaceViewModel(_imageSize, (x) => LeftMouseClickAction(x), (y) => RightMouseClickAction(y), (temp, mr, mg, mb) => ColorFillAction(temp, mr, mg, mb));
            ImageFrame.RefreshImage(ImageData);
            ImageFrame.PropertyChanged += ImageFrame_PropertyChanged;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void InitializeDataMapping(Color[] rampColors)
        {
            ColorRamp = new ColorRampWorkspaceViewModel(true, rampColors);
            ColorRamp.PropertyChanged += ColorRamp_PropertyChanged;
        }

        #endregion

        ////////////////////////////////////////
        #region Mouse-Click Actions

        private void LeftMouseClickAction(object sender)
        {
            Rectangle pix = sender as Rectangle;
            if (ImageFrame.SelectedPixelTag == pix.Tag.ToString())
            {
                ZoomIn(ImageFrame.SelectedPixelTag);
                ImageFrame.RefreshImage(ImageData);
            }
        }

        private void RightMouseClickAction(object sender)
        {
            Rectangle pix = sender as Rectangle;
            if (ImageFrame.SelectedPixelTag == pix.Tag.ToString())
            {
                ZoomOut(ImageFrame.SelectedPixelTag);
                ImageFrame.RefreshImage(ImageData);
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Color Fill Action

        private Color ColorFillAction(double temperature, int maxR, int maxB, int maxG)
        {
            return ColorRamp.Ramp.MatchOffsetToColor(temperature, maxR, maxG, maxB, false);
        }

        #endregion

        ////////////////////////////////////////
        #region Event Handling


        void ColorRamp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (ImageFrame.RenderedImage != null)
            {
                ImageFrame.RefreshImage(ImageData);
            }
        }


        void ImageFrame_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SelectedPixelTag = ImageFrame.SelectedPixelTag;
        }

        #endregion
    }
}
