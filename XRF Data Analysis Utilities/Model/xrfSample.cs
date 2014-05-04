///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using XRF_Data_Analysis_Utilities.Model.Components;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model
{
    public class xrfSample
    {
        ////////////////////////////////////////
        #region Properties

        public int BeamHeight { get; set; }

        public int BeamWidth { get; set; }

        public string[] Labels { get; set; }

        public xrfPixel[][] PixelData { get; set; }

        public double[][][] RawPixelData { get; set; }

        public int TotalPixels
        {
            get
            {
                return RawPixelData.Length * RawPixelData[0].Length;
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Properties (Metadata)

        public double Exposure { get; set; }

        public string Instrument { get; set; }

        public motorGroup Motors { get; set; }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public xrfSample(string[] labels, string[][] metaData, double[][] pixelData)
        {
            Motors = new motorGroup();
            ParseMetaData(metaData);
            this.Labels = labels;
            RawPixelData = SortPixelData(pixelData);
            GetBeamHeightAndWidth();
            PixelData = ConvertRawDataToObjects(Labels, RawPixelData);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private xrfPixel[][] ConvertRawDataToObjects(string[] labels, double[][][] rawData)
        {
            int height = rawData.Length;
            int width = rawData[0].Length;

            xrfPixel[][] convertedData = new xrfPixel[height][];

            for (int i = 0; i < height; i++)
            {
                convertedData[i] = new xrfPixel[width];

                for (int j = 0; j < width; j++)
                {
                    convertedData[i][j] = new xrfPixel(rawData[i][j], labels);
                }
            }

            return convertedData;
        }


        private void GetBeamHeightAndWidth()
        {
            BeamHeight = RawPixelData.Length;
            BeamWidth = RawPixelData[0].Length;
        }


        public elementData GetElementData(string elementName)
        {
            int elementIndex = (new List<string>(Labels)).IndexOf(elementName);

            elementData vehicle = new elementData(BeamHeight, BeamWidth, elementIndex, elementName, RawPixelData);

            return vehicle;
        }


        private void ParseMetaData(string[][] data)
        {
            foreach (string[] item in data)
            {
                if (item.Length == 1)
                {
                    Motors.Pattern = item[0];
                }

                switch (item[0])
                {
                    case "Instrument":
                        this.Instrument = item[1];
                        break;

                    case "Delay After Move (s)":
                        Motors.DelayAfterMovement = Convert.ToDouble(item[1]);
                        break;

                    case "Stay at End":
                        Motors.StayAtEnd = Convert.ToDouble(item[1]);
                        break;

                    case "Exposure (s)":
                        this.Exposure = Convert.ToDouble(item[1]);
                        break;
                }
            }
        }


        private double[][][] SortPixelData(double[][] pixelData)
        {
            List<double[][]> rows = new List<double[][]>();
            List<double[]> row = new List<double[]>();

            int i = 1;

            row.Add(pixelData[0]);

            while (i < pixelData.Length)
            {
                if (pixelData[i][1] != pixelData[i - 1][1])
                {
                    rows.Add(row.ToArray());
                    row.Clear();
                }

                row.Add(pixelData[i]);
                i++;
            }

            return rows.ToArray();
        }

        #endregion
    }
}
