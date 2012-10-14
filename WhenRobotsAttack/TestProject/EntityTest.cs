using WhenRobotsAttack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for EntityTest and is intended
    ///to contain all EntityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EntityTest
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
        ///A test for Jump
        ///</summary>
        [TestMethod()]
        public void JumpTest()
        {
            GameManager game = new GameManager();
            Entity target = new Entity(game);
            
            target.Jump();
            Assert.AreEqual(target.getState(), Entity.State.INAIR);
        }

        /// <summary>
        ///A test for Land
        ///</summary>
        [TestMethod()]
        public void LandTest()
        {
            GameManager game = new GameManager();
            Entity target = new Entity(game); 

            target.Land();
            Assert.AreEqual(target.getState(), Entity.State.STANDING);
        }

        /// <summary>
        ///A test for MoveDown
        ///</summary>
        [TestMethod()]
        public void MoveDownTest()
        {
            GameManager game = new GameManager();
            Entity target = new Entity(game); 

            float before = target.DeltaY;
            target.MoveDown();
            Assert.AreEqual(target.DeltaY, before+Physics.yAcceleration);
        }

        /// <summary>
        ///A test for MoveLeft
        ///</summary>
        [TestMethod()]
        public void MoveLeftTest()
        {
            GameManager game = new GameManager();
            Entity target = new Entity(game); 

            float before = target.DeltaX;
            target.MoveLeft();
            Assert.AreEqual(target.DeltaX, before - Physics.xAcceleration);
        }

        /// <summary>
        ///A test for MoveRight
        ///</summary>
        [TestMethod()]
        public void MoveRightTest()
        {
            GameManager game = new GameManager();
            Entity target = new Entity(game); 

            float before = target.DeltaX;
            target.MoveRight();
            Assert.AreEqual(target.DeltaX, before + Physics.xAcceleration);
        }

        /// <summary>
        ///A test for MoveUp
        ///</summary>
        [TestMethod()]
        public void MoveUpTest()
        {
            GameManager game = new GameManager();
            Entity target = new Entity(game); 

            float before = target.DeltaY;
            target.MoveUp();
            Assert.AreEqual(target.DeltaY, before - Physics.yAcceleration);
        }

        /// <summary>
        ///A test for centerPoint
        ///</summary>
        [TestMethod()]
        public void centerPointTest()
        {
            GameManager game = new GameManager();
            Entity target = new Entity(game); 

            Vector2 expected = new Vector2(target.position.X + (target.rect.Width / 2), target.position.Y + (target.rect.Height / 2));
            Vector2 actual;
            actual = target.centerPoint();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for getState
        ///</summary>
        [TestMethod()]
        public void getStateTest()
        {
            GameManager game = new GameManager();
            Entity target = new Entity(game); 

            Entity.State expected = Entity.State.STANDING;
            Entity.State actual;
            actual = target.getState();
            Assert.AreEqual(expected, actual);
            expected = Entity.State.INAIR;
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for place
        ///</summary>
        [TestMethod()]
        public void placeTest()
        {
            GameManager game = new GameManager();
            Entity target = new Entity(game); 

            Vector2 location = new Vector2();
            target.place(location);
            Assert.AreEqual(target.position,location);
        }
    }
}
