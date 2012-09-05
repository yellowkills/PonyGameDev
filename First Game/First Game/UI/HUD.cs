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
    class HUD : GameScreen
    {
        private Vector2 healthDisplay;
        private Hero hero;
        private SpriteFont hpFont;


        // Default Constructor
        public HUD(GameManager game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            healthDisplay = new Vector2(5, 5);
            hpFont = game.Content.Load<SpriteFont>("MenuFont - Medium");
        }


        public void setHero(Hero hero)
        {
            this.hero = hero;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            spriteBatch.DrawString(hpFont, "HP " + Convert.ToString(hero.HP), healthDisplay, Color.Red);
            spriteBatch.End();
        }
    }
}