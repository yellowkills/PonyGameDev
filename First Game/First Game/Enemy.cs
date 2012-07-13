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
    class Enemy : Entity //TODO
    {
        // [Private Variables] : none
        // [Protected Variables] : none
        // [Public Variables] : none


        // Default Constructor
        public Enemy(Game game, SpriteBatch spriteBatch, Camera camera, Vector2 position)
            : base(game, spriteBatch, camera)
        {
        }
    }
}
