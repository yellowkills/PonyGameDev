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
    class Hero : KillableCreature
    {
        // [Private Variables]
        private const int _heroHeight = 64;
        private const int _heroWidth = 96;

        Animation walkLeft, walkRight;              // Animations
        Vector2 LframesStartPos, RframesStartPos;   // These are the positions for each frame on the sprite sheet (for walking)
        Rectangle RstandingRECT, LstandingRECT;     // These are the positions for the frames used when the hero is 
                                                    // standing. These are separate b/c the frames precede the walk animation
                                                    // and would disrupt the walk cycle if used with the other vectors.

        // [Protected Variables] : none

        // [Public Variables]
        public Texture2D spritesheet;
        public Map map;
        public bool DEBUG = false;

 
        // Default Constructor
        public Hero(GameManager game, SpriteBatch spriteBatch, Camera camera,Vector2 position, int startingHealth,Texture2D spritesheet)
            : base(game, spriteBatch, camera, startingHealth)
        {
            this.position = position;
            this.spritesheet = spritesheet;

            // Physics stuff
            xAcceleration = .2f;
            yAcceleration = .5f;
            friction = .7f;
            airFriction = .98f;
            gravity = .4f;
            //gravity = 0.0f;
            jumpforce = -20.0f;
            maxSpeedX = 4.5f;
            maxSpeedY = 9.0f;

            direction = Direction.RIGHT;
            state = State.STANDING;

            rect = new Rectangle((int)position.X, (int)position.Y, _heroWidth, _heroHeight);
            pxlrect = new Rectangle(0, 0, 3, 3);


            // Animation stuff
            RstandingRECT = new Rectangle(0, 0, 96, 64);
            RframesStartPos = new Vector2(96, 0);
            LstandingRECT = new Rectangle(0, 64, 96, 64);
            LframesStartPos = new Vector2(96, 64);
            setAnimation();

            // Collision Points
            topLeft = new Vector2(32,16);
            topRight = new Vector2(64,16);
            botLeft = new Vector2(32, rect.Height);
            botRight = new Vector2(64, rect.Height);
            leftSideHigh = new Vector2(25,27);
            leftSideLow = new Vector2(25,57);
            rightSideHigh = new Vector2(71,27);
            rightSideLow = new Vector2(71,57);



            deltaX = 0.0f;
            deltaY = 0.0f;
        }

        // Debug toggle
        public void toggleDebug()
        {
            DEBUG = !DEBUG;
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


        // Base Game Functions
        public override void Update(GameTime gameTime)
        {
            tickTimers();
            // TODO: cause faster deceleration when the player switches directions

            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                if(state == State.INAIR)
                    DeltaX *= airFriction;
                else
                    DeltaX *= friction;

                if (Math.Abs(DeltaX) < 0.1f) DeltaX = 0;
            }

            DeltaY += gravity;

            if(state != State.INAIR) state = (Math.Abs(DeltaX) < 1) ? State.STANDING : State.RUNNING;
            
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
            //spriteBatch.Draw(img,rect,Color.White);
            spriteBatch.End();

            if (DEBUG)
            {
                drawTestedCells(map.map);
                drawCollisionPoints();
            }
        }
    }
}
