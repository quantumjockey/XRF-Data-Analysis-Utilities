///////////////////////////////////////
#region Namespace Directives

using CompUhaul.Files.Handlers;
using System;
using System.Collections.Generic;
using XRF_Data_Analysis_Utilities.Model;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Files
{
    public class DumpFileHandler : TextFileHandler
    {
        ////////////////////////////////////////
        #region Constructor

        public DumpFileHandler(string _fullPath) : base(_fullPath) { }

        #endregion

        ////////////////////////////////////////
        #region Data Retrieval

        public xrfSample GetSampleData()
        {
            if (!_dataFile.Exists)
                throw new ArgumentException("Path does not exist.");

            xrfSample sample = ParseFileData(ReadTabularContentFromFile());
            return sample;
        }

        #endregion

        ////////////////////////////////////////
        #region Data Parsing


        private xrfSample ParseFileData(string[][] columnsData)
        {
            int columnLengthCutOff = 4;

            string[][] metaData = ExtractMetaData(columnsData, columnLengthCutOff);

            string[][] rawPixelData = ExtractRawPixelData(columnsData, columnLengthCutOff);

            double[][] convertedPixelData = ConvertPixelDataToDouble(rawPixelData, 1);

            string[] columnLabels = rawPixelData[0];

            xrfSample sample = GenerateSampleObject(columnLabels, metaData, convertedPixelData);

            return sample;
        }

        #endregion

        ////////////////////////////////////////
        #region Private Methods


        private double[][] ConvertPixelDataToDouble(string[][] rawData, int startIndex)
        {
            List<double[]> convertedData = new List<double[]>();

            for (int i = startIndex; i < rawData.Length; i++)
            {
                List<double> rowData = new List<double>();

                foreach (string item in rawData[i])
                {
                    double result = 0.0;
                    Double.TryParse(item, out result);
                    rowData.Add(result);
                }

                convertedData.Add(rowData.ToArray());
            }

            return convertedData.ToArray();
        }


        private string[][] ExtractMetaData(string[][] rawData, int arrayLengthBoundary)
        {
            List<string[]> metaData = new List<string[]>();

            foreach (string[] item in rawData)
                if (item.Length < arrayLengthBoundary)
                    metaData.Add(item);

            return metaData.ToArray();
        }


        private string[][] ExtractRawPixelData(string[][] rawData, int arrayLengthBoundary)
        {
            List<string[]> pixelData = new List<string[]>();

            foreach (string[] item in rawData)
                if (item.Length > arrayLengthBoundary)
                    pixelData.Add(item);

            return pixelData.ToArray();
        }


        private xrfSample GenerateSampleObject(string[] _labels, string[][] _metaData, double[][] _pixelData)
        {
            return new xrfSample(_labels, _metaData, _pixelData);
        }

        #endregion
    }
}
