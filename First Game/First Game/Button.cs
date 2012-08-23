using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Button : DrawableGameComponent
    {
        // [Private Variables]
        private enum BState { HOVER, UP, DOWN}
        private BState buttonState;
        private Texture2D graphic;
        private String text;
        private Vector2 textPos;
        private SpriteFont buttonFont;
        private Color hoverColor = Color.Yellow, clickColor = Color.LightGoldenrodYellow;
        private MouseState mouseState;
        private SpriteBatch spriteBatch;
        private GameScreen goToScreen;
        private GameManager game;


        // [Protected Variables] : none
        // [Public Variables]
        public Rectangle buttonArea;

        // Default Constructor
        public Button(GameManager game, SpriteBatch spriteBatch, String text, Rectangle buttonArea)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.graphic = game.Content.Load<Texture2D>("whtpxl");
            this.text = text;
            this.buttonArea = buttonArea;
            this.buttonState = BState.UP;

            buttonFont = game.Content.Load<SpriteFont>("Font1");
            textPos = new Vector2(0,0);
        }

        public Button(GameManager game, SpriteBatch spriteBatch, Texture2D graphic, String text, Rectangle buttonArea)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.graphic = graphic;
            this.text = text;
            this.buttonArea = buttonArea;
            this.buttonState = BState.UP;

            buttonFont = game.Content.Load<SpriteFont>("Font1");
            textPos = new Vector2(0,0);
        }

        public void setButtonDestination(GameScreen goToScreen)
        {
            this.goToScreen = goToScreen;
        }

        public void setButtonPosition(int x, int y)
        {
            buttonArea.X = x;
            buttonArea.Y = y;
        }
        public void centerText()
        {
            textPos.X = buttonArea.X + (buttonArea.Width / 2 - buttonFont.MeasureString(text).X / 2);
            textPos.Y = buttonArea.Y + (buttonArea.Height / 2 - buttonFont.LineSpacing / 2);
        }

        public void checkMouse()
        {
            mouseState = Mouse.GetState();

            if ((mouseState.X >= buttonArea.Left && mouseState.X <= buttonArea.Right) &&
               (mouseState.Y >= buttonArea.Top && mouseState.Y <= buttonArea.Bottom))
            {
                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    buttonState = BState.DOWN;
                    game.switchScreens(goToScreen);
                }
                else
                    buttonState = BState.HOVER;
            }
            else
                buttonState = BState.UP;
        }
        private Color buttonTint(BState b)
        {
            switch (b)
            {
                case BState.HOVER:
                    return hoverColor;
                case BState.DOWN:
                    return clickColor;
                default:
                    return Color.White;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            checkMouse();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            spriteBatch.Draw(graphic, buttonArea, buttonTint(buttonState));
            spriteBatch.DrawString(buttonFont,text, textPos,Color.Black);
            spriteBatch.End();
        }
    }
}