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
    class Enemy : KillableCreature //TODO
    {

        private const int _enemyHeight = 64;
        private const int _enemyWidth = 96;

        Animation walkLeft, walkRight;              // Animations
        Vector2 LframesStartPos, RframesStartPos;   // These are the positions for each frame on the sprite sheet (for walking)
        Rectangle RstandingRECT, LstandingRECT;     // These are the positions for the frames used when the hero is 
                                                    // standing. These are separate b/c these frames precede the walk animation
                                                    // and would disrupt the walk cycle if used with the other vectors.

        public Texture2D spritesheet;
        public Map map;


        // Default Constructor
        public Enemy(GameManager game, SpriteBatch spriteBatch, Camera camera, int startingHealth)
            : base(game, spriteBatch, camera, startingHealth)
        {
        }

        private void setAnimation()
        {
            walkLeft = new Animation(spritesheet, LframesStartPos, 96, 64, 12, 3);
            walkRight = new Animation(spritesheet, RframesStartPos, 96, 64, 12, 3);
        }


        public void testHealing()
        {
            heal();
        }













        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                if (state == State.INAIR)
                    DeltaX *= airFriction;
                else
                    DeltaX *= friction;

                if (Math.Abs(DeltaX) < 0.1f) DeltaX = 0;
            }

            DeltaY += gravity;

            if (state != State.INAIR) state = (Math.Abs(DeltaX) < 1) ? State.STANDING : State.RUNNING;

            RefreshPosition();
            CollisionTest(map.map);
        }
        public override void Draw(GameTime gameTime)
        {
            rect.X = (int)position.X - (int)camera.pubPosition.X;
            rect.Y = (int)position.Y - (int)camera.pubPosition.Y;

            switch (state)
            {
                case State.STANDING:
                    if (direction == Direction.LEFT)
                    {
                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                        spriteBatch.Draw(spritesheet, rect, LstandingRECT, Color.White);
                        spriteBatch.End();
                    }
                    else
                    {
                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                        spriteBatch.Draw(spritesheet, rect, RstandingRECT, Color.White);
                        spriteBatch.End();
                    }
                    break;
                case State.RUNNING:
                    if (direction == Direction.LEFT)
                        walkLeft.Draw(spriteBatch, rect);
                    else
                        walkRight.Draw(spriteBatch, rect);
                    break;
                case State.INAIR:
                    if (direction == Direction.LEFT)
                        walkLeft.DrawFirstFrame(spriteBatch, rect);
                    else
                        walkRight.DrawFirstFrame(spriteBatch, rect);
                    break;
            }





            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            spriteBatch.End();
        }
    }
}
