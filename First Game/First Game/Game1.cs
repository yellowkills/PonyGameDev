using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldKeyboardState, currentKeyboardState;
        MouseState oldMouseState, currentMouseState;
        Keys[] keys;
        Texture2D spritesheet;
        Texture2D wht, blk, clr, red;
        Texture2D heroimg, heroLimg, heroRimg, swordimg, swordLimg, swordRimg, enemy1Normalimg, enemy1Damagedimg;
        List<Texture2D> tiles;
        Camera camera;
        Vector2 motion;
        Hero hero;

        bool DEBUG;
        
        Rectangle debug;
        SpriteFont debugFont;
        Vector2 dbug1, dbug2;// dbug3, dbug4, dbug5;

        int[,] map;
        int tileHeight, tileWidth;
        int tileMapWidth;
        int tileMapHeight;
        static int screenWidth;
        static int screenHeight;
        static int mapWidthInPixels;
        static int mapHeightInPixels;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            DEBUG = true;
            camera = new Camera();
            this.IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 1024;
            //graphics.ToggleFullScreen();
        }


        private void ScrollRight()
        {
            motion.X = 1;
        }
        private void ScrollDown()
        {
            motion.Y = 1;
        }
        private void ScrollLeft()
        {
            motion.X = -1;
        }
        private void ScrollUp()
        {
            motion.Y = -1;
        }
        public static int ScreenWidth
        {
            get { return screenWidth; }
        }
        public static int ScreenHeight
        {
            get { return screenHeight; }
        }
        public static int MapWidthInPixels
        {
            get { return mapWidthInPixels; }
        }
        public static int MapHeightInPixels
        {
            get { return mapHeightInPixels; }
        }
        private Point VectorToCell(Vector2 vector)
        {
            return new Point(
                        (int)(vector.X / tileWidth),
                        (int)(vector.Y / tileHeight));
        }
        private Vector2 ViewPortVector()
        {
            return new Vector2(
                    screenWidth + tileWidth,
                    screenHeight + tileHeight);
        }
        public void MarkTile(Point cell)
        {
            spriteBatch.Begin();
            debug.X = tileWidth * cell.X + tileWidth / 2 - 1 - (int)camera.Position.X;
            debug.Y = tileHeight * cell.Y + tileHeight / 2 - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(red, debug, Color.White);
            spriteBatch.End();
        }
        public void MarkTile(Point cell, Color tint)
        {
            spriteBatch.Begin();
            debug.X = tileWidth * cell.X + tileWidth / 2 - 1 - (int)camera.Position.X;
            debug.Y = tileHeight * cell.Y + tileHeight / 2 - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(wht, debug, tint);
            spriteBatch.End();
        }
        Point[] NearbyCells(Hero h)
        {
            List<Point> cells = new List<Point>();
            Point topleft = VectorToCell(new Vector2(hero.position.X-tileMapWidth,hero.position.Y-tileHeight));
            Point bottomright = VectorToCell(new Vector2(hero.position.X + hero.rect.Width + tileMapWidth,
                                                         hero.position.Y + hero.rect.Height + tileHeight));
            for (int i = topleft.Y; i <= bottomright.Y; i++)
                for (int j = topleft.X; j <= bottomright.X; j++)
                    cells.Add(new Point(j,i));

            return cells.ToArray();
        }

        void CollisionTest(Hero h)
        {
            Point[] cells = NearbyCells(h);

            Point top = VectorToCell(h.top);
            Point midleftHIGH = VectorToCell(h.midleftHIGH);
            Point midleftLOW = VectorToCell(h.midleftLOW);
            Point midrightHIGH = VectorToCell(h.midrightHIGH);
            Point midrightLOW = VectorToCell(h.midrightLOW);
            Point botleft = VectorToCell(h.botleft);
            Point botright = VectorToCell(h.botright);


            foreach (Point p in cells)
            {
                if (map[p.Y, p.X] == 1)
                {
                    if (p.Equals(midleftHIGH) || p.Equals(midleftLOW))
                    {
                        h.DeltaX = 0;
                        h.position.X = (p.X + 1) * tileWidth;
                    }
                    if (p.Equals(midrightHIGH) || p.Equals(midrightLOW))
                    {
                        h.DeltaX = 0;
                        h.position.X = (p.X - 1) * tileWidth;
                    }
                    if (h.DeltaY > 0 && (p.Equals(botleft) || p.Equals(botright)))
                    {
                        h.Land();
                        h.position.Y = (p.Y - 2) * tileHeight;
                    }
                    else if (p.Equals(top))
                    {
                        h.DeltaY = 0;
                        h.position.Y = (p.Y+1) * tileHeight;
                    }


                }
            }
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            tileHeight = 32;
            tileWidth = 32;

            tiles = new List<Texture2D>();

            currentKeyboardState = Keyboard.GetState();
            oldKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            oldMouseState = currentMouseState;

            map =   new int[40,40]  {
                                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},  //  01
                                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},  //  02
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  03
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  04
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1},  //  05
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1},  //  06
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1},  //  07
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1},  //  08
                                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1},  //  09
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1},  //  10
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1},  //  11
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1},  //  12
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1},  //  13
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  14
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  15
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  16
                                        {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  17
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  18
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  19
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  20
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  21
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  22
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  23
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  24
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  25
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  26
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  27
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  28
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  29
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  30
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  31
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  32
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  33
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  34
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  35
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  36
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  37
                                        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},  //  38
                                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},  //  39
                                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}   //  40

                                    };

            dbug1 = new Vector2(5, 5);
            dbug2 = new Vector2(5, 25);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            debugFont = Content.Load<SpriteFont>("DebugFont");
            spritesheet = Content.Load<Texture2D>("spritesheet");

            wht = Content.Load<Texture2D>("WhiteBox");
            blk = Content.Load<Texture2D>("BlackBox");
            clr = Content.Load<Texture2D>("ClearBox");
            red = Content.Load<Texture2D>("RedBox");
            heroimg = Content.Load<Texture2D>("Hero");
            heroLimg = Content.Load<Texture2D>("HeroLeft");
            heroRimg = Content.Load<Texture2D>("HeroRight");
            swordimg = Content.Load<Texture2D>("Sword1Normal");
            swordLimg = Content.Load<Texture2D>("Sword1SwingL");
            swordRimg = Content.Load<Texture2D>("Sword1SwingR");
            enemy1Normalimg = Content.Load<Texture2D>("Enemy1Normal");
            enemy1Damagedimg = Content.Load<Texture2D>("Enemy1Damaged");
            
            tiles.Add(wht);
            tiles.Add(blk);
            tiles.Add(clr);
            tiles.Add(red);

            tileMapWidth = map.GetLength(1);
            tileMapHeight = map.GetLength(0);

            mapWidthInPixels = tileMapWidth * tileWidth;
            mapHeightInPixels = tileMapHeight * tileHeight;

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            debug = new Rectangle(0, 0, 3, 3);

            Vector2[] Rframes = new Vector2[4] { new Vector2(0, 32), new Vector2(32, 32), new Vector2(64, 32), new Vector2(96, 32) };
            Vector2[] Lframes = new Vector2[4] { new Vector2(0, 96), new Vector2(32, 96), new Vector2(64, 96), new Vector2(96, 96) };
            Animation walkLeft = new Animation(spritesheet, Lframes, 32, 64, 4, 5);
            Animation walkRight = new Animation(spritesheet, Rframes, 32,64,4,5);
            Vector2 startPos = new Vector2(100, 100);
            hero = new Hero(heroRimg, swordimg, startPos, walkLeft, walkRight);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            keys = currentKeyboardState.GetPressedKeys();
            motion = Vector2.Zero;

            hero.weaponimg = swordimg;

            if (currentMouseState.LeftButton == ButtonState.Pressed &&
               oldMouseState.LeftButton == ButtonState.Released)
            {
                if (hero.direction == Hero.Direction.LEFT)
                    hero.weaponimg = swordLimg;
                else
                    hero.weaponimg = swordRimg;
            }


            foreach (Keys key in keys)
            {
                if(oldKeyboardState.IsKeyUp(key))
                    switch (key)
                    {
                        case Keys.Space:
                            hero.Jump();
                            break;
                        case Keys.OemTilde:
                            DEBUG = DEBUG? false : true;
                            hero.DEBUG = hero.DEBUG ? false : true;
                            break;
                        case Keys.Escape:
                            this.Exit();
                            break;
                    }
                else
                    switch (key)
                    {
                        case Keys.Up:
                            ScrollUp();
                            break;
                        case Keys.Down:
                            ScrollDown();
                            break;
                        case Keys.Left:
                            ScrollLeft();
                            break;
                        case Keys.Right:
                            ScrollRight();
                            break;
                        case Keys.W:
                            hero.MoveUp();
                            break;
                        case Keys.S:
                            hero.MoveDown();
                            break;
                        case Keys.A:
                            hero.MoveLeft();
                            break;
                        case Keys.D:
                            hero.MoveRight();
                            break;
                    }
            }
            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                camera.Position += motion * camera.Speed;
            }

            hero.Update(gameTime);
            CollisionTest(hero);
            

            base.Update(gameTime);
            oldKeyboardState = currentKeyboardState;
            oldMouseState = currentMouseState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawMap();

            hero.Draw(spriteBatch, camera, wht);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            if (DEBUG)
            {
                spriteBatch.DrawString(debugFont, hero.DeltaX.ToString(), dbug1, Color.Red);
                spriteBatch.DrawString(debugFont, hero.DeltaY.ToString(), dbug2, Color.Red);
            }

            spriteBatch.End();

            if (DEBUG)
            {
                Point[] cells = NearbyCells(hero);

                Point top = VectorToCell(hero.top);
                Point midleftHIGH = VectorToCell(hero.midleftHIGH);
                Point midleftLOW = VectorToCell(hero.midleftLOW);
                Point midrightHIGH = VectorToCell(hero.midrightHIGH);
                Point midrightLOW = VectorToCell(hero.midrightLOW);
                Point botleft = VectorToCell(hero.botleft);
                Point botright = VectorToCell(hero.botright);

                foreach (Point p in cells)
                {
                    if (map[p.Y, p.X] == 1 && (p.Equals(botleft) || p.Equals(botright) ||
                                               p.Equals(midleftHIGH) || p.Equals(midleftLOW) ||
                                               p.Equals(midrightHIGH) || p.Equals(midrightLOW) ||
                                               p.Equals(top)))
                        MarkTile(p, Color.Yellow);
                    else
                        MarkTile(p);
                }
            }

            

            base.Draw(gameTime);
        }

        private void DrawMap()
        {
            Point cameraPoint = VectorToCell(camera.Position);
            Point viewPoint = VectorToCell(camera.Position +
                                ViewPortVector());

            Point min = new Point();
            Point max = new Point();

            min.X = cameraPoint.X;
            min.Y = cameraPoint.Y;
            max.X = (int)Math.Min(viewPoint.X, map.GetLength(1));
            max.Y = (int)Math.Min(viewPoint.Y, map.GetLength(0));

            Rectangle tileRectangle = new Rectangle(0, 0, tileWidth, tileHeight);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            for (int y = min.Y; y < max.Y; y++)
            {
                for (int x = min.X; x < max.X; x++)
                {
                    tileRectangle.X = x * tileWidth - (int)camera.Position.X;
                    tileRectangle.Y = y * tileHeight - (int)camera.Position.Y;
                    spriteBatch.Draw(tiles[map[y, x]],
                        tileRectangle,
                        Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}
