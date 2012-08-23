using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace First_Game
{
    class Tiles
    {
        private const int numTiles = 4; // The number of tile positions that will be loaded into the rectangle array.
                                        // ^hardcoded. need to fix


        //Pointers to Objects of Interest
        public static int tileHeight;
        public static int tileWidth;

        public Rectangle[] tiles;

        public Tiles(Texture2D spritesheet)
        {
            tileHeight = 32;
            tileWidth = 32;
            loadTiles(spritesheet);
        }

        // Hardcoded. need to fix
        private void loadTiles(Texture2D spritesheet) 
        {
            tiles = new Rectangle[numTiles];

            for (int i = 0; i < 4; i++)
            {
                tiles[i] = new Rectangle((i+1)*tileWidth, 0, tileWidth, tileHeight);
            }
        }

        /*Helper Methods*/

        public static Point VectorToCell(Vector2 vector)
        {
            return new Point(
                        (int)(vector.X / tileWidth),
                        (int)(vector.Y / tileHeight));
        }
        public static Vector2 ViewPortVector()
        {
            return new Vector2(
                    GameManager.screenWidth + tileWidth,
                    GameManager.screenHeight + tileHeight);
        }

        public static Point[] NearbyCells(Hero h)
        {
            List<Point> cells = new List<Point>();
            Point topleft = VectorToCell(new Vector2(h.position.X - Map.tileMapWidth, h.position.Y - tileHeight));
            Point bottomright = VectorToCell(new Vector2(h.position.X + h.rect.Width + Map.tileMapWidth,
                                                         h.position.Y + h.rect.Height + tileHeight));
            for (int i = topleft.Y; i <= bottomright.Y; i++)
                for (int j = topleft.X; j <= bottomright.X; j++)
                    cells.Add(new Point(j, i));

            return cells.ToArray();
        }
    }
}
