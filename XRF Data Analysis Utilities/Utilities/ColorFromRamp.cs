///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Utilities
{
    public static class ColorFromRamp
    {
        ////////////////////////////////////////
        #region Public Methods

        public static Color GetRelativeColor(this GradientStopCollection ramp, double offset, int maxR, int maxG, int maxB, bool alphaEnabled)
        {
            double startBoundaryOffset = 0.0;
            double finishBoundaryOffset = 1.0;

            GradientStop startBoundary = new GradientStop();
            GradientStop finishBoundary = new GradientStop();

            foreach (GradientStop boundary in ramp)
            {
                if (boundary.Offset <= offset && boundary.Offset > startBoundaryOffset)
                {
                    startBoundary = boundary;
                }
                if (boundary.Offset > offset && boundary.Offset <= finishBoundaryOffset)
                {
                    finishBoundary = boundary;
                    break;
                }
            }

            var color = Color.FromScRgb(
                (alphaEnabled) ? CalculateChannelValue(startBoundary, finishBoundary, "ScA", offset, 255) : (float)1.0,
                CalculateChannelValue(startBoundary, finishBoundary, "ScR", offset, maxR),
                CalculateChannelValue(startBoundary, finishBoundary, "ScG", offset, maxG),
                CalculateChannelValue(startBoundary, finishBoundary, "ScB", offset, maxB)
                );

            return color;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private static float CalculateChannelValue(GradientStop _before, GradientStop _after, string _colorComponent, double _offset, int _maxValue)
        {
            double afterOffset = _after.Offset;
            double beforeOffset = _before.Offset;

            float max = (float)_maxValue / (float)255;

            float afterColorChannelValue = GetScRgbChannelValue(_after.Color, _colorComponent);
            float beforeColorChannelValue = GetScRgbChannelValue(_before.Color, _colorComponent);

            double scaleFactor = (_offset - beforeOffset) / (afterOffset - beforeOffset);

            double channelRange = afterColorChannelValue - beforeColorChannelValue;

            float newChannel = (float)(scaleFactor * channelRange);

            float result = (float)(newChannel + beforeColorChannelValue);

            return (result < max) ? result : max;
        }

        private static float GetScRgbChannelValue(Color _color, string _channelName)
        {
            float channelValue = (float)0.0;

            PropertyInfo[] properties = (typeof(Color)).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name == _channelName)
                {
                    channelValue = (float)properties[i].GetValue(_color, null);
                    break;
                }
            }

            return channelValue;
        }

        #endregion
    }
}
