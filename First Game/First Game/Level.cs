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
        public Level(Game game, SpriteBatch spriteBatch, Player player, Map map)
            : base(game, spriteBatch)
        {

        }

        public void loadMap(string filename)
        {
            string line;
            List<string> lines = new List<string>();
            StreamReader reader = new StreamReader(filename);

            while(!reader.EndOfStream)
            {
                line = reader.ReadLine();
                lines.Add(line);
            }




        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
