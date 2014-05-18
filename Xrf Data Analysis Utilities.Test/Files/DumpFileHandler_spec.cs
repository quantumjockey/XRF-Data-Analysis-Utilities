///////////////////////////////////////
#region Namespace Directives

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XRF_Data_Analysis_Utilities.Files;
using XRF_Data_Analysis_Utilities.Model;

#endregion
///////////////////////////////////////

namespace Xrf_Data_Analysis_Utilities.Test.Files
{
    /// <summary>
    /// Summary description for DumpFileHandlerSpec
    /// </summary>
    [TestClass]
    public class DumpFileHandler_spec
    {
        ////////////////////////////////////////
        #region Constants

        const string testFilePath = @"D:\UNLV HiPSEC\Data Files\XRF\XRFscan8_015_2D - Copy.txt";
        const string dummyPath = @"D:\";

        #endregion

        ////////////////////////////////////////
        #region Constructor (Auto-generated)

        public DumpFileHandler_spec()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        ////////////////////////////////////////
        #region TestContext Components (Auto-Generated)

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Unit Tests (Methods)

        // static xrfSample GetSampleData(string _fullPath)

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSampleData_PathNullOrEmpty_InvalidArgumentException()
        {
            DumpFileHandler.GetSampleData("");
            DumpFileHandler.GetSampleData(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSampleData_PathIsWhiteSpace_InvalidArgumentException()
        {
            DumpFileHandler.GetSampleData("  ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSampleData_PathInvalid_InvalidArgumentException()
        {
            DumpFileHandler.GetSampleData(dummyPath);
        }


        // will create additional specifications for determining file integrity once a consistent set of formats has been thoroughly examined

        #endregion

        ////////////////////////////////////////
        #region Child Classes (Used in Testing)



        #endregion
    }
}
