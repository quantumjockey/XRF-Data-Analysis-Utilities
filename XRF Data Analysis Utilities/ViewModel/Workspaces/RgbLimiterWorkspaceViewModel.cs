using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfHelper.ViewModel.Workspaces;

namespace XRF_Data_Analysis_Utilities.ViewModel.Workspaces
{
    public class RgbLimiterWorkspaceViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Constants

        const int _maxRgbValue = 255;

        #endregion

        ////////////////////////////////////////
        #region Generic Fields

        // Channel-related
        private int _maxBlueChannel;
        private int _maxGreenChannel;
        private int _maxRedChannel;
        private double _scaleForBlueChannel;
        private double _scaleForGreenChannel;
        private double _scaleForRedChannel;

        #endregion

        ////////////////////////////////////////
        #region Channel-Related

        public int MaxBlueChannel
        {
            get
            {
                return _maxBlueChannel;
            }
            set
            {
                _maxBlueChannel = value;
                OnPropertyChanged("MaxBlueChannel");
            }
        }

        public int MaxGreenChannel
        {
            get
            {
                return _maxGreenChannel;
            }
            set
            {
                _maxGreenChannel = value;
                OnPropertyChanged("MaxGreenChannel");
            }
        }

        public int MaxRedChannel
        {
            get
            {
                return _maxRedChannel;
            }
            set
            {
                _maxRedChannel = value;
                OnPropertyChanged("MaxRedChannel");
            }
        }

        public double ScaleForBlueChannel
        {
            get
            {
                return _scaleForBlueChannel;
            }
            set
            {
                _scaleForBlueChannel = value;
                MaxBlueChannel = ScaleMaxChannelValue(value, _maxRgbValue);
                OnPropertyChanged("ScaleForBlueChannel");
            }
        }

        public double ScaleForGreenChannel
        {
            get
            {
                return _scaleForGreenChannel;
            }
            set
            {
                _scaleForGreenChannel = value;
                MaxGreenChannel = ScaleMaxChannelValue(value, _maxRgbValue);
                OnPropertyChanged("ScaleForGreenChannel");
            }
        }

        public double ScaleForRedChannel
        {
            get
            {
                return _scaleForRedChannel;
            }
            set
            {
                _scaleForRedChannel = value;
                MaxRedChannel = ScaleMaxChannelValue(value, _maxRgbValue);
                OnPropertyChanged("ScaleForRedChannel");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public RgbLimiterWorkspaceViewModel()
        {
            ScaleForRedChannel = ScaleForGreenChannel = ScaleForBlueChannel = 10.0;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private int ScaleMaxChannelValue(double _value, int _maxChannelValue)
        {
            return (int)((_value / 10.0) * _maxChannelValue);
        }

        #endregion
    }
}
