using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WhenRobotsAttack
{
    public class TileLayer
    {
        public Tile[,] tile;
        Texture2D spritesheetPTR; // A pointer to the sprite sheet
        GameManager game;
        int[] fullHeightmap;
        int[] xySlopeHeightmap;

        public TileLayer(GameManager game, Texture2D spritesheet, int[,] tileNums)
        {
            this.game = game;
            this.spritesheetPTR = spritesheet;

            buildHeightmaps();

            tile = buildTileLayer(tileNums);
        }

        private Tile[,] buildTileLayer(int[,] tileNums)
        {
            Tile[,] TL = new Tile[tileNums.GetLength(1), tileNums.GetLength(0)];

            for (int x = 0; x < tileNums.GetLength(0); x++)
                for (int y = 0; y < tileNums.GetLength(1); y++)
                {
                    TL[y, x] = new Tile(game, spritesheetPTR, tileNums[x, y], new Rectangle(tileNums[x, y] * Map.tileWidth, 0, Map.tileWidth, Map.tileHeight));
                    if (TL[y, x].value == 1 || TL[y, x].value == 2 || TL[y, x].value == 3)
                        TL[y, x].heightmap = fullHeightmap;
                    else if (TL[y, x].value == 4)
                        TL[y, x].heightmap = xySlopeHeightmap;
                    else if (TL[y, x].value == 5)
                    {
                        Array.Reverse(xySlopeHeightmap);
                        TL[y, x].heightmap = xySlopeHeightmap;
                    }
                }

                    return TL;
        }

        // hardcoded for testing
        private void buildHeightmaps()
        {
            fullHeightmap = new int[Map.tileWidth];
            xySlopeHeightmap = new int[Map.tileWidth];

            for (int i = 0; i < Map.tileWidth; i += 1)
                fullHeightmap[i] = Map.tileHeight;

            for (int i = 0; i < Map.tileWidth; i += 1)
                xySlopeHeightmap[i] = i;
        }






        public static Point VectorToCell(Vector2 vector)
        {
            return new Point(
                        (int)(vector.X / Map.tileWidth),
                        (int)(vector.Y / Map.tileHeight));
        }
        public static Vector2 ViewPortVector()
        {
            return new Vector2(
                    GameManager.screenWidth + Map.tileWidth,
                    GameManager.screenHeight + Map.tileHeight);
        }

        // Draws only the tiles the are within the view of the camera
        public void Draw()
        {

            Point cameraPoint = VectorToCell(game.camera.pubPosition);
            Point viewPoint = VectorToCell(game.camera.pubPosition + ViewPortVector());

            Point min = new Point();
            Point max = new Point();

            min.X = cameraPoint.X;
            min.Y = cameraPoint.Y;
            max.X = (int)Math.Min(viewPoint.X, tile.GetLength(1));
            max.Y = (int)Math.Min(viewPoint.Y, tile.GetLength(0));

            Rectangle tileRectangle = new Rectangle(0, 0, Map.tileWidth, Map.tileHeight);

            for (int y = min.Y; y < max.Y; y++)
            {
                for (int x = min.X; x < max.X; x++)
                {
                    tileRectangle.X = x * Map.tileWidth - (int)game.camera.pubPosition.X;
                    tileRectangle.Y = y * Map.tileHeight - (int)game.camera.pubPosition.Y;
                    tile[y, x].Draw(tileRectangle);
                }
            }

        }
    }
}
