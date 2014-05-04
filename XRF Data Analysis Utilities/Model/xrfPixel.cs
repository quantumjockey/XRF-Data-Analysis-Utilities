///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.Model
{
    public class xrfPixel
    {
        ////////////////////////////////////////
        #region Properties

        // motor names list
        // - goal
        // - actual

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

        #endregion

        ////////////////////////////////////////
        #region Constructor


        public xrfPixel(double[] data, string[] labels)
        {
            ExtractElementCounts(new List<double>(data), new List<string>(labels));
            ParsePixelData(data, labels);
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
