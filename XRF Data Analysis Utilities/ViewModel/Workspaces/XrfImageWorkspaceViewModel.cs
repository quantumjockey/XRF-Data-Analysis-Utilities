﻿///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using TheseColorsDontRun.Extensions;
using TheseColorsDontRun.ViewModel.Workspaces;
using WpfHelper.ViewModel;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.Model.Structures;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class XrfImageWorkspaceViewModel : ImageGraphWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Constants

        const double _imageSize = 480.0;
        const int _maxChannelValue = 255;

        #endregion

        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        private Canvas _renderedImage;
        private bool _renderToScale;
        private string _selectedPixelTag;

        #endregion

        ////////////////////////////////////////
        #region Properties

        public ColorRampWorkspaceViewModel ColorRamp
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

        public bool RenderToScale
        {
            get
            {
                return _renderToScale;
            }
            set
            {
                _renderToScale = value;
                RefreshImage();
                OnPropertyChanged("RenderToScale");
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
            InitializeDataMapping();
            RenderToScale = true;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void AddPixel(double x, double y, double xScale, double yScale, ref Canvas _renderedImage, ref pixel _data, int _maxR, int _maxG, int _maxB)
        {
            PixelViewModel xrfPix = new PixelViewModel(_data, xScale, yScale);
            Canvas.SetTop(xrfPix.Graphic, y);
            Canvas.SetLeft(xrfPix.Graphic, x);
            xrfPix.Graphic.Fill = new SolidColorBrush(ColorRamp.Ramp.MatchOffsetToColor(_data.Temperature, _maxR, _maxG, _maxB, false));
            xrfPix.Graphic.MouseDown += Graphic_MouseDown;
            xrfPix.Graphic.MouseLeftButtonDown += Graphic_MouseLeftButtonDown;
            xrfPix.Graphic.MouseRightButtonDown += Graphic_MouseRightButtonDown;
            _renderedImage.Children.Add(xrfPix.Graphic);
        }


        private void InitializeDataMapping()
        {
            Color[] rampColors = new Color[]{ Colors.White, Colors.Blue, Colors.Red, Colors.Yellow };

            ColorRamp = new ColorRampWorkspaceViewModel(true, rampColors);
            ColorRamp.PropertyChanged += ColorRamp_PropertyChanged;

            RefreshImage();
        }


        private Canvas RenderImage(pixel[][] imageGrid, double imageSize, int maxR, int maxG, int maxB)
        {
            Canvas _effectiveImage = new Canvas();

            int imageRows = imageGrid.Length;
            int imageColumns = imageGrid[0].Length;

            double pixelScaleX, pixelScaleY;

            if (RenderToScale)
            {
                int scaleDivisor = (imageRows > imageColumns) ? imageRows : imageColumns;
                pixelScaleY = pixelScaleX = imageSize / scaleDivisor;

                _effectiveImage.Height = (imageRows < imageColumns) ? (imageSize * imageRows / imageColumns) : imageSize;
                _effectiveImage.Width = (imageRows < imageColumns) ? imageSize : (imageSize * imageColumns / imageRows);
            }
            else
            {
                pixelScaleY = imageSize / imageRows;
                pixelScaleX = imageSize / imageColumns;

                _effectiveImage.Height = _effectiveImage.Width = imageSize;
            }

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
            RenderedImage = RenderImage(_imageData, _imageSize, _maxChannelValue, _maxChannelValue, _maxChannelValue);
        }

        #endregion

        ////////////////////////////////////////
        #region Event Handling


        void ColorRamp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (RenderedImage != null)
            {
                RefreshImage();
            }
        }


        void Graphic_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle pix = sender as Rectangle;
            SelectedPixelTag = pix.Tag.ToString();
        }


        void Graphic_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle pix = sender as Rectangle;
            if (SelectedPixelTag == pix.Tag.ToString())
            {
                ZoomIn(SelectedPixelTag);
                RefreshImage();
            }
        }


        void Graphic_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle pix = sender as Rectangle;
            if (SelectedPixelTag == pix.Tag.ToString())
            {
                ZoomOut(SelectedPixelTag);
                RefreshImage();
            }
        }

        #endregion
    }
}
