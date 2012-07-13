using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace First_Game
{
    class Camera
    {
        //Useless comment for git testing!
        private Vector2 position;
        private float speed;
        private Vector2 motion;

        /***Pointers***/
        private Game1 gamePtr;
        private Entity lockTarget;

        public float Speed
        {
            get { return speed; }
            set
            {
                speed = MathHelper.Clamp(value, 0.5f, 50f);
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position.X = MathHelper.Clamp(value.X, 0,
                        Map.MapWidthInPixels - Game1.ScreenWidth);
                position.Y = MathHelper.Clamp(value.Y, 0,
                        Map.MapHeightInPixels - Game1.ScreenHeight);
            }
        }

        /******************************/

        public Camera(Game1 parent)
        {
            gamePtr = parent;
            position = new Vector2();
            speed = 10.0f;
        }

        public void Update()
        {
            Track();
            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                Position += motion * Speed;
            }
        }

        public void lockEntity(Entity target)
        {
            lockTarget = target;
        }

        private void Track()
        {
            if(lockTarget.position.X >= (position.X + Game1.screenWidth - (Tiles.tileWidth*3)) )
            {
                ScrollRight();
            }
            else if (lockTarget.position.X <= (position.X + (Tiles.tileWidth * 2)))
            {
                ScrollLeft();
            }

            if (lockTarget.position.Y >= (position.Y + Game1.screenHeight - (Tiles.tileHeight * 3)))
            {
                ScrollDown();
            }
            else if (lockTarget.position.Y <= (position.Y + (Tiles.tileHeight * 2)))
            {
                ScrollUp();
            }
        }

        /***/

        public void ScrollRight()
        {
            motion.X = 1;
        }
        public void ScrollDown()
        {
            motion.Y = 1;
        }
        public void ScrollLeft()
        {
            motion.X = -1;
        }
        public void ScrollUp()
        {
            motion.Y = -1;
        }
        
        /***/

        public void resetMotion()
        {
            motion = Vector2.Zero;
        }
    }
}
