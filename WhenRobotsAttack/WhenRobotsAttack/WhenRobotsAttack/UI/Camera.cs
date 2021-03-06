﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WhenRobotsAttack
{
    public class Camera
    {

        #region Class Variables

        private Vector2 position;
        private float speed;
        private Vector2 motion;
        private Vector2 cameraCenter;
        private Vector2 targetCenter;
        private bool moveCam;

        /***Pointers***/
        private GameManager gamePtr;

        private bool hasTarget;
        private Entity lockTarget;

        #endregion

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
                        Map.mapWidthInPixels - GameManager.screenWidth);
                position.Y = MathHelper.Clamp(value.Y, 0,
                        Map.mapHeightInPixels - GameManager.screenHeight);
            }
        }

        public Vector2 TargetPosition
        {
            get { return position; }
            set
            {
                position.X = MathHelper.Clamp(value.X, 0,
                        Map.mapWidthInPixels - GameManager.screenWidth);
                position.Y = MathHelper.Clamp(value.Y, 0,
                        Map.mapHeightInPixels - GameManager.screenHeight);
            }
        }

        /******************************/

        public Camera(GameManager parent)
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
            if (moveCam)
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
                if (lockTarget.position.X >= (GameManager.screenWidth / 3) * 2 ||
                    lockTarget.position.X <= GameManager.screenWidth / 3)
                {
                    targetCenter.X = lockTarget.position.X;
                    moveCam = true;
                }

                /* Platform Lock on Y axis */
                if (lockTarget.position.Y > (GameManager.screenHeight / 3) * 2 ||
                    lockTarget.position.Y < (GameManager.screenHeight / 3))
                {
                    targetCenter.Y = lockTarget.position.Y; //Target the player's current Y as the center for the camera.
                    moveCam = true;
                }
            }
        }

        private void gotoTarget()
        {
            bool moved = false;
            if (targetCenter.X > cameraCenter.X + Map.tileWidth)
            {
                ScrollRight();
                moved = true;
            }
            else if (targetCenter.X < cameraCenter.X - Map.tileWidth)
            {
                ScrollLeft();
                moved = true;
            }
            if (targetCenter.Y > cameraCenter.Y + Map.tileHeight)
            {
                ScrollDown();
                moved = true;
            }
            else if (targetCenter.Y < cameraCenter.Y - Map.tileHeight)
            {
                ScrollUp();
                moved = true;
            }
            if (!moved)
                moveCam = true;

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
            cameraCenter.X = position.X + GameManager.screenWidth / 2;
            cameraCenter.Y = position.Y + GameManager.screenHeight / 2;
        }

        public void resetMotion()
        {
            motion = Vector2.Zero;
        }
    }
}
