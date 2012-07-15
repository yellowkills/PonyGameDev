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
        
        Texture2D enemy1Normalimg, enemy1Damagedimg;

        Texture2D spritesheet_GenericMap, spritesheet_GenericMap_DEBUG, spritesheet_Twilight, spritesheet_ponybot1;
        
        private Camera camera;
        private kbdController keyControls;
        private Map gameMap, gameMap2;
        //private Tiles tiles;
        
        Hero _player;

        public bool DEBUG;
        Rectangle debug;
        SpriteFont debugFont;
        Vector2 dbug1, dbug2;// dbug3, dbug4, dbug5;


        // Various GameScreens
        GameScreen activescreen;
        TestLevel testlvl, battlelvl;

        public static int screenWidth;
        public static int screenHeight;

        public static int ScreenWidth
        {
            get { return screenWidth; }
        }
        public static int ScreenHeight
        {
            get { return screenHeight; }
        }


        // Default Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            DEBUG = false;
            
            this.IsMouseVisible = true;
            

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 1024;
            //graphics.ToggleFullScreen();
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
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            debugFont = Content.Load<SpriteFont>("DebugFont");
            debug = new Rectangle(0, 0, 3, 3);




            /*Move all this to a function in the Player/Entity class?*/
            enemy1Normalimg = Content.Load<Texture2D>("Enemy1Normal");
            enemy1Damagedimg = Content.Load<Texture2D>("Enemy1Damaged");

            // Sprite sheets
            spritesheet_GenericMap = Content.Load<Texture2D>("spritesheet_map_genericTiles");
            spritesheet_GenericMap_DEBUG = Content.Load<Texture2D>("spritesheet_map_genericTiles_DEBUG");
            spritesheet_Twilight = Content.Load<Texture2D>("spritesheet_Twilight"); // TWILIGHT!
            spritesheet_ponybot1 = Content.Load<Texture2D>("spritesheet_ponybot1"); // ROBOTS!
            




            Vector2 startPos = new Vector2(100, 100);

            camera = new Camera(this);
            _player = new Hero(this, spriteBatch, camera, gameMap,startPos, 8, spritesheet_Twilight);
            camera.lockEntity(_player);
            keyControls = new kbdController(this, _player);            

            // General test lvl
            testlvl = new TestLevel(this, spriteBatch);
            testlvl.LoadHero(_player);
            testlvl.LoadMap(camera, spritesheet_GenericMap);

            // enemy AI testing lvl
            battlelvl = new TestLevel(this, spriteBatch);
            battlelvl.LoadHero(_player);
            battlelvl.LoadMap(camera, spritesheet_GenericMap);



            Components.Add(testlvl);
            //Components.Add(battlelvl);

            //activescreen = battlelvl;
            activescreen = testlvl;
            testlvl.Show();
        }

        public void toggleDebug()
        {
            DEBUG = !DEBUG;
            _player.DEBUG = DEBUG;

            if (DEBUG)
                gameMap.setSpriteSheet(spritesheet_GenericMap_DEBUG);
            else
                gameMap.setSpriteSheet(spritesheet_GenericMap);
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

            keyControls.kbdUpdate();
            camera.Update();

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

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);

            if (DEBUG)
            {
                spriteBatch.DrawString(debugFont, _player.DeltaX.ToString(), dbug1, Color.Red);
                spriteBatch.DrawString(debugFont, _player.DeltaY.ToString(), dbug2, Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
