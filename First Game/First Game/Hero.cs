﻿using System;
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

        Animation walkLeft, walkRight;
        Vector2[] Rframes, Lframes;
        Rectangle RstandingRECT, LstandingRECT;

        // [Protected Variables] : none

        // [Public Variables]
        public Texture2D spritesheet;
        public Map map;
        public bool DEBUG = true;

 
        // Default Constructor
        public Hero(Game game, SpriteBatch spriteBatch, Camera camera, Map map, Vector2 position, int startingHealth,Texture2D spritesheet)
            : base(game, spriteBatch, camera, position, startingHealth)
        {
            this.position = position;
            this.spritesheet = spritesheet;
            this.map = map;

            // Physics stuff
            xAcceleration = .2f;
            yAcceleration = .5f;
            friction = .7f;
            airFriction = .98f;
            gravity = .4f;
            jumpforce = -20.0f;
            maxSpeedX = 4.5f;
            maxSpeedY = 9.0f;

            direction = Direction.RIGHT;
            state = State.STANDING;


            // Animation stuff
            RstandingRECT = new Rectangle(0, 0, 96, 64);
            Rframes = new Vector2[12] { new Vector2(96, 0), new Vector2(192, 0), new Vector2(288, 0), new Vector2(384, 0),
                                        new Vector2(480, 0), new Vector2(576, 0), new Vector2(672, 0), new Vector2(768, 0),
                                        new Vector2(864, 0), new Vector2(960, 0), new Vector2(1056, 0), new Vector2(1152, 0)};
            LstandingRECT = new Rectangle(0, 64, 96, 64);
            Lframes = new Vector2[12] { new Vector2(96, 64), new Vector2(192, 64), new Vector2(288, 64), new Vector2(384, 64),
                                        new Vector2(480, 64), new Vector2(576, 64), new Vector2(672, 64), new Vector2(768, 64),
                                        new Vector2(864, 64), new Vector2(960, 64), new Vector2(1056, 64), new Vector2(1152, 64)};
            setAnimation();

            // Collision Points
            top = new Vector2(position.X + rect.Width / 2, position.Y);
            botleft = new Vector2(position.X, position.Y + rect.Height);
            botright = new Vector2(position.X + rect.Width, position.Y + rect.Height);
            midleftHIGH = new Vector2(position.X, position.Y + rect.Height * (4.0f / 10.0f));
            midleftLOW = new Vector2(position.X, position.Y + rect.Height * (9.0f / 10.0f));
            midrightHIGH = new Vector2(position.X + rect.Width, position.Y + rect.Height * (4.0f / 10.0f));
            midrightLOW = new Vector2(position.X + rect.Width, position.Y + rect.Height * (9.0f / 10.0f));

            rect = new Rectangle((int)position.X, (int)position.Y, _heroWidth, _heroHeight);
            pxlrect = new Rectangle(0, 0, 3, 3);

            deltaX = 0.0f;
            deltaY = 0.0f;
        }


        private void setAnimation()
        {
            walkLeft = new Animation(spritesheet, Lframes, 96, 64, 12, 3);
            walkRight = new Animation(spritesheet, Rframes, 96, 64, 12, 3);
        }


        // Base Game Functions
        public override void Update(GameTime gameTime)
        {

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
            rect.X = (int)position.X - (int)camera.Position.X;
            rect.Y = (int)position.Y - (int)camera.Position.Y;

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
