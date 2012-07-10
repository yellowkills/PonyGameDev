using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace First_Game
{
    class Camera
    {
        Vector2 position;
        float speed;

        public Camera()
        {
            position = new Vector2();
            speed = 10.0f;
        }

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
                        Game1.MapWidthInPixels - Game1.ScreenWidth);
                position.Y = MathHelper.Clamp(value.Y, 0,
                        Game1.MapHeightInPixels - Game1.ScreenHeight);
            }
        }
    }
}
