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
    class MainMenu : GameScreen 
    {
        // [Private Variables] : none
        private Button button1, button2, button3, button4;
        // [Protected Variables] : none
        // [Public Variables] : none


        // Default Constructor
        public MainMenu(Game1 game, SpriteBatch spriteBatch, TestLevel lvl1, TestLevel lvl2)
            : base(game, spriteBatch)
        {

            button1 = new Button(game, spriteBatch, "LVL 1", new Rectangle(0,0,300,200));
            button1.setButtonDestination(lvl1);
            button2 = new Button(game, spriteBatch, "LVL 2", new Rectangle(0, 0, 300, 200));
            button2.setButtonDestination(lvl2);
            button3 = new Button(game, spriteBatch, "Settings", new Rectangle(0, 0, 300, 200));
            button4 = new Button(game, spriteBatch, "Quit", new Rectangle(0, 0, 300, 200));

            centerButtons(game.Window.ClientBounds.Width,
                          game.Window.ClientBounds.Height);

            Components.Add(button1);
            Components.Add(button2);
            Components.Add(button3);
            Components.Add(button4);

        }


        public void centerButtons(int scrW, int scrH)
        {
            Rectangle area = new Rectangle(0, 0, scrW/2, scrH/2);
            Vector2 buttonPos = centerRectangle(area, button1.buttonArea);
            button1.setButtonPosition(Convert.ToInt32(buttonPos.X), Convert.ToInt32(buttonPos.Y));
            button1.centerText();

            area.X += area.Width + 1;
            buttonPos = centerRectangle(area, button2.buttonArea);
            button2.setButtonPosition(Convert.ToInt32(buttonPos.X), Convert.ToInt32(buttonPos.Y));
            button2.centerText();

            area.X = 0;
            area.Y += area.Height + 1;
            buttonPos = centerRectangle(area, button3.buttonArea);
            button3.setButtonPosition(Convert.ToInt32(buttonPos.X), Convert.ToInt32(buttonPos.Y));
            button3.centerText();

            area.X += area.Width + 1;
            buttonPos = centerRectangle(area, button4.buttonArea);
            button4.setButtonPosition(Convert.ToInt32(buttonPos.X), Convert.ToInt32(buttonPos.Y));
            button4.centerText();
        }

        public Vector2 centerRectangle(Rectangle area, Rectangle rect)
        {
            Vector2 pos = new Vector2();
            pos.X = area.X + (area.Width / 2 - rect.Width / 2);
            pos.Y = area.Y + (area.Height / 2 - rect.Height / 2);

            return pos;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}