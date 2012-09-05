using First_Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for CameraTest and is intended
    ///to contain all CameraTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CameraTest
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
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            GameManager parent = null; // TODO: Initialize to an appropriate value
            Camera target = new Camera(parent); // TODO: Initialize to an appropriate value
            target.Update();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ScrollDown
        ///</summary>
        [TestMethod()]
        public void ScrollDownTest()
        {
            GameManager parent = null; // TODO: Initialize to an appropriate value
            Camera target = new Camera(parent); // TODO: Initialize to an appropriate value
            target.ScrollDown();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Track
        ///</summary>
        [TestMethod()]
        [DeploymentItem("First Game.exe")]
        public void TrackTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Initialize to an appropriate value
            target.Track();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for gotoTarget
        ///</summary>
        [TestMethod()]
        [DeploymentItem("First Game.exe")]
        public void gotoTargetTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Camera_Accessor target = new Camera_Accessor(param0); // TODO: Initialize to an appropriate value
            target.gotoTarget();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
