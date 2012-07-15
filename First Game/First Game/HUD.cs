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
    class HUD : GameScreen //TODO
    {
        // [Private Variables] : none
        //private MenuComponent health;
        private Vector2 healthDisplay;
        private Hero _player;
        private SpriteFont hpFont;

        // [Protected Variables] : none
        // [Public Variables] : none


        // Default Constructor
        public HUD(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            healthDisplay = new Vector2(5, 5);
            hpFont = game.Content.Load<SpriteFont>("Font1");
        }


        public void setHero(Hero _player)
        {
            this._player = _player;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            spriteBatch.DrawString(hpFont, "HP " + Convert.ToString(_player.HP), healthDisplay, Color.Red);
            spriteBatch.End();
        }
    }
}