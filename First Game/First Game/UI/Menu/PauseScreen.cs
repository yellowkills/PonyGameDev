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
    class PauseScreen : GameScreen
    {
        Button resume, quit;


        // Default Constructor
        public PauseScreen(GameManager game, SpriteBatch spriteBatch, KillScreen killScreen)
            : base(game, spriteBatch)
        {
            resume = new Button(game, spriteBatch, "Resume", new Rectangle(0, 0, 300, 200));
            quit = new Button(game, spriteBatch, "Quit", new Rectangle(0, 0, 300, 200));
            quit.setButtonDestination(killScreen);

            centerButtons(game.Window.ClientBounds.Width,
                          game.Window.ClientBounds.Height);

            Components.Add(resume);
            Components.Add(quit);
        }

        public void setLevel(Level lvl)
        {
            resume.setButtonDestination(lvl);
        }

        public void centerButtons(int scrW, int scrH)
        {
            Rectangle area = new Rectangle(0, 0, scrW, scrH / 2);
            Vector2 buttonPos = centerRectangle(area, resume.buttonArea);
            resume.setButtonPosition(Convert.ToInt32(buttonPos.X), Convert.ToInt32(buttonPos.Y));
            resume.centerText();

            area.Y += area.Height + 1;
            buttonPos = centerRectangle(area, quit.buttonArea);
            quit.setButtonPosition(Convert.ToInt32(buttonPos.X), Convert.ToInt32(buttonPos.Y));
            quit.centerText();
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