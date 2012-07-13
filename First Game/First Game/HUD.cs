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
        // [Protected Variables] : none
        // [Public Variables] : none


        // Default Constructor
        public HUD(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
        }
    }
}