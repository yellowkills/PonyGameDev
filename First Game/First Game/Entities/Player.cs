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
    class Player : DrawableGameComponent
    {
        private GameManager game;
        

        public Hero[] heroes;
        public Hero activeHero;


        // Default Constructor
        public Player(GameManager game, SpriteBatch spriteBatch, Camera camera)
            : base(game)
        {
            // !HARDCODE! This is where the player starts. This will be removed once the player spawn block is implemented
            Vector2 startPos = new Vector2(100, 100);

            // !HARDCODE! creates the heroes.
            Texture2D spritesheet_Twilight = game.Content.Load<Texture2D>("spritesheet_Twilight");
            Hero[] heroes = new Hero[] { new Hero(game, spriteBatch, camera, startPos, 8, spritesheet_Twilight) };
            setHeroesPlayable(heroes);

            
        }

        // Debug toggle
        public void toggleDebug()
        {
            foreach (Hero h in heroes)
                h.toggleDebug();
        }

        public void setHeroesPlayable(Hero[] heroes)
        {
            this.heroes = heroes;
            activeHero = heroes[0];
            
        }


        public void setMap(Map map)
        {
            foreach (Hero h in heroes)
                h.map = map;
        }

        public override void Update(GameTime gameTime)
        {
            activeHero.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            activeHero.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
