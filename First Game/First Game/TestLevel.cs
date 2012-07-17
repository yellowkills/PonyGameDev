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
        private Hero _player;
        public Map map;
        private Camera camera;
        private HUD hud;

        // Protected Variables : none
        // Public Variables : none


        // Default Constructor
        public TestLevel(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            hud = new HUD(game, spriteBatch);
        }
        

        // Loading :: Important: always load the player before the map or player will be null. this needs to be fixed somehow
        public void LoadHero(Hero _player) // this will be changed to load all the entities for a lvl
        {
            this._player = _player;
            Components.Add(_player);
            hud.setHero(_player);
            hud.Show();
            Components.Add(hud);
        }
        public void LoadMap(Camera camera, Texture2D spritesheet)
        {
            this.map = new Map(camera, spritesheet);
            this.camera = camera;
            _player.map = map;

        }

        public void switchToBattleMap()
        {
            map.switchToBattleMap();
            _player.map = map;
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
