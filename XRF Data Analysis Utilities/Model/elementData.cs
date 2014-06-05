///////////////////////////////////////
#region Namespace Directives

using System;
using XRF_Data_Analysis_Utilities.Model.Components;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model
{
    public class elementData
    {
        ////////////////////////////////////////
        #region Properties

        public xrfPixel[][] ImageGridData { get; set; }

        public int MaxCounts { get; set; }

        public int MinCounts { get; set; }

        public string Name { get; set; }

        #endregion

        ////////////////////////////////////////
        #region Constructor


        public elementData(int beamHeight, int beamWidth, int index, string name, double[][][] sortedData)
        {
            Name = name;
            DetermineMaxima(beamHeight, beamWidth, index, sortedData);
            ImageGridData = ExtractElementRelatedPixelData(beamHeight, beamWidth, index, sortedData);      
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private double CalculateGradientTemperature(int value)
        {
            return (double)(value - MinCounts) / (double)(MaxCounts - MinCounts);
        }


        private void DetermineMaxima(int beamHeight, int beamWidth, int index, double[][][] sortedData)
        {
            MaxCounts = 0;
            MinCounts = 9 * 1000000;

            for (int i = 0; i < beamHeight; i++)
            {
                for (int j = 0; j < beamWidth; j++)
                {
                    int counts = (int)sortedData[i][j][index];

                    if (counts > MaxCounts)
                        MaxCounts = counts;

                    if (counts < MinCounts)
                        MinCounts = counts;
                }
            }
        }


        private xrfPixel[][] ExtractElementRelatedPixelData(int beamHeight, int beamWidth, int index, double[][][] sortedData)
        {
            xrfPixel[][] pixelData = new xrfPixel[beamHeight][];

            for (int i = 0; i < beamHeight; i++)
            {
                pixelData[i] = new xrfPixel[beamWidth];
                for (int j = 0; j < beamWidth; j++)
                {
                    double[] row = sortedData[i][j];
                    double xActual = row[2];
                    double yActual = row[3];
                    double xGoal = row[0];
                    double yGoal = row[1];
                    int counts = (int)row[index];
                    double temperature = CalculateGradientTemperature(counts);

                    pixelData[i][j] = new xrfPixel(xActual, yActual, xGoal, yGoal, counts, temperature);
                }
            }

            return pixelData;
        }

        #endregion
    }
}
