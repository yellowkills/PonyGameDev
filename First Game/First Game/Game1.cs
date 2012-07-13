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
        Texture2D spritesheet;
        
        Texture2D heroimg, heroLimg, heroRimg, swordimg, swordLimg, swordRimg, enemy1Normalimg, enemy1Damagedimg;
        
        private Camera camera;
        private kbdController keyControls;
        private Map gameMap;
        private Tiles tiles;
        
        Hero hero;

        //Will make a function to control this eventually. Public for now.
        public bool DEBUG;
        
        Rectangle debug;
        SpriteFont debugFont;
        Vector2 dbug1, dbug2;// dbug3, dbug4, dbug5;

        public static int screenWidth;
        public static int screenHeight;
        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            DEBUG = true;
            
            this.IsMouseVisible = true;
            

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 1024;
            //graphics.ToggleFullScreen();
        }


        
        public static int ScreenWidth
        {
            get { return screenWidth; }
        }
        public static int ScreenHeight
        {
            get { return screenHeight; }
        }
        
       
        public void MarkTile(Point cell)
        {
            spriteBatch.Begin();
            debug.X = Tiles.tileWidth * cell.X + Tiles.tileWidth / 2 - 1 - (int)camera.Position.X;
            debug.Y = Tiles.tileHeight * cell.Y + Tiles.tileHeight / 2 - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(tiles.red, debug, Color.White);
            spriteBatch.End();
        }
        public void MarkTile(Point cell, Color tint)
        {
            spriteBatch.Begin();
            debug.X = Tiles.tileWidth * cell.X + Tiles.tileWidth / 2 - 1 - (int)camera.Position.X;
            debug.Y = Tiles.tileHeight * cell.Y + Tiles.tileHeight / 2 - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(tiles.wht, debug, tint);
            spriteBatch.End();
        }
        

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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

            /*Move all this to a function in the Hero/Entity class?*/
            heroimg = Content.Load<Texture2D>("Hero");
            heroLimg = Content.Load<Texture2D>("HeroLeft");
            heroRimg = Content.Load<Texture2D>("HeroRight");
            swordimg = Content.Load<Texture2D>("Sword1Normal");
            swordLimg = Content.Load<Texture2D>("Sword1SwingL");
            swordRimg = Content.Load<Texture2D>("Sword1SwingR");
            enemy1Normalimg = Content.Load<Texture2D>("Enemy1Normal");
            enemy1Damagedimg = Content.Load<Texture2D>("Enemy1Damaged");
            
            

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            debug = new Rectangle(0, 0, 3, 3);

            Vector2[] Rframes = new Vector2[4] { new Vector2(0, 32), new Vector2(32, 32), new Vector2(64, 32), new Vector2(96, 32) };
            Vector2[] Lframes = new Vector2[4] { new Vector2(0, 96), new Vector2(32, 96), new Vector2(64, 96), new Vector2(96, 96) };
            Animation walkLeft = new Animation(spritesheet, Lframes, 32, 64, 4, 5);
            Animation walkRight = new Animation(spritesheet, Rframes, 32,64,4,5);
            Vector2 startPos = new Vector2(100, 100);
            hero = new Hero(heroRimg, swordimg, startPos, walkLeft, walkRight);
            camera = new Camera(hero,this);
            keyControls = new kbdController(this, hero);
            tiles = new Tiles(this);
            gameMap = new Map(this, hero, camera, tiles);

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

            camera.resetMotion();

            hero.weaponimg = swordimg;


            keyControls.kbdUpdate();
            camera.Update();

            hero.Update(gameTime);
            CollisionTest(hero);
            

            base.Update(gameTime);
            keyControls.storeStates();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gameMap.DrawMap(spriteBatch);

            hero.Draw(spriteBatch, camera, tiles.wht);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);

            if (DEBUG)
            {
                spriteBatch.DrawString(debugFont, hero.DeltaX.ToString(), dbug1, Color.Red);
                spriteBatch.DrawString(debugFont, hero.DeltaY.ToString(), dbug2, Color.Red);
            }

            spriteBatch.End();

            if (DEBUG)
            {
                Point[] cells = Tiles.NearbyCells(hero);

                Point top = Tiles.VectorToCell(hero.top);
                Point midleftHIGH = Tiles.VectorToCell(hero.midleftHIGH);
                Point midleftLOW = Tiles.VectorToCell(hero.midleftLOW);
                Point midrightHIGH = Tiles.VectorToCell(hero.midrightHIGH);
                Point midrightLOW = Tiles.VectorToCell(hero.midrightLOW);
                Point botleft = Tiles.VectorToCell(hero.botleft);
                Point botright = Tiles.VectorToCell(hero.botright);

                foreach (Point p in cells)
                {
                    if (gameMap.map[p.Y, p.X] == 1 && (p.Equals(botleft) || p.Equals(botright) ||
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


        //Should be moved to Hero.
        void CollisionTest(Hero h)
        {
            Point[] cells = Tiles.NearbyCells(h);

            Point top = Tiles.VectorToCell(h.top);
            Point midleftHIGH = Tiles.VectorToCell(h.midleftHIGH);
            Point midleftLOW = Tiles.VectorToCell(h.midleftLOW);
            Point midrightHIGH = Tiles.VectorToCell(h.midrightHIGH);
            Point midrightLOW = Tiles.VectorToCell(h.midrightLOW);
            Point botleft = Tiles.VectorToCell(h.botleft);
            Point botright = Tiles.VectorToCell(h.botright);


            foreach (Point p in cells)
            {
                if (gameMap.map[p.Y, p.X] == 1)
                {
                    if (p.Equals(midleftHIGH) || p.Equals(midleftLOW))
                    {
                        h.DeltaX = 0;
                        h.position.X = (p.X + 1) * Tiles.tileWidth;
                    }
                    if (p.Equals(midrightHIGH) || p.Equals(midrightLOW))
                    {
                        h.DeltaX = 0;
                        h.position.X = (p.X - 1) * Tiles.tileWidth;
                    }
                    if (h.DeltaY > 0 && (p.Equals(botleft) || p.Equals(botright)))
                    {
                        h.Land();
                        h.position.Y = (p.Y - 2) * Tiles.tileHeight;
                    }
                    else if (p.Equals(top))
                    {
                        h.DeltaY = 0;
                        h.position.Y = (p.Y + 1) * Tiles.tileHeight;
                    }


                }
            }
        }
        
    }
}
