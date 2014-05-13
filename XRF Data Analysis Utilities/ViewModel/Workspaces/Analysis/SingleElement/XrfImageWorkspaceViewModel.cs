﻿///////////////////////////////////////
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
    public class XrfImageWorkspaceViewModel : ImageDataWorkspaceViewModel, IXrfImageViewModel
    {
        ////////////////////////////////////////
        #region Constants

        const int _defaultImageSize = 480;

        #endregion

        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        private DataRenderingWorkspaceViewModel _imageFrame;
        private RampWrapperWorkspaceViewModel _rampContainer;

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
            : base(_data)
        {
            Header = _elementName;
            RampContainer = new RampWrapperWorkspaceViewModel(new Color[] { Colors.White, Colors.Blue, Colors.Red, Colors.Yellow }, (x) => ColorRampUpdateAction());
            ImageFrame = new DataRenderingWorkspaceViewModel(_defaultImageSize, (x) => LeftMouseClickAction(x), (y) => RightMouseClickAction(y), (temp, mr, mg, mb) => ColorFillAction(temp, mr, mg, mb));
            ImageFrame.RefreshImage(ImageData);
        }

        #endregion

        ////////////////////////////////////////
        #region Mouse-Click Actions


        private void LeftMouseClickAction(Rectangle _pix)
        {
            if (ImageFrame.SelectedPixelTag == _pix.Tag.ToString())
            {
                ZoomIn(ImageFrame.SelectedPixelTag);
                ImageFrame.RefreshImage(ImageData);
            }
        }


        private void RightMouseClickAction(Rectangle _pix)
        {
            if (ImageFrame.SelectedPixelTag == _pix.Tag.ToString())
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
            return RampContainer.ColorRamp.Ramp.MatchOffsetToColor(temperature, maxR, maxG, maxB, false);
        }

        #endregion

        ////////////////////////////////////////
        #region Color Ramp Change Action

        private void ColorRampUpdateAction()
        {
            if (ImageFrame.RenderedImage != null)
            {
                ImageFrame.RefreshImage(ImageData);
            }
        }

        #endregion
    }
}
