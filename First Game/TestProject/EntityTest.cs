using First_Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        ///A test for TopRight
        ///</summary>
        [TestMethod()]
        public void TopRightTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = target.TopRight;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TopLeft
        ///</summary>
        [TestMethod()]
        public void TopLeftTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = target.TopLeft;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RightSideLow
        ///</summary>
        [TestMethod()]
        public void RightSideLowTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = target.RightSideLow;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RightSideHigh
        ///</summary>
        [TestMethod()]
        public void RightSideHighTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = target.RightSideHigh;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LeftSideLow
        ///</summary>
        [TestMethod()]
        public void LeftSideLowTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = target.LeftSideLow;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LeftSideHigh
        ///</summary>
        [TestMethod()]
        public void LeftSideHighTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = target.LeftSideHigh;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeltaY
        ///</summary>
        [TestMethod()]
        public void DeltaYTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            float expected = 0F; // TODO: Initialize to an appropriate value
            float actual;
            target.DeltaY = expected;
            actual = target.DeltaY;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeltaX
        ///</summary>
        [TestMethod()]
        public void DeltaXTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            float expected = 0F; // TODO: Initialize to an appropriate value
            float actual;
            target.DeltaX = expected;
            actual = target.DeltaX;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BotRight
        ///</summary>
        [TestMethod()]
        public void BotRightTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = target.BotRight;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BotLeft
        ///</summary>
        [TestMethod()]
        public void BotLeftTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = target.BotLeft;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for place
        ///</summary>
        [TestMethod()]
        public void placeTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 location = new Vector2(); // TODO: Initialize to an appropriate value
            target.place(location);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for getState
        ///</summary>
        [TestMethod()]
        public void getStateTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Entity.State expected = new Entity.State(); // TODO: Initialize to an appropriate value
            Entity.State actual;
            actual = target.getState();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for drawTestedCells
        ///</summary>
        [TestMethod()]
        [DeploymentItem("First Game.exe")]
        public void drawTestedCellsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Entity_Accessor target = new Entity_Accessor(param0); // TODO: Initialize to an appropriate value
            int[,] map = null; // TODO: Initialize to an appropriate value
            target.drawTestedCells(map);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for drawCollisionPoints
        ///</summary>
        [TestMethod()]
        [DeploymentItem("First Game.exe")]
        public void drawCollisionPointsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Entity_Accessor target = new Entity_Accessor(param0); // TODO: Initialize to an appropriate value
            target.drawCollisionPoints();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for centerPoint
        ///</summary>
        [TestMethod()]
        public void centerPointTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            Vector2 expected = new Vector2(); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = target.centerPoint();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for VectorToCell
        ///</summary>
        [TestMethod()]
        [DeploymentItem("First Game.exe")]
        public void VectorToCellTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Entity_Accessor target = new Entity_Accessor(param0); // TODO: Initialize to an appropriate value
            Vector2 vector = new Vector2(); // TODO: Initialize to an appropriate value
            Point expected = new Point(); // TODO: Initialize to an appropriate value
            Point actual;
            actual = target.VectorToCell(vector);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            GameTime gameTime = null; // TODO: Initialize to an appropriate value
            target.Update(gameTime);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RefreshPosition
        ///</summary>
        [TestMethod()]
        [DeploymentItem("First Game.exe")]
        public void RefreshPositionTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Entity_Accessor target = new Entity_Accessor(param0); // TODO: Initialize to an appropriate value
            target.RefreshPosition();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for NearbyCells
        ///</summary>
        [TestMethod()]
        [DeploymentItem("First Game.exe")]
        public void NearbyCellsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Entity_Accessor target = new Entity_Accessor(param0); // TODO: Initialize to an appropriate value
            int[,] map = null; // TODO: Initialize to an appropriate value
            Point[] expected = null; // TODO: Initialize to an appropriate value
            Point[] actual;
            actual = target.NearbyCells(map);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MoveUp
        ///</summary>
        [TestMethod()]
        public void MoveUpTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            target.MoveUp();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for MoveRight
        ///</summary>
        [TestMethod()]
        public void MoveRightTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            target.MoveRight();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for MoveLeft
        ///</summary>
        [TestMethod()]
        public void MoveLeftTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            target.MoveLeft();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for MoveDown
        ///</summary>
        [TestMethod()]
        public void MoveDownTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            target.MoveDown();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for MarkTile
        ///</summary>
        [TestMethod()]
        [DeploymentItem("First Game.exe")]
        public void MarkTileTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Entity_Accessor target = new Entity_Accessor(param0); // TODO: Initialize to an appropriate value
            Point cell = new Point(); // TODO: Initialize to an appropriate value
            Color tint = new Color(); // TODO: Initialize to an appropriate value
            target.MarkTile(cell, tint);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Land
        ///</summary>
        [TestMethod()]
        public void LandTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            target.Land();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Jump
        ///</summary>
        [TestMethod()]
        public void JumpTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            target.Jump();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Draw
        ///</summary>
        [TestMethod()]
        public void DrawTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            GameTime gameTime = null; // TODO: Initialize to an appropriate value
            target.Draw(gameTime);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CollisionTest
        ///</summary>
        [TestMethod()]
        public void CollisionTestTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera); // TODO: Initialize to an appropriate value
            int[,] map = null; // TODO: Initialize to an appropriate value
            target.CollisionTest(map);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Entity Constructor
        ///</summary>
        [TestMethod()]
        public void EntityConstructorTest()
        {
            Game game = null; // TODO: Initialize to an appropriate value
            SpriteBatch spriteBatch = null; // TODO: Initialize to an appropriate value
            Camera camera = null; // TODO: Initialize to an appropriate value
            Entity target = new Entity(game, spriteBatch, camera);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
