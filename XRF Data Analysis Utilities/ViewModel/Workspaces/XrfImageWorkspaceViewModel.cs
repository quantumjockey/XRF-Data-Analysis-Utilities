///////////////////////////////////////
#region Namespace Directives

using System.Windows.Media;
using System.Windows.Shapes;
using TheseColorsDontRun.Extensions;
using TheseColorsDontRun.ViewModel.Workspaces;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model.Components;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Interfaces;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class XrfImageWorkspaceViewModel : WorkspaceViewModel, IXrfImageViewModel
    {
        ////////////////////////////////////////
        #region Constants

        const int _defaultImageSize = 480;

        #endregion

        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        private DataRenderingViewModel _imageFrame;
        private ImageDataWorkspaceViewModel _imageSource;
        private RampWrapperWorkspaceViewModel _rampContainer;

        #endregion

        ////////////////////////////////////////
        #region Properties

        public DataRenderingViewModel ImageFrame
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

        public ImageDataWorkspaceViewModel ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public RampWrapperWorkspaceViewModel RampContainer
        {
            get
            {
                return _rampContainer;
            }
            set
            {
                _rampContainer = value;
                OnPropertyChanged("RampContainer");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public XrfImageWorkspaceViewModel(string _elementName, pixel[][] _data)
        {
            Header = _elementName;
            ImageSource = new ImageDataWorkspaceViewModel(_elementName, _data);
            RampContainer = new RampWrapperWorkspaceViewModel(new Color[] { Colors.White, Colors.Blue, Colors.Red, Colors.Yellow }, (x) => ColorRampUpdateAction());
            ImageFrame = new DataRenderingViewModel(_defaultImageSize, (x) => LeftMouseClickAction(x), (y) => RightMouseClickAction(y), (temp, mr, mg, mb) => ColorFillAction(temp, mr, mg, mb));
            ImageFrame.RefreshImage(ImageSource.ImageData);
        }

        #endregion

        ////////////////////////////////////////
        #region Mouse-Click Actions


        private void LeftMouseClickAction(Rectangle _pix)
        {
            if (ImageFrame.SelectedPixelTag == _pix.Tag.ToString())
            {
                ImageSource.ZoomIn(ImageFrame.SelectedPixelTag);
                ImageFrame.RefreshImage(ImageSource.ImageData);
            }
        }


        private void RightMouseClickAction(Rectangle _pix)
        {
            if (ImageFrame.SelectedPixelTag == _pix.Tag.ToString())
            {
                ImageSource.ZoomOut(ImageFrame.SelectedPixelTag);
                ImageFrame.RefreshImage(ImageSource.ImageData);
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Color Fill Action

        private Color ColorFillAction(double temperature, int maxR, int maxB, int maxG)
        {
            return RampContainer.ColorRamp.Ramp.MatchOffsetToColor(temperature, maxR, maxG, maxB, false);
        }

        #endregion

        ////////////////////////////////////////
        #region Color Ramp Change Action

        private void ColorRampUpdateAction()
        {
            if (ImageFrame.RenderedImage != null)
            {
                ImageFrame.RefreshImage(ImageSource.ImageData);
            }
        }

        #endregion
    }
}
