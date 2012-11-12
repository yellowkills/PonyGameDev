using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WhenRobotsAttack
{
    public class Tile
    {
        Rectangle clip; // The section of the sprite sheet that contains the image for this tile
        Texture2D spritesheetPTR; // A pointer to the sprite sheet
        public int value;
        GameManager game;
        public int[] heightmap;

        public Tile(GameManager game, Texture2D spritesheetPTR, int value,Rectangle clip)
        {
            this.game = game;
            this.spritesheetPTR = spritesheetPTR;
            this.clip = clip;
            this.value = value;
        }


        // Clips the image from the sprite sheet and draws it on the screen at the proper location
        public void Draw(Rectangle location)
        {
            game.spriteBatch.Draw(spritesheetPTR, location, clip, Color.White);
        }
    }
}
