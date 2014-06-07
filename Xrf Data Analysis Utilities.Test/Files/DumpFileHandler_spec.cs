///////////////////////////////////////
#region Namespace Directives

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XRF_Data_Analysis_Utilities.Files;

#endregion
///////////////////////////////////////

namespace Xrf_Data_Analysis_Utilities.Test.Files
{
    [TestClass]
    public class DumpFileHandler_spec
    {
        ////////////////////////////////////////
        #region Constants

        const string testFilePath = @"D:\UNLV HiPSEC\Data Files\XRF\XRFscan8_015_2D - Copy.txt";
        const string dummyPath = @"D:\";

        #endregion

        ////////////////////////////////////////
        #region Unit Tests (Methods)

        // static xrfSample GetSampleData(string _fullPath)

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSampleData_PathNullOrEmpty_InvalidArgumentException()
        {
            DumpFileHandler handler;
            handler = new DumpFileHandler("");
            handler.GetSampleData();
            handler = new DumpFileHandler(null);
            handler.GetSampleData();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSampleData_PathIsWhiteSpace_InvalidArgumentException()
        {
            DumpFileHandler handler = new DumpFileHandler("  ");
            handler.GetSampleData();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSampleData_PathInvalid_InvalidArgumentException()
        {
            DumpFileHandler handler = new DumpFileHandler(dummyPath);
            handler.GetSampleData();
        }


        // will create additional specifications for determining file integrity once a consistent set of formats has been thoroughly examined

        #endregion
    }
}
