///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows.Shapes;
using XRF_Data_Analysis_Utilities.Model.Structures;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel
{
    public class PixelViewModel
    {
        ////////////////////////////////////////
        #region Properties

        public Rectangle Graphic
        {
            get;
            private set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public PixelViewModel(pixel _data, double _xScale, double _yScale)
        {
            Graphic = InitializeRectangle(_yScale, _xScale);
            Graphic.Tag = _data.Tag;
            Graphic.ToolTip = GenerateToolTip(_data);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private string GenerateToolTip(pixel _pixelData)
        {
            return "Counts: " + _pixelData.Counts
                + Environment.NewLine + "xGoal: " + _pixelData.Goal.X + " | yGoal: " + _pixelData.Goal.Y
                + Environment.NewLine + "xActual: " + _pixelData.Actual.X + " | yActual: " + _pixelData.Actual.Y;
        }

        private Rectangle InitializeRectangle(double _height, double _width)
        {
            Rectangle rec = new Rectangle();
            rec.Height = _height;
            rec.Width = _width;
            return rec;
        }

        #endregion
    }
}
