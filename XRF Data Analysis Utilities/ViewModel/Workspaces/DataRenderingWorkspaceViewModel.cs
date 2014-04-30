using System;
using System.Collections.Generic;
using WpfHelper.PropertyChanged;
using WpfHelper.ViewModel;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.Utilities;

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using TheseColorsDontRun.ViewModel.Workspaces;


namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class DataRenderingWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        private Canvas _renderedImage;
        private pixel _selectedPixel;

        #endregion

        ////////////////////////////////////////
        #region Properties

        public ColorRampWorkspaceViewModel ColorRamp
        {
            get;
            set;
        }

        public ObservableCollection<pixel> CompleteDataListing
        {
            get;
            set;
        }

        public elementData ElementData
        {
            get;
            set;
        }

        public Canvas RenderedImage
        {
            get
            {
                return _renderedImage;
            }
            set
            {
                _renderedImage = value;
                OnPropertyChanged("RenderedImage");
            }
        }

        public RgbLimiterWorkspaceViewModel RgbLimits
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="sample"></param>
        public DataRenderingWorkspaceViewModel(string _elementName, ref xrfSample _sample)
        {
            ElementData = _sample.GetElementData(_elementName);
            InitializeDataMapping();
            ListAllPixelsForGridDisplay();
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xScale"></param>
        /// <param name="yScale"></param>
        /// <param name="_renderedImage"></param>
        /// <param name="_data"></param>
        /// <param name="_maxR"></param>
        /// <param name="_maxG"></param>
        /// <param name="_maxB"></param>
        private void AddPixel(double x, double y, double xScale, double yScale, ref Canvas _renderedImage, ref pixel _data, int _maxR, int _maxG, int _maxB)
        {
            XrfPixelViewModel xrfPix = new XrfPixelViewModel(_data, xScale, yScale);
            Canvas.SetTop(xrfPix.Graphic, y);
            Canvas.SetLeft(xrfPix.Graphic, x);
            xrfPix.Graphic.Fill = new SolidColorBrush(ColorRamp.Ramp.GetRelativeColor(_data.Temperature, _maxR, _maxG, _maxB, false));
            xrfPix.Graphic.MouseDown += Graphic_MouseDown;
            _renderedImage.Children.Add(xrfPix.Graphic);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeDataMapping()
        {
            RgbLimits = new RgbLimiterWorkspaceViewModel();
            RgbLimits.PropertyChanged += RgbLimits_PropertyChanged;

            ColorRamp = new ColorRampWorkspaceViewModel();
            ColorRamp.PropertyChanged += ColorRamp_PropertyChanged;

            RefreshImage();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ListAllPixelsForGridDisplay()
        {
            CompleteDataListing = new ObservableCollection<pixel>();
            foreach (pixel[] row in ElementData.ImageData)
            {
                foreach (pixel column in row)
                {
                    CompleteDataListing.Add(column);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageGrid"></param>
        /// <param name="imageSize"></param>
        /// <param name="maxR"></param>
        /// <param name="maxG"></param>
        /// <param name="maxB"></param>
        /// <returns></returns>
        private Canvas RenderImage(pixel[][] imageGrid, double imageSize, int maxR, int maxG, int maxB)
        {
            Canvas _effectiveImage = new Canvas();

            int imageRows = imageGrid.Length;
            int imageColumns = imageGrid[0].Length;

            double pixelScaleY = imageSize / imageRows;
            double pixelScaleX = imageSize / imageColumns;

            _effectiveImage.Height= _effectiveImage.Width = imageSize;

            AddPixel(0, 0, pixelScaleX, pixelScaleY, ref _effectiveImage, ref imageGrid[0][0], maxR, maxG, maxB);

            for (int i = 0; i < imageRows; i++)
            {
                for (int j = 0; j < imageColumns; j++)
                {
                    AddPixel(j * pixelScaleX, i * pixelScaleY, pixelScaleX, pixelScaleY, ref _effectiveImage, ref imageGrid[i][j], maxR, maxG, maxB);
                }
            }

            return _effectiveImage;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshImage()
        {
            RenderedImage = RenderImage(ElementData.ImageData, 400.0, RgbLimits.MaxRedChannel, RgbLimits.MaxGreenChannel, RgbLimits.MaxBlueChannel);
        }

        #endregion

        ////////////////////////////////////////
        #region Event Handling
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Graphic_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle pix = sender as Rectangle;

            for (int i = 0; i < CompleteDataListing.Count; i++)
            {
                if (pix.Tag == CompleteDataListing[i].Tag)
                {
                    SelectedPixel = CompleteDataListing[i];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RgbLimits_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (RenderedImage != null)
            {
                RefreshImage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ColorRamp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (RenderedImage != null)
            {
                RefreshImage();
            }
        }

        #endregion
    }
}
