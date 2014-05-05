﻿///////////////////////////////////////
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
using XRF_Data_Analysis_Utilities.Model.Structures;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class XrfImageWorkspaceViewModel : SingleElementWorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        private Canvas _renderedImage;

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

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public XrfImageWorkspaceViewModel(string _elementName, ref xrfSample _sample)
            : base(_elementName, ref _sample)
        {
            InitializeDataMapping();
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void AddPixel(double x, double y, double xScale, double yScale, ref Canvas _renderedImage, ref pixel _data, int _maxR, int _maxG, int _maxB)
        {
            PixelViewModel xrfPix = new PixelViewModel(_data, xScale, yScale);
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
            RenderedImage = RenderImage(ElementData.ImageGridData, 500.0, 255, 255, 255);
        }

        #endregion

        ////////////////////////////////////////
        #region Event Handling


        void Graphic_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle pix = sender as Rectangle;
            base.SelectPixelByTag(pix.Tag.ToString());
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
