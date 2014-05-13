///////////////////////////////////////
#region Namespace Directives

using System.Collections.Generic;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model
{
    public class xrfPixel
    {
        ////////////////////////////////////////
        #region Properties

        public double Deadtime
        {
            get;
            private set;
        }

        public int[] Counts
        {
            get;
            private set;
        }

        public string[] Elements
        {
            get;
            private set;
        }

        public int FullCounts
        {
            get;
            private set;
        }

        public string[] MotorNames
        {
            get;
            private set;
        }

        public double[][] MotorValues
        {
            get;
            private set;
        }

        public string Tag
        {
            get;
            private set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor


        public xrfPixel(double[] data, string[] labels,string tag)
        {
            ExtractElementCounts(new List<double>(data), new List<string>(labels));
            ExtractMotorData(new List<double>(data), new List<string>(labels));
            ParsePixelData(data, labels);
            Tag = tag;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void ExtractElementCounts(List<double> _data, List<string> _labels)
        {
            int startIndex = _labels.IndexOf("Deadtime (%)") + 1;
            int endIndex = _labels.IndexOf("Full Counts");
            int range = endIndex - startIndex;

            Elements = _labels.GetRange(startIndex, range).ToArray();
            Counts = new int[range];
            int i = 0;
            foreach (double item in _data.GetRange(startIndex, range))
            {
                Counts[i] = (int)item;
                i++;
            }
        }


        private void ExtractMotorData(List<double> _data, List<string> _labels)
        {
            int startIndex = 0;
            int endIndex = _labels.IndexOf("Deadtime (%)");
            int range = endIndex - startIndex;
            int numberOfMotors = range / 2;

            MotorNames = new string[numberOfMotors]; 
            MotorValues = new double[numberOfMotors][];

            for (int i = startIndex; i < numberOfMotors; i++)
            {
                MotorValues[i] = new double[2];

                int goalPosIndex = i;
                int actPosIndex = i + 2;
                int longLength = (_labels[goalPosIndex].Length > _labels[actPosIndex].Length) ? _labels[goalPosIndex].Length : _labels[actPosIndex].Length;

                for (int j = longLength; j >= 0; j--)
                {
                    if (_labels[goalPosIndex].Substring(0, j) == _labels[actPosIndex].Substring(0, j))
                    {
                        MotorNames[i] = _labels[goalPosIndex].Substring(0, j);
                        break;
                    }
                }

                MotorValues[i][0] = _data[goalPosIndex];
                MotorValues[i][1] = _data[actPosIndex];
            }
        }


        private void ParsePixelData(double[] _data, string[] _labels)
        {
            int length = _labels.Length;

            for (int i = 0; i < length; i++)
            {
                switch (_labels[i])
                {
                    case "Deadtime (%)":
                        Deadtime = _data[i];
                        break;

                    case "Full Counts":
                        FullCounts = (int)_data[i];
                        break;

                }
            }
        }

        #endregion
    }
}
