using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spritesheet_GenericMap, spritesheet_GenericMap_DEBUG, spritesheet_Twilight, spritesheet_ponybot1;
        
        private Camera camera;
        private kbdController keyControls;
        Player player;

        public bool DEBUG;
        Rectangle debug;
        SpriteFont debugFont;
        Vector2 dbug1, dbug2;// dbug3, dbug4, dbug5;


        // Various GameScreens
        GameScreen activescreen;
        MainMenu mainMenu;
        testLevel_1 testlvl;
        testLevel_2 battlelvl;

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

        public void toggleDebug()
        {
            DEBUG = !DEBUG;
            testlvl.toggleDebug();
            battlelvl.toggleDebug();
        }

        public void switchScreens(GameScreen newScreen)
        {
            activescreen.Hide();
            activescreen = newScreen;
            activescreen.Show();
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

            // Debug
            debugFont = Content.Load<SpriteFont>("DebugFont");
            debug = new Rectangle(0, 0, 3, 3);


            // Sprite sheets
            spritesheet_GenericMap = Content.Load<Texture2D>("spritesheet_map_genericTiles");
            spritesheet_GenericMap_DEBUG = Content.Load<Texture2D>("spritesheet_map_genericTiles_DEBUG");
            spritesheet_Twilight = Content.Load<Texture2D>("spritesheet_Twilight"); // TWILIGHT!
            spritesheet_ponybot1 = Content.Load<Texture2D>("spritesheet_ponybot1"); // ROBOTS!



            // This is where the player starts. This will be removed once the player spawn block is implemented
            Vector2 startPos = new Vector2(100, 100);

            
            camera = new Camera(this);

            // Player Creation
            Hero[] heroes = new Hero[] { new Hero(this, spriteBatch, camera, startPos, 8, spritesheet_Twilight) };
            player = new Player(this, spriteBatch, camera, startPos);
            player.setHeroesPlayable(heroes);
            camera.lockEntity(player.activeHero); // TODO: Change lockEntity so that it locks onto a player instead of a hero
            keyControls = new kbdController(this, player.activeHero);
            

            // Test Level 1. Used for general testing
            testlvl = new testLevel_1(this, spriteBatch);
            testlvl.Hide();

            // Test Level 2. Used for enemy AI testing
            battlelvl = new testLevel_2(this, spriteBatch);
            battlelvl.Hide();

            // Main Menu
            mainMenu = new MainMenu(this, spriteBatch,testlvl,battlelvl);
            mainMenu.Hide();


            Components.Add(mainMenu);
            Components.Add(testlvl);
            Components.Add(battlelvl);

            activescreen = mainMenu;
            activescreen.Show();
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
                spriteBatch.DrawString(debugFont, player.activeHero.DeltaX.ToString(), dbug1, Color.Red);
                spriteBatch.DrawString(debugFont, player.activeHero.DeltaY.ToString(), dbug2, Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
