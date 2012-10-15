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

namespace WhenRobotsAttack
{
    public class Level : GameScreen
    {

        #region Class Variables

        // Debug
        protected bool DEBUG = false;

        // Pointers
        private GameManager game;
        private Camera camera;

        // Heads Up Display
        private HUD hud;

        // Player and enemies
        private Player player;
        private Enemy[] enemies;

        // Map
        public Map map;

        #endregion

        // Default Constructor
        public Level(GameManager game, string LevelNumber)
            : base(game, game.spriteBatch)
        {
            this.game = game;
            this.player = game.player;
            this.camera = game.camera;
            
            // Loads Map
            Texture2D mapSpritesheet = game.Content.Load<Texture2D>("spritesheet_map-" + LevelNumber);
            this.map = new Map(game, mapSpritesheet, "Data\\map-" + LevelNumber + ".txt");

            // Loads Level
            loadLevel("Data\\level-" + LevelNumber + ".txt");

            Components.Add(this.player);

        }

        public void toggleDebug()
        {
            player.toggleDebug();
            foreach (Enemy e in enemies) e.toggleDebug();
        }


        // load all the level info from file 'filename' and creates the objects of this class
        private void loadLevel(string filename)
        {

            List<Tuple<string, string>> data = ReadFromFile.read(filename);
            string herostring = null;
            string enemystring = null;

            for (int i = 0; i < data.Count; i++)
            {
                switch (data[i].Item1)
                {
                    case "HEROES":
                        herostring = data[i].Item2;
                        break;
                    case "ENEMIES":
                        enemystring = data[i].Item2;
                        break;
                    default:
                        break;
                }
            }

            this.player.setHeroesPlayable(loadHeroes(herostring));
            this.enemies = loadEnemies(enemystring);

            player.Position = findPlayerSpawn(map);
            foreach (Enemy e in enemies) e.position = findEnemySpawn(map);
        }

        // Loads the trio of heroes for this level
        private Hero[] loadHeroes(string herostring)
        {

            string[] heroSet = herostring.Split('/');
            Hero[] heroes = new Hero[3];

            for (int i = 0; i < heroSet.Length; i++)
            {
                switch (heroSet[i])
                {
                    case "TwilightSparkle":
                        heroes[i] = new Hero(game, "TwilightSparkle");
                        break;
                    case "RainbowDash":
                        heroes[i] = new Hero(game, "RainbowDash");
                        break;
                    case "Applejack":
                        heroes[i] = new Hero(game, "Applejack");
                        break;
                    case "Fluttershy":
                        heroes[i] = new Hero(game, "Fluttershy");
                        break;
                    case "PinkiePie":
                        heroes[i] = new Hero(game, "PinkiePie");
                        break;
                    case "Rarity":
                        heroes[i] = new Hero(game, "Rarity");
                        break;
                    default:
                        break;
                }
            }

            return heroes;

        }

        // Loads the enemies for this level
        private Enemy[] loadEnemies(string enemystring)
        {
            string[] enemySet = enemystring.Split('/');
            Enemy[] enemies = new Enemy[enemySet.Length];

            for (int i = 0; i < enemySet.Length; i++)
            {
                switch (enemySet[i])
                {
                    case "Ponybot":
                        enemies[i] = new Enemy(game, "Ponybot");
                        break;
                    case "Alicornbot":
                        enemies[i] = new Enemy(game, "Alicornbot");
                        break;
                    default:
                        break;
                }
            }

            return enemies;
        }


        // Locate spawn points
        public Vector2 findPlayerSpawn(Map m)
        {
            Vector2 spawn = new Vector2();

            for (int i = 0; i < m.layer[m.collisionLayer].tile.GetLength(0); i++)
            {
                for (int j = 0; j < m.layer[m.collisionLayer].tile.GetLength(1); j++)
                {
                    int w = j * Map.tileMapWidth;
                    int h = i * Map.tileMapHeight;


                    if (m.layer[m.collisionLayer].tile[i, j].value == 4)
                        spawn = new Vector2(j * Map.tileWidth, i * Map.tileHeight);
                }
            }

            return spawn;
        }
        public Vector2 findEnemySpawn(Map m)
        {
            Vector2 spawn = new Vector2();

            for (int i = 0; i < m.layer[m.collisionLayer].tile.GetLength(0); i++)
            {
                for (int j = 0; j < m.layer[m.collisionLayer].tile.GetLength(1); j++)
                {
                    if (m.layer[m.collisionLayer].tile[i, j].value == 3)
                        spawn = new Vector2(j * Map.tileWidth, i * Map.tileHeight);
                }
            }

            return spawn;
        }

        // Checks if the two entities collide
        private void checkCollisions(Entity a, Entity b)
        {
            // TODO
        }

        // Base game functions
        public override void Update(GameTime gameTime)
        {
            camera.resetMotion();
            game.keyControls.kbdUpdate();
            camera.Update();

            // Player update and vsMap collision check
            player.Update(gameTime);
            map.checkTileCollisions(player.activeHero);

            // Enemies' update and vsMap collisions check. Then vsPlayer collision check
            foreach(Enemy e in enemies)
            {
                e.Update(gameTime);
                map.checkTileCollisions(e);
                checkCollisions(player.activeHero, e);
            }

            game.keyControls.storeStates();
        }
        public override void Draw(GameTime gameTime)
        {
            //base.Draw(gameTime);

            game.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            // This draws: 
            // 1. all the background layers
            // 2. the enemies
            // 3. the player
            // 4. collision and foreground layers 
            // 5. HUD
            //
            // TODO: Decide when the collision layer should be drawn [before player/after player]

            /*1*/
            for (int i = 0; i <= map.collisionLayer; i++)
                map.layer[i].Draw();

            /*2*/
            foreach (Enemy e in enemies)
                e.Draw(gameTime);

            /*3*/
            player.Draw(gameTime);

            /*4*/
            for (int i = map.collisionLayer+1; i < map.layer.Length; i++)
                map.layer[i].Draw();

            /*5*/
            // TODO: Draw HUD


            game.spriteBatch.End();
        }
    }
}
