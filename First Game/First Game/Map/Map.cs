using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace First_Game
{
    class Map
    {
        //Statics
        
        public static int mapWidthInPixels;
        public static int mapHeightInPixels;
        public static int tileMapWidth;
        public static int tileMapHeight;

        //Pointers to objects of interest
        private Camera camera;

        //Private Vars
        private Texture2D spritesheet;
        private Tiles tiles;

        public int[,] map;


        public static int MapWidthInPixels
        {
            get { return mapWidthInPixels; }
        }
        public static int MapHeightInPixels
        {
            get { return mapHeightInPixels; }
        }

        /** Constructor **/
        public Map(Camera camera, Texture2D spritesheet)
        {
            this.camera = camera;
            this.spritesheet = spritesheet;
            tiles = new Tiles(spritesheet);
        }

        public void setSpriteSheet(Texture2D spritesheet)
        {
            this.spritesheet = spritesheet;
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            Point cameraPoint = Tiles.VectorToCell(camera.pubPosition);
            Point viewPoint = Tiles.VectorToCell(camera.pubPosition + Tiles.ViewPortVector());

            Point min = new Point();
            Point max = new Point();

            min.X = cameraPoint.X;
            min.Y = cameraPoint.Y;
            max.X = (int)Math.Min(viewPoint.X, map.GetLength(1));
            max.Y = (int)Math.Min(viewPoint.Y, map.GetLength(0));

            Rectangle tileRectangle = new Rectangle(0, 0, Tiles.tileWidth, Tiles.tileHeight);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);

            for (int y = min.Y; y < max.Y; y++)
            {
                for (int x = min.X; x < max.X; x++)
                {
                    tileRectangle.X = x * Tiles.tileWidth - (int)camera.pubPosition.X;
                    tileRectangle.Y = y * Tiles.tileHeight - (int)camera.pubPosition.Y;
                    spriteBatch.Draw(spritesheet, tileRectangle, tiles.tiles[map[y, x]], Color.White);
                }
            }

            spriteBatch.End();

        }
    }
}
