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
    class Animation
    {
        private Texture2D spritesheet;
        private Vector2[] frames;
        private Rectangle clip;
        private int frameWidth, frameHeight, frameIndex, numFrames;
        private int timespan, countdown;

        public Animation(Texture2D spritesheet, Vector2 startingPosition, int frameWidth, int frameHeight, int numFrames,
                         int timespan)
        {
            this.spritesheet = spritesheet;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.numFrames = numFrames;
            this.timespan = timespan;
            this.frameIndex = 0;
            this.frames = generateFramePositions(startingPosition, frameWidth, numFrames);

            clip = new Rectangle((int)startingPosition.X, (int)startingPosition.Y, frameWidth, frameHeight);
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
                    clip.X = (int)frames[FrameIndex].X;
                }
                else
                    countdown = value;
            }
        }

        public Vector2[] generateFramePositions(Vector2 startingPosition, int frameWidth, int numFrames)
        {
            Vector2[] f = new Vector2[numFrames];

            for (int i = 0; i < numFrames; i++)
            {
                f[i] = new Vector2(startingPosition.X + i * frameWidth, startingPosition.Y);
            }

            return f;
        }


        public void reset()
        {
            FrameIndex = 0;
        }

        public void DrawFirstFrame(SpriteBatch spriteBatch, Rectangle rect)
        {
            clip.X = (int)frames[0].X;
            spriteBatch.Draw(spritesheet, rect, clip, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(spritesheet, rect, clip, Color.White);

            Countdown--;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 spritePos, float rotationAngle)
        {
            spriteBatch.Draw(spritesheet, spritePos, clip, Color.White, rotationAngle, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

            Countdown--;
        }
    }
}
