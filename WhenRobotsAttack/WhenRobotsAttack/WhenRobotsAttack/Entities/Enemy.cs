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
    public class Enemy : KillableCreature
    {

        #region Class Variables

        public string enemyname;

        private static int _enemyHeight = 64;
        private static int _enemyWidth = 96;

        Animation walkLeft, walkRight;              // Animations
        Vector2 LframesStartPos, RframesStartPos;   // These are the positions for each frame on the sprite sheet (for walking)
        Rectangle RstandingRECT, LstandingRECT;     // These are the positions for the frames used when the hero is 
                                                    // standing. These are separate b/c these frames precede the walk animation
                                                    // and would disrupt the walk cycle if used with the other vectors.

        public Texture2D spritesheet;

        #endregion

        public Enemy(GameManager game, string enemyname)
            : base(game)
        {
            this.enemyname = enemyname;
            this.spritesheet = game.Content.Load<Texture2D>("spritesheet_" + enemyname);

            loadEnemy(enemyname);
            loadAnimations();
            setCollisionOffsets();
            
            this.status = Status.ACTIVE;
        }

        private void loadEnemy(string enemyname)
        {
            List<Tuple<string, string>> enemyInfo = ReadFromFile.read("Data\\enemies.txt");
            string[] enemyVars = null;

            for (int i = 0; i < enemyInfo.Count; i++)
            {
                if (enemyInfo[i].Item1 == enemyname)
                {
                    enemyVars = enemyInfo[i].Item2.Split('/');

                    // Loads the stats
                    this.attackPower = Convert.ToInt32(enemyVars[0]);
                    this.startingHP = Convert.ToInt32(enemyVars[1]);
                }
            }
        }

        // Loading
        private void loadAnimations()
        {
            int numframes = spritesheet.Width / _enemyWidth - 1;

            // !HARDCODE
            RstandingRECT = new Rectangle(0, 0, 96, 64);
            RframesStartPos = new Vector2(96, 0);
            LstandingRECT = new Rectangle(0, 64, 96, 64);
            LframesStartPos = new Vector2(96, 64);

            walkLeft = new Animation(spritesheet, LframesStartPos, _enemyWidth, _enemyHeight, numframes, 3);
            walkRight = new Animation(spritesheet, RframesStartPos, _enemyWidth, _enemyHeight, numframes, 3);
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


        // Base game functions
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (status == Status.ACTIVE)
            {
                if (state == State.INAIR)
                    DeltaX *= airFriction;
                else
                    DeltaX *= friction;

                if (Math.Abs(DeltaX) < 0.1f) DeltaX = 0;

                if (state != State.INAIR) state = (Math.Abs(DeltaX) < 1) ? State.STANDING : State.RUNNING;
            }
            else
            {
                
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
                default:
                    break;
            }
        }
    }
}
