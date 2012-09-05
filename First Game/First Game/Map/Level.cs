using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    class Level : GameScreen
    {
        // Private Variables : none
        // Protected Variables
        protected kbdController keyControls;
        protected Player player;
        protected Camera camera;
        protected HUD hud;
        protected bool DEBUG = false;
        
        // Public Variables
        public Map map;

        // Default Constructor
        public Level(GameManager game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            hud = new HUD(game, spriteBatch);
        }


        // Loads all of the major assests for the level
        public void LoadAssets(Player player, Map map, kbdController keyControls)
        {
            this.map = map;
            this.player = player;
            this.keyControls = keyControls;
            player.setMap(map);
            player.activeHero.place(findPlayerSpawn(map));



            Components.Add(player);
            hud.setHero(player.activeHero);
            Components.Add(hud);
        }

        public Vector2 findPlayerSpawn(Map m)
        {
            Vector2 spawn = new Vector2();

            for (int i = 0; i < m.map.GetLength(0); i++)
            {
                for (int j = 0; j < m.map.GetLength(1); j++)
                {
                    if(m.map[i,j] == 3)
                        spawn = new Vector2(j*32,i*32);
                }
            }

            return spawn;
        }
        public Vector2 findEnemySpawn(Map m)
        {
            Vector2 spawn = new Vector2();

            for (int i = 0; i < m.map.GetLength(0); i++)
            {
                for (int j = 0; j < m.map.GetLength(1); j++)
                {
                    if(m.map[i,j] == 2)
                        spawn = new Vector2(j*32,i*32);
                }
            }

            return spawn;
        }

        // Base Game Functions
        public override void Update(GameTime gameTime)
        {
            camera.resetMotion();

            keyControls.kbdUpdate();
            camera.Update();

            base.Update(gameTime);
            keyControls.storeStates();
        }
        public override void Draw(GameTime gameTime)
        {
            map.DrawMap(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
