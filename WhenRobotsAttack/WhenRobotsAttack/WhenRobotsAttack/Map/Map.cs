using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WhenRobotsAttack
{
    public class Map
    {

        #region Class Variables

        // Tile dimentions
        public static int tileWidth = 32;
        public static int tileHeight = 32;

        // Map dimentions
        public static int mapWidthInPixels;
        public static int mapHeightInPixels;
        public static int tileMapWidth;
        public static int tileMapHeight;

        //Pointers to objects of interest
        private GameManager game;
        private Camera camera;

        //Private Vars
        private Texture2D spritesheet;
        public int collisionLayer = 1;

        // layers of tile grids
        public TileLayer[] layer;

        #endregion

        // Default Consructor
        public Map(GameManager game, Texture2D spritesheet, string MapFileName)
        {
            this.game = game;
            this.camera = game.camera;

            this.spritesheet = spritesheet;
            loadMap(MapFileName);

            tileMapWidth = layer[collisionLayer].tile.GetLength(1);
            tileMapHeight = layer[collisionLayer].tile.GetLength(0);

            mapWidthInPixels = tileMapWidth * tileWidth;
            mapHeightInPixels = tileMapHeight * tileHeight;
        }

        // Finds and loads the map file. Then creates tilelayers and tiles.
        public void loadMap(string filename)
        {
            List<TileLayer> TL = new List<TileLayer>();
            List<int[,]> tileNums = ReadFromFile.readMap(filename);

            for (int i = 0; i < tileNums.Count; i++)
                TL.Add(new TileLayer(game, spritesheet, tileNums[i]));

            layer = TL.ToArray();
        }

        // Checks the collision on layer[1];
        public void checkTileCollisions(Entity e)
        {
            Point[] cells = e.NearbyCells(layer[collisionLayer].tile);

            Point CELL_topleft = VectorToCell(e.TopLeft);
            Point CELL_topright = VectorToCell(e.TopRight);
            Point CELL_botleft = VectorToCell(e.BotLeft);
            Point CELL_botright = VectorToCell(e.BotRight);
            Point CELL_leftSideHigh = VectorToCell(e.LeftSideHigh);
            Point CELL_leftSideLow = VectorToCell(e.LeftSideLow);
            Point CELL_rightSideHigh = VectorToCell(e.RightSideHigh);
            Point CELL_rightSideLow = VectorToCell(e.RightSideLow);

            List<Point> collisionCells = new List<Point> {  CELL_topleft, CELL_topright, 
                                                            CELL_botleft, CELL_botright,
                                                            CELL_leftSideHigh, CELL_leftSideLow, 
                                                            CELL_rightSideHigh, CELL_rightSideLow};

            

            foreach (Point p in cells)
            {
                if (layer[collisionLayer].tile[p.Y, p.X].value == 2)
                {
                    if (e.DeltaY > 0 && (p.Equals(CELL_botleft) || p.Equals(CELL_botright)))
                    {
                        e.Land();
                        e.position.Y = p.Y * Map.tileHeight - e.rect.Height;

                        CELL_topleft = VectorToCell(e.TopLeft);
                        CELL_topright = VectorToCell(e.TopRight);
                        CELL_botleft = VectorToCell(e.BotLeft);
                        CELL_botright = VectorToCell(e.BotRight);
                        CELL_leftSideHigh = VectorToCell(e.LeftSideHigh);
                        CELL_leftSideLow = VectorToCell(e.LeftSideLow);
                        CELL_rightSideHigh = VectorToCell(e.RightSideHigh);
                        CELL_rightSideLow = VectorToCell(e.RightSideLow);
                    }
                    if (p.Equals(CELL_leftSideHigh) || p.Equals(CELL_leftSideLow))
                    {
                        e.DeltaX = 0;
                        e.position.X = (p.X + 1) * Map.tileWidth - e.leftSideHigh.X;

                        CELL_topleft = VectorToCell(e.TopLeft);
                        CELL_topright = VectorToCell(e.TopRight);
                        CELL_botleft = VectorToCell(e.BotLeft);
                        CELL_botright = VectorToCell(e.BotRight);
                        CELL_leftSideHigh = VectorToCell(e.LeftSideHigh);
                        CELL_leftSideLow = VectorToCell(e.LeftSideLow);
                        CELL_rightSideHigh = VectorToCell(e.RightSideHigh);
                        CELL_rightSideLow = VectorToCell(e.RightSideLow);
                    }
                    if (p.Equals(CELL_rightSideHigh) || p.Equals(CELL_rightSideLow))
                    {
                        e.DeltaX = 0;
                        e.position.X = (p.X * Map.tileWidth) - e.rightSideHigh.X;

                        CELL_topleft = VectorToCell(e.TopLeft);
                        CELL_topright = VectorToCell(e.TopRight);
                        CELL_botleft = VectorToCell(e.BotLeft);
                        CELL_botright = VectorToCell(e.BotRight);
                        CELL_leftSideHigh = VectorToCell(e.LeftSideHigh);
                        CELL_leftSideLow = VectorToCell(e.LeftSideLow);
                        CELL_rightSideHigh = VectorToCell(e.RightSideHigh);
                        CELL_rightSideLow = VectorToCell(e.RightSideLow);
                    }
                    else if (p.Equals(CELL_topleft) || p.Equals(CELL_topright))
                    {
                        e.DeltaY = 0;
                        e.position.Y = ((p.Y + 1) * Map.tileHeight) - e.topLeft.Y;
                    }
                }
            }
        }

        public static Point VectorToCell(Vector2 vector)
        {
            return new Point(
                        (int)(vector.X / Map.tileWidth),
                        (int)(vector.Y / Map.tileHeight));
        }

    }
}
