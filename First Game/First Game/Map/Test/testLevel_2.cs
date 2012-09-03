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
    class testLevel_2 : Level
    {

        // Private Variables
        private Texture2D spritesheet_GenericMap, spritesheet_GenericMap_DEBUG, spritesheet_Twilight, spritesheet_ponybot1;

        // Protected Variables : none
        // Public Variables : none


        // Default Constructor
        public testLevel_2(GameManager game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            hud = new HUD(game, spriteBatch);

            spritesheet_GenericMap = game.Content.Load<Texture2D>("spritesheet_map_genericTiles");
            spritesheet_GenericMap_DEBUG = game.Content.Load<Texture2D>("spritesheet_map_genericTiles_DEBUG");
            spritesheet_Twilight = game.Content.Load<Texture2D>("spritesheet_Twilight"); // TWILIGHT!
            spritesheet_ponybot1 = game.Content.Load<Texture2D>("spritesheet_ponybot1"); // ROBOTS!

            camera = new Camera(game);

            testMap_2 testmap2 = new testMap_2(camera, spritesheet_GenericMap);

            Vector2 startPos = new Vector2(100, 100);

            // Player Creation
            Hero[] heroes = new Hero[] { new Hero(game, spriteBatch, camera, startPos, 8, spritesheet_Twilight) };
            player = new Player(game, spriteBatch, camera);
            player.setHeroesPlayable(heroes);
            camera.lockEntity(player.activeHero); // TODO: Change lockEntity so that it locks onto a player instead of a hero
            kbdController keyControls = new kbdController(game, player.activeHero);


            LoadAssets(player, testmap2, keyControls);
        }

        // Debug toggle
        public void toggleDebug()
        {
            DEBUG = !DEBUG;
            player.toggleDebug();

            if (DEBUG)
                map.setSpriteSheet(spritesheet_GenericMap_DEBUG);
            else
                map.setSpriteSheet(spritesheet_GenericMap);
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
