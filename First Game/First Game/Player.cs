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
        // [Private Variables]
        private int numLives; // This is only here b/c it is standard in most games. We may not even use this
        

        // [Protected Variables] : none

        // [Public Variables]
        public Hero[] heroes;
        public Hero activeHero;


        // Default Constructor
        public Player(Game game, SpriteBatch spritebatch, Camera camera, Vector2 position)
            : base(game)
        {
            numLives = 3; // Hardcoded for simplicity. This can be changed later.
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
    }
}
