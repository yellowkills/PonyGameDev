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

namespace WhenRobotsAttack
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameManager : Microsoft.Xna.Framework.Game
    {
        #region Class Variables

        // Debug
        protected bool DEBUG = false;

        // Current working directory
        public static string path = Directory.GetCurrentDirectory();

        // SpriteBatch and Camera objects.
        public SpriteBatch spriteBatch;
        public Camera camera;

        // Graphics
        protected GraphicsDeviceManager graphics;

        // Keyboard Input
        public kbdController keyControls;

        // Player and Level objects
        public Player player;
        public Level currentLevel;

        // Textures
        public Texture2D spritesheet_GenericMap, spritesheet_GenericMap_DEBUG, spritesheet_Twilight, spritesheet_ponybot1;

        // Various GameScreens
        public GameScreen activescreen;

        // Screen dimentions
        public static int screenWidth;
        public static int screenHeight;

        // Debug vars
        Rectangle debug;
        SpriteFont debugFont;
        Vector2 dbug1, dbug2;

        #endregion

        // Default Constructor
        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            
            // Gets the path for WhenRobotsAttack
            int index = path.LastIndexOf("WhenRobotsAttack") + "WhenRobotsAttack".Length;
            path = path.Substring(0,index) + "\\";
            

            this.IsMouseVisible = true;


            //graphics.PreferredBackBufferWidth = 600;
            //graphics.PreferredBackBufferHeight = 400;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 1024;
            //graphics.ToggleFullScreen();
        }

        public void toggleDebug()
        {
            if(activescreen is Level) currentLevel.toggleDebug();
            //if(activescreen is Menu) currentMenu.toggleDebug();
        }

        // Switches the active screen to 'newScreen'
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


            /*~~~~~~~~~~~~~~~~~*/

            camera = new Camera(this);

            player = new Player(this);

            currentLevel = new Level(this, player.currentLevel);

            keyControls = new kbdController(this, player.activeHero);

            Components.Add(currentLevel);


            activescreen = currentLevel;
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
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
