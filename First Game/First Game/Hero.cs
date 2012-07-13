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
    class Hero : Entity
    {
        // [Private Variables]
        private const int _heroHeight = 64;
        private const int _heroWidth = 32;

        // [Protected Variables] : none

        // [Public Variables]
        public Texture2D img;
        public Map map;
        public bool DEBUG = true;


        // Default Constructor
        public Hero(Game game, SpriteBatch spriteBatch, Camera camera, Map map, Vector2 position, Texture2D img)
            : base(game, spriteBatch, camera)
        {
            this.position = position;
            this.img = img;
            this.map = map;

            // Physics stuff
            xAcceleration = .15f;
            yAcceleration = .5f;
            friction = .9f;
            airFriction = .98f;
            gravity = .2f;
            jumpforce = -6.0f;
            maxSpeedX = 4.0f;
            maxSpeedY = 7.0f;

            direction = Direction.RIGHT;
            state = State.STANDING;

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

        // Updates the position and re-calculates all the collision points. [inefficient? maybe]
        void RefreshPosition()
        {
            position.X += DeltaX;
            position.Y += DeltaY;
        

            top.X = position.X + rect.Width / 2;
            top.Y = position.Y;

            botleft.X = position.X + 7;
            botleft.Y = position.Y + rect.Height;

            botright.X = position.X + rect.Width - 7;
            botright.Y = position.Y + rect.Height;

            midleftHIGH.X = position.X;
            midleftHIGH.Y = position.Y + rect.Height * (4.0f / 10.0f);

            midleftLOW.X = position.X;
            midleftLOW.Y = position.Y + rect.Height * (9.0f / 10.0f) - 4;

            midrightHIGH.X = position.X + rect.Width;
            midrightHIGH.Y = position.Y + rect.Height * (4.0f / 10.0f);

            midrightLOW.X = position.X + rect.Width;
            midrightLOW.Y = position.Y + rect.Height * (9.0f / 10.0f) - 4;
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
        }
        public override void Draw(GameTime gameTime)
        {
            rect.X = (int)position.X - (int)camera.Position.X;
            rect.Y = (int)position.Y - (int)camera.Position.Y;


            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            spriteBatch.Draw(img,rect,Color.White);
            spriteBatch.End();

            if (DEBUG)
            {
                drawTestedCells(map.map);
                drawCollisionPoints();
            }
        }
    }
}
