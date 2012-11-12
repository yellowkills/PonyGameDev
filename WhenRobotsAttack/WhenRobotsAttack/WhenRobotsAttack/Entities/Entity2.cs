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
    public class Entity2 : DrawableGameComponent
    {
        #region Class Variables

        // Debug
        protected bool DEBUG = false;

        // Game pointer; SpriteBatch pointer; Camera pointer
        protected GameManager game;
        protected SpriteBatch spriteBatch;
        protected Camera camera;

        // Axis Aligned Bounding Box
        public Rectangle AABB;
        public Vector2 sensorA, sensorB;
        public Vector2 animationOffset;

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

        // Actual collision point coordinates
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
        public Entity2(GameManager game)
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

            animationOffset = new Vector2(-33,-12);

            setAABB();

            sensorA = new Vector2(AABB.Left + 1, AABB.Bottom);
            sensorB = new Vector2(AABB.Right - 1, AABB.Bottom);
        }

        // Debug toggle
        public void toggleDebug()
        {
            DEBUG = !DEBUG;
        }

        // hardcoded for testing
        private void setAABB()
        {
            AABB = new Rectangle(0,0,30,60);
            AABB.X = (int) position.X + 33;
            AABB.Y = (int) position.Y + 12;
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
            DeltaX -= xAcceleration * game.ElapsedSeconds;
            if (DeltaX < 0)
                direction = Direction.LEFT;
        }
        public void MoveRight()
        {
            DeltaX += xAcceleration * game.ElapsedSeconds;
            if (DeltaX > 0)
                direction = Direction.RIGHT;
        }
        public void MoveUp()
        {
            DeltaY -= yAcceleration * game.ElapsedSeconds;
        }
        public void MoveDown()
        {
            DeltaY += yAcceleration * game.ElapsedSeconds;
        }

        // Jumping and Landing
        public void Jump()
        {
            if (state != State.INAIR)
            {
                state = State.INAIR;
                DeltaY = jumpforce;
                position.Y += DeltaY * game.ElapsedSeconds;
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

        public Point[] NearbyCellsAABB(Tile[,] map)
        {
            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            List<Point> cells = new List<Point>();
            Point CELL_topleft = Map.VectorToCell(new Vector2(AABB.X - tileMapWidth, AABB.Y - Map.tileHeight));
            Point CELL_botright = Map.VectorToCell(new Vector2(AABB.X + AABB.Width + tileMapWidth,
                                                         AABB.Y + AABB.Height + Map.tileHeight));
            for (int i = CELL_topleft.Y; i <= CELL_botright.Y; i++)
                for (int j = CELL_topleft.X; j <= CELL_botright.X; j++)
                    cells.Add(new Point(j, i));

            return cells.ToArray();
        }

        // Debug
        private void MarkTile(Point cell, Color tint)
        {
            pxlrect.X = Map.tileWidth * cell.X + Map.tileWidth / 2 - 1 - (int)camera.pubPosition.X;
            pxlrect.Y = Map.tileHeight * cell.Y + Map.tileHeight / 2 - 1 - (int)camera.pubPosition.Y;
            spriteBatch.Draw(whtpxl, pxlrect, tint);
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

            if (isGravityOn) DeltaY += gravity*game.ElapsedSeconds;
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
