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
    class Player
    {
        // [Private Variables]
        private int numLives; // This is only here b/c it is standard in most games. We may not even use this
        private Hero[] heroes;

        // [Protected Variables] : none
        // [Public Variables] : none


        // Default Constructor
        public Player(Game game, SpriteBatch spritebatch, Camera camera, Vector2 position)
        {
            numLives = 3; // Hardcoded for simplicity. I will change this later.
        }

        public void setHeroesPlayable(Hero[] heroes)
        {
            this.heroes = heroes;
        }

    }
}
