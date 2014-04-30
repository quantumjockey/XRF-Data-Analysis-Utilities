///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;

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

        public double[][][] SortedPixelData { get; set; }

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
            SortedPixelData = SortPixelData(pixelData);
            GetBeamHeightAndWidth();
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        private void GetBeamHeightAndWidth()
        {
            BeamHeight = SortedPixelData.Length;
            BeamWidth = SortedPixelData[0].Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public elementData GetElementData(string elementName)
        {
            int elementIndex = (new List<string>(Labels)).IndexOf(elementName);

            elementData vehicle = new elementData(BeamHeight, BeamWidth, elementIndex, elementName, SortedPixelData);

            return vehicle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
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

        #endregion
    }
}
