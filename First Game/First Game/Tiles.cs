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
        //Pointers to Objects of Interest
        private Game1 gamePtr;

        public static int tileHeight;
        public static int tileWidth;

        public List<Texture2D> tiles;

        //Find a way to protect these.
        public Texture2D wht, blk, clr, red;

        public Tiles(Game1 mainGame)
        {
            gamePtr = mainGame;
            tiles = new List<Texture2D>();
            tileHeight = 32;
            tileWidth = 32;
            loadTiles();
        }

        private void loadTiles()
        {
            wht = gamePtr.Content.Load<Texture2D>("WhiteBox");
            tiles.Add(wht);

            blk = gamePtr.Content.Load<Texture2D>("BlackBox");
            tiles.Add(blk);

            clr = gamePtr.Content.Load<Texture2D>("ClearBox");
            tiles.Add(clr);

            red = gamePtr.Content.Load<Texture2D>("RedBox");
            tiles.Add(red);
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
                    Game1.screenWidth + tileWidth,
                    Game1.screenHeight + tileHeight);
        }

        public static Point[] NearbyCells(Player h)
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
