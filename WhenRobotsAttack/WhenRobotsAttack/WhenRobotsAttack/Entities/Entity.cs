using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace WhenRobotsAttack
{
    public class Entity : DrawableGameComponent
    {
        #region Class Variables

        // Debug
        protected bool DEBUG = false;

        // Game pointer; SpriteBatch pointer; Camera pointer
        protected GameManager game;
        protected SpriteBatch spriteBatch;
        protected Camera camera;


        // enums
        public enum Direction { LEFT, RIGHT, UP, DOWN }
        public enum State { STANDING, RUNNING, INAIR }
        public enum Status { ACTIVE, INACTIVE };
        

        // States
        protected Direction direction;
        protected State state;
        public Status status;

        // Gravity Switch
        public bool isGravityOn;

        // Debug Pixel
        protected Rectangle pxlrect;
        protected Texture2D whtpxl;

        // Physics stuff
        protected float xAcceleration;
        protected float yAcceleration;
        protected float friction;
        protected float airFriction;
        protected float gravity;
        protected float jumpforce;
        protected float maxSpeedX;
        protected float maxSpeedY;

        // Location and Area that the entity is inhabiting
        public Rectangle rect;
        public Vector2 position;

        // Collision point offsets
        public Vector2 topLeft, topRight, botLeft, botRight,
                  leftSideHigh, leftSideLow, rightSideHigh, rightSideLow;

        // Actual collision point coordinates
        public Vector2 TopLeft
        {
            get { return Vector2.Add(position, topLeft); }
        }
        public Vector2 TopRight
        {
            get { return Vector2.Add(position, topRight); }
        }
        public Vector2 BotLeft
        {
            get { return Vector2.Add(position, botLeft); }
        }
        public Vector2 BotRight
        {
            get { return Vector2.Add(position, botRight); }
        }
        public Vector2 LeftSideHigh
        {
            get { return Vector2.Add(position, leftSideHigh); }
        }
        public Vector2 LeftSideLow
        {
            get { return Vector2.Add(position, leftSideLow); }
        }
        public Vector2 RightSideHigh
        {
            get { return Vector2.Add(position, rightSideHigh); }
        }
        public Vector2 RightSideLow
        {
            get { return Vector2.Add(position, rightSideLow); }
        }


        // The change in position
        protected float deltaX, deltaY;

        // 2D movement in pixels
        public float DeltaX
        {
            get { return deltaX; }
            set
            {
                deltaX = MathHelper.Clamp(value, -maxSpeedX, maxSpeedX);
            }
        }
        public float DeltaY
        {
            get { return deltaY; }
            set
            {
                deltaY = MathHelper.Clamp(value, -maxSpeedY, maxSpeedY);
            }
        }

        #endregion

        // Default Constructor
        public Entity(GameManager game)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = game.spriteBatch;
            this.camera = game.camera;

            pxlrect = new Rectangle(0, 0, 3, 3);
            whtpxl = game.Content.Load<Texture2D>("whtpxl");

            state = State.STANDING;
            direction = Direction.RIGHT;
            status = Status.INACTIVE;

            loadPhysics();
            isGravityOn = true;
            deltaX = 0.0f;
            deltaY = 0.0f;
        }

        // Debug toggle
        public void toggleDebug()
        {
            DEBUG = !DEBUG;
        }

        public void loadPhysics()
        {
            xAcceleration = Physics.xAcceleration;
            yAcceleration = Physics.yAcceleration;
            friction = Physics.friction;
            airFriction = Physics.airFriction;
            gravity = Physics.gravity;
            jumpforce = Physics.jumpforce;
            maxSpeedX = Physics.maxSpeedX;
            maxSpeedY = Physics.maxSpeedY;
        }

        // Movement
        public void MoveLeft()
        {
            DeltaX -= xAcceleration;
            if (DeltaX < 0)
                direction = Direction.LEFT;
        }
        public void MoveRight()
        {
            DeltaX += xAcceleration;
            if (DeltaX > 0)
                direction = Direction.RIGHT;
        }
        public void MoveUp()
        {
            DeltaY -= yAcceleration;
        }
        public void MoveDown()
        {
            DeltaY += yAcceleration;
        }

        // Jumping and Landing
        public void Jump()
        {
            if (state != State.INAIR)
            {
                state = State.INAIR;
                DeltaY = jumpforce;
                position.Y += DeltaY;
            }
        }
        public void Land()
        {
            DeltaY = 0;
            state = (Math.Abs(DeltaX) == 0) ? State.STANDING : State.RUNNING;
        }

        // Places this entity at the coordinates of "location"
        public void place(Vector2 location)
        {
            position = location;
        }

        // Finds the center of this entity
        public Vector2 centerPoint()
        {
            float cX, cY;
            cX = position.X + (rect.Width / 2);
            cY = position.Y + (rect.Height / 2);

            return new Vector2(cX, cY);
        }

        public Point[] NearbyCells(Tile[,] map) // it would be best to break this up into smaller functions
        {
            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            List<Point> cells = new List<Point>();
            Point CELL_topleft = Map.VectorToCell(new Vector2(LeftSideHigh.X - tileMapWidth, TopLeft.Y - Map.tileHeight));
            Point CELL_botright = Map.VectorToCell(new Vector2(RightSideLow.X + tileMapWidth,
                                                         BotRight.Y + Map.tileHeight));
            for (int i = CELL_topleft.Y; i <= CELL_botright.Y; i++)
                for (int j = CELL_topleft.X; j <= CELL_botright.X; j++)
                    cells.Add(new Point(j, i));

            return cells.ToArray();
        }

        // Debug
        private void MarkTile(Point cell, Color tint)
        {
            game.spriteBatch.Begin();
            pxlrect.X = Map.tileWidth * cell.X + Map.tileWidth / 2 - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = Map.tileHeight * cell.Y + Map.tileHeight / 2 - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, tint);
            spriteBatch.End();
        }
        protected void drawTestedCells(Tile[,] map)
        {
            Point[] cells = NearbyCells(map);

            Point CELL_topleft = Map.VectorToCell(TopLeft);
            Point CELL_topright = Map.VectorToCell(TopRight);
            Point CELL_botleft = Map.VectorToCell(BotLeft);
            Point CELL_botright = Map.VectorToCell(BotRight);
            Point CELL_leftSideHigh = Map.VectorToCell(LeftSideHigh);
            Point CELL_leftSideLow = Map.VectorToCell(LeftSideLow);
            Point CELL_rightSideHigh = Map.VectorToCell(RightSideHigh);
            Point CELL_rightSideLow = Map.VectorToCell(RightSideLow);

            foreach (Point p in cells)
            {
                if (map[p.Y, p.X].value == 2 && (p.Equals(CELL_topleft) || p.Equals(CELL_topright) ||
                                           p.Equals(CELL_botleft) || p.Equals(CELL_botright) ||
                                           p.Equals(CELL_leftSideHigh) || p.Equals(CELL_leftSideLow) ||
                                           p.Equals(CELL_rightSideHigh) || p.Equals(CELL_rightSideLow)))
                    MarkTile(p, Color.Yellow);
                else
                    MarkTile(p, Color.Red);
            }
        }
        protected void drawCollisionPoints()
        {
            pxlrect.X = (int)TopLeft.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)TopLeft.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)TopRight.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)TopRight.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)BotLeft.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)BotLeft.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)BotRight.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)BotRight.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)LeftSideHigh.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)LeftSideHigh.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)LeftSideLow.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)LeftSideLow.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)RightSideHigh.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)RightSideHigh.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)RightSideLow.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)RightSideLow.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)position.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)position.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Green);
        }

        // Gets the current state
        public State getState()
        {
            return state;
        }


        // Base Game Functions
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position.X += DeltaX;
            position.Y += DeltaY;

            rect.X = (int)position.X - (int)camera.pubPosition.X;
            rect.Y = (int)position.Y - (int)camera.pubPosition.Y;

            if (isGravityOn) DeltaY += gravity;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (DEBUG)
            {
                drawTestedCells(game.currentLevel.map.layer[game.currentLevel.map.collisionLayer].tile);
                drawCollisionPoints();
            }
        }
    }
}
