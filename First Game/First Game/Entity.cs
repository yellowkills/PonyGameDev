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

        // Collision points
        protected Vector2 topleft, topright,            // For simplicity, I'm just making a rectangle for the collision points.
                          botleft, botright,            // Later on, we can make this more accurate. Also, it may be more efficient
                          midleftHIGH, midleftLOW,      // for these to be offsets of 'position' rather than the true coordinates
                          midrightHIGH, midrightLOW;    // of the collision points.

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
            Point topleft = VectorToCell(new Vector2(this.midleftHIGH.X - tileMapWidth, this.topleft.Y - _tileHeight));
            Point bottomright = VectorToCell(new Vector2(this.midrightLOW.X + tileMapWidth,
                                                         this.botright.Y + _tileHeight));
            for (int i = topleft.Y; i <= bottomright.Y; i++)
                for (int j = topleft.X; j <= bottomright.X; j++)
                    cells.Add(new Point(j, i));

            return cells.ToArray();
        }
        public void CollisionTest(int[,] map) // it would be best to break this up into smaller functions
        {
            Point[] cells = NearbyCells(map);

            Point topleft = VectorToCell(this.topleft);
            Point topright = VectorToCell(this.topright);
            Point midleftHIGH = VectorToCell(this.midleftHIGH);
            Point midleftLOW = VectorToCell(this.midleftLOW);
            Point midrightHIGH = VectorToCell(this.midrightHIGH);
            Point midrightLOW = VectorToCell(this.midrightLOW);
            Point botleft = VectorToCell(this.botleft);
            Point botright = VectorToCell(this.botright);


            foreach (Point p in cells)
            {
                if (map[p.Y, p.X] == 1)
                {
                    if (p.Equals(midleftHIGH) || p.Equals(midleftLOW))
                    {
                        DeltaX = 0;
                        position.X = ((p.X + 1) * _tileWidth) - Math.Abs((position.X - this.midleftHIGH.X));
                    }
                    if (p.Equals(midrightHIGH) || p.Equals(midrightLOW))
                    {
                        DeltaX = 0;
                        position.X = (p.X * _tileWidth - 1) - Math.Abs((position.X - this.midrightHIGH.X));
                    }
                    if (DeltaY > 0 && (p.Equals(botleft) || p.Equals(botright)))
                    {
                        Land();
                        //position.Y = ((p.Y-1) * _tileHeight) - (rect.Height - Math.Abs((position.Y - this.topleft.Y)));
                        position.Y = p.Y * _tileHeight - rect.Height;
                    }
                    else if (p.Equals(topleft) || p.Equals(topright))
                    {
                        DeltaY = 0;
                        position.Y = ((p.Y + 1) * _tileHeight) - Math.Abs((position.Y - this.topleft.Y));
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

            Point topleft = VectorToCell(this.topleft);
            Point topright = VectorToCell(this.topright);
            Point midleftHIGH = VectorToCell(this.midleftHIGH);
            Point midleftLOW = VectorToCell(this.midleftLOW);
            Point midrightHIGH = VectorToCell(this.midrightHIGH);
            Point midrightLOW = VectorToCell(this.midrightLOW);
            Point botleft = VectorToCell(this.botleft);
            Point botright = VectorToCell(this.botright);

            foreach (Point p in cells)
            {
                if (map[p.Y, p.X] == 1 && (p.Equals(botleft) || p.Equals(botright) ||
                                           p.Equals(midleftHIGH) || p.Equals(midleftLOW) ||
                                           p.Equals(midrightHIGH) || p.Equals(midrightLOW) ||
                                           p.Equals(topleft) || p.Equals(topright)))
                    MarkTile(p, Color.Yellow);
                else
                    MarkTile(p, Color.Red);
            }
        }
        protected void drawCollisionPoints()
        {
            spriteBatch.Begin();

            pxlrect.X = (int)topleft.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)topleft.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)topright.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)topright.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)botleft.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)botleft.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)botright.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)botright.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)midleftHIGH.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)midleftHIGH.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)midleftLOW.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)midleftLOW.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)midrightHIGH.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)midrightHIGH.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            pxlrect.X = (int)midrightLOW.X - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = (int)midrightLOW.Y - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, Color.Lime);

            spriteBatch.End();
        }

        // Updates the position and re-calculates all the collision points. [inefficient? maybe]
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
