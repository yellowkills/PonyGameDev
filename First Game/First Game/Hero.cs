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
    class Hero : Entity
    {
        public Texture2D img;
        public Rectangle pxlrect;
        public bool DEBUG = true;
        const int heroHeight = 64;
        const int heroWidth = 32;
        bool inAir;

        public Hero(Game game, SpriteBatch spriteBatch, Camera camera,Vector2 position, Texture2D img)
            : base(game, spriteBatch, camera)
        {
            this.position = position;
            this.img = img;

            xAcceleration = .3f;
            yAcceleration = .5f;
            xDeceleration = .7f;
            xDeceleration2 = .98f;
            gravity = .4f;
            jumpforce = -20.0f;
            maxSpeedX = 6.5f;
            maxSpeedY = 5.0f;


            direction = Direction.RIGHT;
            state = State.STANDING;


            top = new Vector2(position.X + rect.Width / 2, position.Y);
            botleft = new Vector2(position.X, position.Y + rect.Height);
            botright = new Vector2(position.X + rect.Width, position.Y + rect.Height);
            midleftHIGH = new Vector2(position.X, position.Y + rect.Height * (4.0f / 10.0f));
            midleftLOW = new Vector2(position.X, position.Y + rect.Height * (9.0f / 10.0f));
            midrightHIGH = new Vector2(position.X + rect.Width, position.Y + rect.Height * (4.0f / 10.0f));
            midrightLOW = new Vector2(position.X + rect.Width, position.Y + rect.Height * (9.0f / 10.0f));


            rect = new Rectangle((int)position.X, (int)position.Y, heroWidth, heroHeight);
            pxlrect = new Rectangle(0, 0, 3, 3);
            inAir = true;
            deltaX = 0.0f;
            deltaY = 0.0f;
        }

        void RefreshPosition()
        {
            position.X += DeltaX;
            position.Y += DeltaY;
        

            top.X = position.X + rect.Width / 2;
            top.Y = position.Y;

            botleft.X = position.X + 7;
            botleft.Y = position.Y + rect.Height;

            botright.X = position.X + rect.Width - 7;
            botright.Y = position.Y + rect.Height;

            midleftHIGH.X = position.X;
            midleftHIGH.Y = position.Y + rect.Height * (4.0f / 10.0f);

            midleftLOW.X = position.X;
            midleftLOW.Y = position.Y + rect.Height * (9.0f / 10.0f) - 4;

            midrightHIGH.X = position.X + rect.Width;
            midrightHIGH.Y = position.Y + rect.Height * (4.0f / 10.0f);

            midrightLOW.X = position.X + rect.Width;
            midrightLOW.Y = position.Y + rect.Height * (9.0f / 10.0f) - 4;
        }
        
        void drawCollisionPoints(SpriteBatch spriteBatch, Camera camera,Texture2D pxl)
        {
            spriteBatch.Begin();

            pxlrect.X = (int)top.X - 1 - (int)camera.Position.X;
            pxlrect.Y = (int)top.Y - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(pxl, pxlrect, Color.Lime);

            pxlrect.X = (int)botleft.X - 1 - (int)camera.Position.X;
            pxlrect.Y = (int)botleft.Y - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(pxl, pxlrect, Color.Lime);

            pxlrect.X = (int)botright.X - 1 - (int)camera.Position.X;
            pxlrect.Y = (int)botright.Y - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(pxl, pxlrect, Color.Lime);

            pxlrect.X = (int)midleftHIGH.X - 1 - (int)camera.Position.X;
            pxlrect.Y = (int)midleftHIGH.Y - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(pxl, pxlrect, Color.Lime);

            pxlrect.X = (int)midleftLOW.X - 1 - (int)camera.Position.X;
            pxlrect.Y = (int)midleftLOW.Y - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(pxl, pxlrect, Color.Lime);

            pxlrect.X = (int)midrightHIGH.X - 1 - (int)camera.Position.X;
            pxlrect.Y = (int)midrightHIGH.Y - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(pxl, pxlrect, Color.Lime);

            pxlrect.X = (int)midrightLOW.X - 1 - (int)camera.Position.X;
            pxlrect.Y = (int)midrightLOW.Y - 1 - (int)camera.Position.Y;
            spriteBatch.Draw(pxl, pxlrect, Color.Lime);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                if(inAir == false)
                    DeltaX *= xDeceleration;
                else
                    DeltaX *= xDeceleration2;
                if (Math.Abs(DeltaX) < 0.1f) DeltaX = 0;
            }
            DeltaY += gravity;

            state = (Math.Abs(DeltaX) < 1) ? State.STANDING : State.RUNNING;

            RefreshPosition();
        }

        public override void Draw(GameTime gameTime)
        {
            rect.X = (int)position.X - (int)camera.Position.X;
            rect.Y = (int)position.Y - (int)camera.Position.Y;


            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            spriteBatch.Draw(img,rect,Color.White);
            spriteBatch.End();


            /*
            if (DEBUG)
            {
                Point[] cells = Tiles.NearbyCells(map);

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

                drawCollisionPoints(spriteBatch, camera, pxl);
            }*/
        }
    }
}
