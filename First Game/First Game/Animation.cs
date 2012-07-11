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
    class Animation
    {
        private Texture2D spritesheet;
        private Vector2[] frames;
        private Rectangle clip;
        private int frameWidth, frameHeight, frameIndex, numFrames;
        private int timespan, countdown;



        public Animation(Texture2D spritesheet, Vector2[] frames, int frameWidth,
                         int frameHeight, int numFrames, int timespan)
        {
            this.spritesheet = spritesheet;
            this.frames = frames;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.numFrames = numFrames;
            this.timespan = timespan;
            this.frameIndex = 0;

            clip = new Rectangle((int) frames[0].X, (int) frames[0].Y, frameWidth, frameHeight);
        }

        public int FrameIndex
        {
            get { return frameIndex; }
            set
            {
                if (value >= numFrames)
                    frameIndex = 0;
                else
                    frameIndex = value;
            }
        }
        public int Countdown
        {
            get { return countdown; }
            set
            {
                if (value < 0)
                {
                    countdown = timespan;
                    FrameIndex += 1;
                    clip.X = (int)frames[0].X + frameWidth * FrameIndex;
                }
                else
                    countdown = value;
            }
        }


        public void reset()
        {
            FrameIndex = 0;
        }

        public void DrawFirstFrame(SpriteBatch spriteBatch, Rectangle rect)
        {
            clip.X = (int)frames[0].X;
            spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.NonPremultiplied);
            spriteBatch.Draw(spritesheet, rect, clip, Color.White);
            spriteBatch.End();
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            spriteBatch.Draw(spritesheet, rect, clip, Color.White);
            spriteBatch.End();

            Countdown--;
        }
    }
}
