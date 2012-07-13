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
        // Private Vaiables
        private const int _tileWidth = 32;
        private const int _tileHeight = 32;

        // Protected Variables
        protected enum Direction { LEFT, RIGHT, UP, DOWN }
        protected enum State { STANDING, RUNNING, INAIR }

        protected Direction direction;
        protected State state;

        protected Game game;
        protected SpriteBatch spriteBatch;

        protected Rectangle rect;
        protected Vector2 top, botleft, botright, midleftHIGH, midleftLOW, midrightHIGH, midrightLOW;

        protected float deltaX, deltaY;

        protected float xAcceleration;
        protected float yAcceleration;
        protected float xDeceleration;
        protected float xDeceleration2;
        protected float gravity;
        protected float jumpforce;
        protected float maxSpeedX;
        protected float maxSpeedY;

        // Public Variables
        public Camera camera;
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
        public Entity(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
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
            state = State.RUNNING;
            DeltaY = 0;
        }


        // Collision Testing
        private Point VectorToCell(Vector2 vector)
        {
            // NOTE: once the map has its own class, then this method will be passed the map object instead of being hardcoded

            return new Point(
                        (int)(vector.X / _tileWidth),
                        (int)(vector.Y / _tileHeight));
        }
        protected Point[] NearbyCells(int[,] map)
        {
            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            List<Point> cells = new List<Point>();
            Point topleft = VectorToCell(new Vector2(position.X - tileMapWidth, position.Y - _tileHeight));
            Point bottomright = VectorToCell(new Vector2(position.X + rect.Width + tileMapWidth,
                                                         position.Y + rect.Height + _tileHeight));
            for (int i = topleft.Y; i <= bottomright.Y; i++)
                for (int j = topleft.X; j <= bottomright.X; j++)
                    cells.Add(new Point(j, i));

            return cells.ToArray();
        }
        public void CollisionTest(int[,] map)
        {
            Point[] cells = NearbyCells(map);

            Point top = VectorToCell(this.top);
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
                        position.X = (p.X + 1) * _tileWidth;
                    }
                    if (p.Equals(midrightHIGH) || p.Equals(midrightLOW))
                    {
                        DeltaX = 0;
                        position.X = (p.X - 1) * _tileWidth;
                    }
                    if (DeltaY > 0 && (p.Equals(botleft) || p.Equals(botright)))
                    {
                        Land();
                        position.Y = (p.Y - 2) * _tileHeight;
                    }
                    else if (p.Equals(top))
                    {
                        DeltaY = 0;
                        position.Y = (p.Y + 1) * _tileHeight;
                    }


                }
            }
        }


        // Game Stuff
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(GameTime gameTime)
        {
        }
    }
}
