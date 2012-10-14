using WhenRobotsAttack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for PhysicsTest and is intended
    ///to contain all PhysicsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReadFromFileTest
    {


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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Physics Constructor
        ///</summary>
        [TestMethod()]
        public void readTest()
        {
            string filename = "C:\\Users\\for example John\\Documents\\Visual Studio 2010\\Projects\\WhenRobotsAttack\\WhenRobotsAttack\\WhenRobotsAttack\\Data\\fileReaderTest.txt";
            List<Tuple<String, String>> actual = WhenRobotsAttack.ReadFromFile.read(filename);
            List<Tuple<String, String>> expected = new List<Tuple<String,String>> 
                {   new Tuple<String, String>("levelnum","3"), 
                    new Tuple<String, String>("mapfilename","Map3Info.txt"), 
                    new Tuple<String, String>("heroes","\"Applejack_Pinkie Pie_Fluttershy\"") };

            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);

        }
    }
}
