using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WhenRobotsAttack
{
    public class Player : DrawableGameComponent
    {
        private GameManager game;

        public string currentLevel;

        public Hero activeHero;
        public Hero[] heroes;

        public Vector2 Position
        {
            set { foreach (Hero h in heroes) h.position = value; }

            get { return activeHero.position; }
        }

        // Default Constructor
        public Player(GameManager game)
            : base(game)
        {
            this.game = game;
            loadPlayer("Data\\player.txt");
        }

        // Debug toggle
        public void toggleDebug()
        {
            foreach (Hero h in heroes) h.toggleDebug();
        }

        // Loading
        private void loadPlayer(string filename)
        {
            List<Tuple<String, String>> data = ReadFromFile.read(filename);

            for (int i = 0; i < data.Count; i++)
            {
                switch (data[i].Item1)
                {
                    case "CURRENT LEVEL":
                        currentLevel = data[i].Item2;
                        break;
                    default:
                        break;
                }
            }
        }

        // Sets the heroes that will be uses in the current level
        public void setHeroesPlayable(Hero[] heroes)
        {
            this.heroes = heroes;
            activeHero = heroes[0];
            activeHero.status = Hero.Status.ACTIVE;
            game.camera.lockEntity(activeHero);
        }

        // Base game functions
        public override void Update(GameTime gameTime)
        {
            activeHero.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            activeHero.Draw(gameTime);
        }


    }
}
