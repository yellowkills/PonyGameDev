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
    public class Hero : KillableCreature
    {
        #region Class Variables

        public enum Status { ACTIVE, INACTIVE};
        public Status status;

        // The dimentions of the hero in pixels
        private static int _heroHeight = 64;
        private static int _heroWidth = 96;

        Animation walkLeft, walkRight;              // Animations
        Vector2 LframesStartPos, RframesStartPos;   // These are the positions for each frame on the sprite sheet (for walking)
        Rectangle RstandingRECT, LstandingRECT;     // These are the positions for the 'standing' frames on the sprite sheet

        // Spritesheet
        public Texture2D spritesheet;
        
        // Name
        public string name;

        #endregion

        public Hero(GameManager game, string ponyname)
            : base(game)
        {
            rect = new Rectangle((int)position.X, (int)position.Y, _heroWidth, _heroHeight);

            this.name = ponyname;
            this.status = Status.INACTIVE;
            this.spritesheet = game.Content.Load<Texture2D>("spritesheet_" + ponyname);
            loadHero(ponyname);
            loadAnimations();
            setCollisionOffsets();

            direction = Direction.RIGHT;
            state = State.STANDING;
            status = Status.INACTIVE;

            deltaX = 0.0f;
            deltaY = 0.0f;

            
        }

        // Loading
        private void loadHero(string heroname)
        {
            List<Tuple<string, string>> heroInfo = ReadFromFile.read("Data\\heroes.txt");
            string[] heroVars = null;

            for (int i = 0; i < heroInfo.Count; i++)
            {
                if (heroInfo[i].Item1 == heroname)
                {
                    heroVars = heroInfo[i].Item2.Split('/');

                    // Loads the stats
                    this.attackPower = Convert.ToInt32(heroVars[0]);
                    this.startingHP = Convert.ToInt32(heroVars[1]);
                }
            }
        }

        // Loading
        private void loadAnimations()
        {
            int numframes = spritesheet.Width / _heroWidth-1;

            // !HARDCODE
            RstandingRECT = new Rectangle(0, 0, 96, 64);
            RframesStartPos = new Vector2(96, 0);
            LstandingRECT = new Rectangle(0, 64, 96, 64);
            LframesStartPos = new Vector2(96, 64);

            walkLeft = new Animation(spritesheet, LframesStartPos, _heroWidth, _heroHeight, numframes, 3);
            walkRight = new Animation(spritesheet, RframesStartPos, _heroWidth, _heroHeight, numframes, 3);
        }

        // Collision point offsets
        private void setCollisionOffsets()
        {
            // !HARDCODE
            topLeft = new Vector2(32, 16);
            topRight = new Vector2(64, 16);
            botLeft = new Vector2(32, rect.Height);
            botRight = new Vector2(64, rect.Height);
            leftSideHigh = new Vector2(25, 27);
            leftSideLow = new Vector2(25, 57);
            rightSideHigh = new Vector2(71, 27);
            rightSideLow = new Vector2(71, 57);
        }

        public void testHealing()
        {
            heal();
        }

        // Base game functions
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (status == Status.ACTIVE)
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
            }
            else
            {
                tickHealTimer();
                tickDamageTimer();
            }



            
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            switch (state)
            {
                case State.STANDING:
                    if (direction == Direction.LEFT) spriteBatch.Draw(spritesheet, rect, LstandingRECT, Color.White);
                    else spriteBatch.Draw(spritesheet, rect, RstandingRECT, Color.White);
                    break;
                case State.RUNNING:
                    if (direction == Direction.LEFT) walkLeft.Draw(spriteBatch, rect);
                    else walkRight.Draw(spriteBatch, rect);
                    break;
                case State.INAIR:
                    if (direction == Direction.LEFT) walkLeft.DrawFirstFrame(spriteBatch, rect);
                    else walkRight.DrawFirstFrame(spriteBatch, rect);
                    break;
            }
        }
    }
}
