/////////////////////////////////////////
//#region Namespace Directives

//using System.Windows.Media;
//using System.Windows.Shapes;
//using TheseColorsDontRun.Extensions;
//using TheseColorsDontRun.ViewModel.Workspaces;
//using WpfHelper.ViewModel.Workspaces;
//using XRF_Data_Analysis_Utilities.Model.Structures;

//#endregion
/////////////////////////////////////////

//namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
//{
//    public class TestWorkspace : WorkspaceViewModel
//    {
//        ////////////////////////////////////////
//        #region Constants

//        const int _defaultImageSize = 480;

//        #endregion

//        ////////////////////////////////////////
//        #region Generic Fields

//        // Workspace-Specific
//        private DataRenderingWorkspaceViewModel _imageFrame;

//        #endregion

//        ////////////////////////////////////////
//        #region Properties

//        public DataRenderingWorkspaceViewModel ImageFrame
//        {
//            get
//            {
//                return _imageFrame;
//            }
//            set
//            {
//                _imageFrame = value;
//                OnPropertyChanged("ImageFrame");
//            }
//        }

//        public IColorRampWorkspaceViewModel ColorRamp
//        {
//            get;
//            set;
//        }

//        #endregion

//        ////////////////////////////////////////
//        #region Constructor

//        public TestWorkspace(string _elementName, pixel[][] _data)
//            : base(_data)
//        {
//            Header = _elementName;
//            InitializeDataMapping(new Color[] { Colors.White, Colors.Blue, Colors.Red, Colors.Yellow });
//            ImageFrame = new DataRenderingWorkspaceViewModel(_defaultImageSize, (x) => LeftMouseClickAction(x), (y) => RightMouseClickAction(y), (temp, mr, mg, mb) => ColorFillAction(temp, mr, mg, mb));
//            ImageFrame.RefreshImage(ImageData);
//        }

//        #endregion

//        ////////////////////////////////////////
//        #region Supporting Methods


//        private void InitializeDataMapping(Color[] rampColors)
//        {
//            ColorRamp = new ColorRampWorkspaceViewModel(true, rampColors);
//            ColorRamp.PropertyChanged += ColorRamp_PropertyChanged;
//        }

//        #endregion

//        ////////////////////////////////////////
//        #region Mouse-Click Actions


//        private void LeftMouseClickAction(Rectangle _pix)
//        {
//            if (ImageFrame.SelectedPixelTag == _pix.Tag.ToString())
//            {
//                ZoomIn(ImageFrame.SelectedPixelTag);
//                ImageFrame.RefreshImage(ImageData);
//            }
//        }


//        private void RightMouseClickAction(Rectangle _pix)
//        {
//            if (ImageFrame.SelectedPixelTag == _pix.Tag.ToString())
//            {
//                ZoomOut(ImageFrame.SelectedPixelTag);
//                ImageFrame.RefreshImage(ImageData);
//            }
//        }

//        #endregion

//        ////////////////////////////////////////
//        #region Color Fill Action


//        private Color ColorFillAction(double temperature, int maxR, int maxB, int maxG)
//        {
//            return ColorRamp.Ramp.MatchOffsetToColor(temperature, maxR, maxG, maxB, false);
//        }

//        #endregion

//        ////////////////////////////////////////
//        #region Event Handling


//        void ColorRamp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
//        {
//            if (ImageFrame.RenderedImage != null)
//            {
//                ImageFrame.RefreshImage(ImageData);
//            }
//        }

//        #endregion
//    }
//}
