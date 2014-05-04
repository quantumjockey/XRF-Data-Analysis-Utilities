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

        public xrfSample(string[] pixelLabels, string[][] metaData, double[][] pixelData)
        {
            Motors = ExtractMotorData(metaData, pixelLabels);
            ParseMetaData(metaData);
            this.Labels = pixelLabels;
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

        private motorGroup ExtractMotorData(string[][] _data, string[] _labels)
        {
            motorGroup motors = new motorGroup();

            int startIndex = 0;
            int endIndex = (new List<string>(_labels)).IndexOf("Deadtime (%)");
            int range = endIndex - startIndex;
            int numberOfMotors = range / 2;

            //for (int i = startIndex; i < numberOfMotors; i++)
            //{
            //    MotorValues[i] = new double[2];

            //    int goalPosIndex = i;
            //    int actPosIndex = i + 2;
            //    int longLength = (_labels[goalPosIndex].Length > _labels[actPosIndex].Length) ? _labels[goalPosIndex].Length : _labels[actPosIndex].Length;

            //    for (int j = longLength; j >= 0; j--)
            //    {
            //        if (_labels[goalPosIndex].Substring(0, j) == _labels[actPosIndex].Substring(0, j))
            //        {
            //            MotorNames[i] = _labels[goalPosIndex].Substring(0, j);
            //            break;
            //        }
            //    }

            //    MotorValues[i][0] = _data[goalPosIndex];
            //    MotorValues[i][1] = _data[actPosIndex];
            //}

            return motors;
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
