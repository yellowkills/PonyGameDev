using System;
using System.Collections.Generic;
using System.Linq;
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
    class TestLevel : GameScreen
    {

        // Private Variables
        private Hero hero;
        private Map map;
        private Camera camera;
        private HUD hud;

        // Protected Variables : none
        // Public Variables : none


        // Default Constructor
        public TestLevel(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            hud = new HUD(game, spriteBatch);
        }
        

        // Loading
        public void LoadCamera(Camera camera)
        {
            this.camera = camera;
        }
        public void LoadMap(Map map)
        {
            this.map = map;
        }
        public void LoadHero(Hero hero) // this will be changed to load all the entities for a lvl
        {
            this.hero = hero;
            Components.Add(hero);
            hud.setHero(hero);
            hud.Show();
            Components.Add(hud);

            
        }


        // Base Game Functions
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }
        public override void Draw(GameTime gameTime)
        {
            map.DrawMap(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
