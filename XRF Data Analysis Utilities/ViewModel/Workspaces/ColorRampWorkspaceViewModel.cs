using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using WpfHelper.ViewModel.Workspaces;

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class ColorRampWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Render-specific
        private LinearGradientBrush _brush;
        private GradientStopCollection _ramp;
        private double _sliderValue;

        #endregion

        ////////////////////////////////////////
        #region Properties

        public LinearGradientBrush Brush
        {
            get
            {
                return _brush;
            }
            set
            {
                _brush = value;
                OnPropertyChanged("Brush");
            }
        }

        public GradientStopCollection Ramp
        {
            get
            {
                return _ramp;
            }
            set
            {
                _ramp = value;
                OnPropertyChanged("Ramp");
            }
        }

        public double SliderValue
        {
            get
            {
                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
                Refresh();
                OnPropertyChanged("SliderValue");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public ColorRampWorkspaceViewModel()
        {
            SliderValue = 5;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        public void Refresh()
        {
            Ramp = CreateColorRamp(SliderValue);
            Brush = RefreshBrush(Brush);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GradientStopCollection CreateColorRamp(double offset)
        {
            double stopOne, stopTwo, stopThree, stopFour;

            stopOne = 0.0;
            stopFour = 1.0;
            stopTwo = (offset / 10.0) * 0.5;
            stopThree = 1.0 - stopTwo;

            List<GradientStop> stops = new List<GradientStop>();
            stops.Add(new GradientStop(Colors.White, stopOne));
            stops.Add(new GradientStop(Colors.Blue, stopTwo));
            stops.Add(new GradientStop(Colors.Red, stopThree));
            stops.Add(new GradientStop(Colors.Yellow, stopFour));
            return new GradientStopCollection(stops);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <returns></returns>
        private LinearGradientBrush RefreshBrush(LinearGradientBrush brush)
        {
            if (brush == null)
            {
                brush = new LinearGradientBrush();
            }
            brush.GradientStops.Clear();
            foreach (GradientStop item in _ramp)
            {
                brush.GradientStops.Add(item);
            }
            brush.StartPoint = new System.Windows.Point(0, 1);
            brush.EndPoint = new System.Windows.Point(0, 0);
            return brush;
        }

        #endregion
    }
}
