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
            Motors = ExtractMotorData(metaData);
            ParseRemainingMetaData(metaData);
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
                    convertedData[i][j] = new xrfPixel(rawData[i][j], labels, Guid.NewGuid().ToString());
                }
            }

            return convertedData;
        }


        private motorGroup ExtractMotorData(string[][] _data)
        {
            motorGroup motors = new motorGroup();

            string _pattern;
            double _delayAfterMovement, _stayAtEnd;

            _pattern = _data[0][0];

            motors.Devices.Add(GetDeviceData(_data, 2));
            motors.Devices.Add(GetDeviceData(_data, 6));

            Double.TryParse(_data[10][1], out _delayAfterMovement);
            Double.TryParse(_data[11][1], out _stayAtEnd);

            motors.DelayAfterMovement = _delayAfterMovement;
            motors.Pattern = _pattern;
            motors.StayAtEnd = _stayAtEnd;

            return motors;
        }


        private motorSettings GetDeviceData(string[][] _data, int _startIndex)
        {
            string _name;
            double _increment, _start, _stop;

            _name = _data[_startIndex][1];
            Double.TryParse(_data[_startIndex + 1][1], out _start);
            Double.TryParse(_data[_startIndex + 2][1], out _stop);
            Double.TryParse(_data[_startIndex + 3][1], out _increment);

            return new motorSettings(_increment, _name, _start, _stop);
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


        private void ParseRemainingMetaData(string[][] data)
        {
            foreach (string[] item in data)
            {
                switch (item[0])
                {
                    case "Instrument":
                        this.Instrument = item[1];
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
