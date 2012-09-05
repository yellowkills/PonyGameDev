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

namespace First_Game
{
    class Entity : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // [Private Vaiables]
        private const int _tileWidth = 32;
        private const int _tileHeight = 32;


        // [Protected Variables]
        public enum Direction { LEFT, RIGHT, UP, DOWN }
        public enum State { STANDING, RUNNING, INAIR }

        protected Direction direction;
        protected State state;

        protected Game game;
        protected SpriteBatch spriteBatch;

        // Collision point offsets
        protected Vector2 topLeft, topRight, botLeft, botRight,    
                          leftSideHigh, leftSideLow, rightSideHigh, rightSideLow;

        // Debug Pixel
        protected Rectangle pxlrect;
        protected Texture2D whtpxl;

        // The change in position
        protected float deltaX, deltaY;

        // Physics stuff
        protected float xAcceleration;
        protected float yAcceleration;
        protected float friction;
        protected float airFriction;
        protected float gravity;
        protected float jumpforce;
        protected float maxSpeedX;
        protected float maxSpeedY;

        
        // [Public Variables]
        public Camera camera;
        public Rectangle rect; //Needs a more descriptive name.
        public Vector2 position;

        // Actual collision point coordinates
        public Vector2 TopLeft
        {
            get { return Vector2.Add(position,topLeft); }
        }
        public Vector2 TopRight
        {
            get { return Vector2.Add(position,topRight); }
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




        // Default Constructor
        public Entity(Game game, SpriteBatch spriteBatch, Camera camera)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.camera = camera;

            pxlrect = new Rectangle(0, 0, 3, 3);
            whtpxl = game.Content.Load<Texture2D>("whtpxl");
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
            state = (Math.Abs(DeltaX) == 0)? State.STANDING : State.RUNNING;
        }

        // Places this entity at the coordinates of "location"
        public void place(Vector2 location)
        {
            position = location;
        }

        public Vector2 centerPoint()
        {
            float cX, cY;
            cX = position.X + (rect.Width/2);
            cY = position.Y + (rect.Height/2);

            return new Vector2(cX, cY);
        }

        // Collision Testing
        private Point VectorToCell(Vector2 vector)
        {
            // NOTE: once the map has its own class, then this method will be passed the map object instead of being hardcoded

            return new Point(
                        (int)(vector.X / _tileWidth),
                        (int)(vector.Y / _tileHeight));
        }
        protected Point[] NearbyCells(int[,] map) // it would be best to break this up into smaller functions
        {
            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            List<Point> cells = new List<Point>();
            Point CELL_topleft = VectorToCell(new Vector2(LeftSideHigh.X - tileMapWidth, TopLeft.Y - _tileHeight));
            Point CELL_botright = VectorToCell(new Vector2(RightSideLow.X + tileMapWidth,
                                                         BotRight.Y + _tileHeight));
            for (int i = CELL_topleft.Y; i <= CELL_botright.Y; i++)
                for (int j = CELL_topleft.X; j <= CELL_botright.X; j++)
                    cells.Add(new Point(j, i));

            return cells.ToArray();
        }
        public void CollisionTest(int[,] map) // it would be best to break this up into smaller functions
        {
            Point[] cells = NearbyCells(map);

            Point CELL_topleft = VectorToCell(TopLeft);
            Point CELL_topright = VectorToCell(TopRight);
            Point CELL_botleft = VectorToCell(BotLeft);
            Point CELL_botright = VectorToCell(BotRight);
            Point CELL_leftSideHigh = VectorToCell(LeftSideHigh);
            Point CELL_leftSideLow = VectorToCell(LeftSideLow);
            Point CELL_rightSideHigh = VectorToCell(RightSideHigh);
            Point CELL_rightSideLow = VectorToCell(RightSideLow);



            foreach (Point p in cells)
            {
                if (map[p.Y, p.X] == 1)
                {
                    if (p.Equals(CELL_leftSideHigh) || p.Equals(CELL_leftSideLow))
                    {
                        DeltaX = 0;
                        position.X = (p.X + 1) * _tileWidth - leftSideHigh.X;
                    }
                    if (p.Equals(CELL_rightSideHigh) || p.Equals(CELL_rightSideLow))
                    {
                        DeltaX = 0;
                        position.X = (p.X * _tileWidth) - rightSideHigh.X;
                    }
                    if (DeltaY > 0 && (p.Equals(CELL_botleft) || p.Equals(CELL_botright)))
                    {
                        Land();
                        position.Y = p.Y * _tileHeight - rect.Height;
                    }
                    else if (p.Equals(CELL_topleft) || p.Equals(CELL_topright))
                    {
                        DeltaY = 0;
                        position.Y = ((p.Y + 1) * _tileHeight) - topLeft.Y;
                    }
                }
            }
        }

        // Debug
        private void MarkTile(Point cell, Color tint)
        {
            spriteBatch.Begin();
            pxlrect.X = Tiles.tileWidth * cell.X + Tiles.tileWidth / 2 - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = Tiles.tileHeight * cell.Y + Tiles.tileHeight / 2 - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, tint);
            spriteBatch.End();
        }
        protected void drawTestedCells(int[,] map)
        {
            Point[] cells = NearbyCells(map);

            Point CELL_topleft = VectorToCell(TopLeft);
            Point CELL_topright = VectorToCell(TopRight);
            Point CELL_botleft = VectorToCell(BotLeft);
            Point CELL_botright = VectorToCell(BotRight);
            Point CELL_leftSideHigh = VectorToCell(LeftSideHigh);
            Point CELL_leftSideLow = VectorToCell(LeftSideLow);
            Point CELL_rightSideHigh = VectorToCell(RightSideHigh);
            Point CELL_rightSideLow = VectorToCell(RightSideLow);

            foreach (Point p in cells)
            {
                if (map[p.Y, p.X] == 1 && (p.Equals(CELL_topleft) || p.Equals(CELL_topright) ||
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
            spriteBatch.Begin();

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

            spriteBatch.End();
        }

        // Updates the position
        protected void RefreshPosition()
        {
            position.X += DeltaX;
            position.Y += DeltaY;
        }

        public State getState()
        {
            return state;
        }


        // Base Game Functions
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(GameTime gameTime)
        {
        }
    }
}
