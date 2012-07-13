using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace First_Game
{
    class Camera
    {
        private Vector2 position;
        private float speed;
        private Vector2 motion;
        private Vector2 cameraCenter;
        private Vector2 targetCenter;
        private bool moveCam;

        /***Pointers***/
        private Game1 gamePtr;

        private bool hasTarget;
        private Entity lockTarget;

        public float Speed
        {
            get { return speed; }
            set
            {
                speed = MathHelper.Clamp(value, 0.5f, 50f);
            }
        }

        public Vector2 pubPosition
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

        public Vector2 TargetPosition
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
            pubPosition = new Vector2();
            cameraCenter = new Vector2();
            speed = 10.0f;
            hasTarget = false;
            moveCam = false;
        }

        public void Update()
        {
            updateCenter();
            Track();
            if(moveCam)
                gotoTarget();
            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                pubPosition += motion * Speed;
            }
        }

        public void lockEntity(Entity target)
        {
            lockTarget = target;
            hasTarget = true;
        }

        public void releaseLock()
        {
            hasTarget = false;
        }

        private void Track()
        {
            if (hasTarget)
            {
                if (lockTarget.position.X >= (Game1.ScreenWidth / 3) * 2 ||
                    lockTarget.position.X <= Game1.screenWidth / 3)
                {
                    targetCenter.X = lockTarget.position.X;
                    moveCam = true;
                }

                /* Platform Lock on Y axis */
                if(lockTarget.position.Y > (Game1.screenHeight/3) *2 ||
                    lockTarget.position.Y < (Game1.ScreenHeight/3) )
                {
                    targetCenter.Y = lockTarget.position.Y; //Target the player's current Y as the center for the camera.
                    moveCam = true;
                }
            }
        }

        private void gotoTarget()
        {
            bool moved = false;
            if(targetCenter.X > cameraCenter.X + Tiles.tileWidth)
            {
                ScrollRight();
                moved = true;
            }
            else if (targetCenter.X < cameraCenter.X - Tiles.tileWidth)
            {
                ScrollLeft();
                moved = true;
            }
            if (targetCenter.Y > cameraCenter.Y + Tiles.tileHeight)
            {
                ScrollDown();
                moved = true;
            }
            else if (targetCenter.Y < cameraCenter.Y - Tiles.tileHeight)
            {
                ScrollUp();
                moved = true;
            }
            if(!moved)
                moveCam = false;

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

        private void updateCenter()
        {
            cameraCenter.X = position.X + Game1.screenWidth / 2;
            cameraCenter.Y = position.Y + Game1.screenHeight / 2;
        }

        public void resetMotion()
        {
            motion = Vector2.Zero;
        }
    }
}
