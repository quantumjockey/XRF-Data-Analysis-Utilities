///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using TheseColorsDontRun.Extensions;
using TheseColorsDontRun.ViewModel.Workspaces;
using WpfHelper.PropertyChanged;
using WpfHelper.ViewModel;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;

#endregion
///////////////////////////////////////


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

        public DataRenderingWorkspaceViewModel(string _elementName, ref xrfSample _sample)
        {
            ElementData = _sample.GetElementData(_elementName);
            InitializeDataMapping();
            ListAllPixelsForGridDisplay();
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void AddPixel(double x, double y, double xScale, double yScale, ref Canvas _renderedImage, ref pixel _data, int _maxR, int _maxG, int _maxB)
        {
            XrfPixelViewModel xrfPix = new XrfPixelViewModel(_data, xScale, yScale);
            Canvas.SetTop(xrfPix.Graphic, y);
            Canvas.SetLeft(xrfPix.Graphic, x);
            xrfPix.Graphic.Fill = new SolidColorBrush(ColorRamp.Ramp.GetRelativeColor(_data.Temperature, _maxR, _maxG, _maxB, false));
            xrfPix.Graphic.MouseDown += Graphic_MouseDown;
            _renderedImage.Children.Add(xrfPix.Graphic);
        }


        private void InitializeDataMapping()
        {
            Color[] rampColors = new Color[]{ Colors.White, Colors.Blue, Colors.Red, Colors.Yellow };

            ColorRamp = new ColorRampWorkspaceViewModel(rampColors);
            ColorRamp.PropertyChanged += ColorRamp_PropertyChanged;

            RefreshImage();
        }


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


        private void RefreshImage()
        {
            RenderedImage = RenderImage(ElementData.ImageData, 500.0, 255, 255, 255);
        }

        #endregion

        ////////////////////////////////////////
        #region Event Handling
        

        void Graphic_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle pix = sender as Rectangle;

            for (int i = 0; i < CompleteDataListing.Count; i++)
            {
                if ((string)pix.Tag == (string)CompleteDataListing[i].Tag)
                {
                    SelectedPixel = CompleteDataListing[i];
                }
            }
        }


        void RgbLimits_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (RenderedImage != null)
            {
                RefreshImage();
            }
        }


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
