///////////////////////////////////////
#region Namespace Directives

using CompUhaul.Paths;
using System;
using System.Collections.Generic;
using System.IO;
using XRF_Data_Analysis_Utilities.Model;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Files
{
    public class DumpFileHandler
    {
        ////////////////////////////////////////
        #region Data Retrieval

        public static xrfSample GetSampleData(string _fullPath)
        {
            filePath dataFile = new filePath(_fullPath);

            if (!dataFile.Exists)
                throw new ArgumentException("Path does not exist.");

            string fileContent = ReadContentFromFile(dataFile.FullPath);
            xrfSample sample = ParseFileData(fileContent);
            return sample;
        }

        #endregion

        ////////////////////////////////////////
        #region Data Parsing


        private static xrfSample ParseFileData(string fileContent)
        {
            int columnLengthCutOff = 4;

            string[] linesInFile = SeparateFileDataByLine(fileContent);

            string[][] columnsData = SeparateLineDataByColumn(linesInFile);

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


        private static double[][] ConvertPixelDataToDouble(string[][] rawData, int startIndex)
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


        private static string[][] ExtractMetaData(string[][] rawData, int arrayLengthBoundary)
        {
            List<string[]> metaData = new List<string[]>();

            foreach (string[] item in rawData)
            {
                if (item.Length < arrayLengthBoundary)
                    metaData.Add(item);
            }

            return metaData.ToArray();
        }


        private static string[][] ExtractRawPixelData(string[][] rawData, int arrayLengthBoundary)
        {
            List<string[]> pixelData = new List<string[]>();

            foreach (string[] item in rawData)
            {
                if (item.Length > arrayLengthBoundary)
                    pixelData.Add(item);
            }

            return pixelData.ToArray();
        }


        private static string[] FilterDataCellContent(string[] dataColumns)
        {
            List<string> filteredSet = new List<string>();

            foreach (string component in dataColumns)
            {
                if (component != String.Empty)
                    filteredSet.Add(component);
            }

            return filteredSet.ToArray();
        }


        private static xrfSample GenerateSampleObject(string[] _labels, string[][] _metaData, double[][] _pixelData)
        {
            return new xrfSample(_labels, _metaData, _pixelData);
        }


        private static string ReadContentFromFile(string _filePath)
        {
            string _content = String.Empty;

            try
            {
                if (File.Exists(_filePath))
                {
                    using (StreamReader FileReadObject = new StreamReader(_filePath))
                    {
                        _content = FileReadObject.ReadToEnd();
                        FileReadObject.Close();
                    }
                }
            }
            catch
            {
                _content = String.Empty;
            }

            return _content;
        }


        private static string[] SeparateFileDataByLine(string fileData)
        {
            string[] linesInFile = fileData.Split('\n', '\r');

            List<string> filteredData = new List<string>();

            foreach (string item in linesInFile)
            {
                if (item != String.Empty && item != "0")
                    filteredData.Add(item);
            }

            return filteredData.ToArray();
        }


        private static string[][] SeparateLineDataByColumn(string[] linesInFile)
        {
            List<string[]> dataParsingVehicle = new List<string[]>();

            foreach (string item in linesInFile)
            {
                string[] set = item.Split(':', '\t');

                for (int i = 0; i < set.Length; i++)
                    set[i] = set[i].Trim();

                dataParsingVehicle.Add(FilterDataCellContent(set));
            }

            return dataParsingVehicle.ToArray();
        }

        #endregion
    }
}
