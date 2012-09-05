using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace First_Game
{
    class kbdController
    {
        private GameManager gamePtr;
        private Hero playerPtr;

        KeyboardState oldKeyboardState, currentKeyboardState;
        MouseState oldMouseState, currentMouseState;
        Keys[] keys;

        public kbdController(GameManager mainGame, Hero inPlayer)
        {
            gamePtr = mainGame;
            playerPtr = inPlayer;
        }

        public void kbdUpdate()
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            keys = currentKeyboardState.GetPressedKeys();

            /*
             * 
            if (currentMouseState.LeftButton == ButtonState.Pressed &&
               oldMouseState.LeftButton == ButtonState.Released)
            {
                if (playerPtr.direction == Player.Direction.LEFT)
                    playerPtr.weaponimg = swordLimg;
                else
                    playerPtr.weaponimg = swordRimg;
            }
             * 
             * Unused right now.
             */

            foreach (Keys key in keys)
            {
                if (oldKeyboardState.IsKeyUp(key))
                {
                    if (gamePtr.activescreen is Level)
                        switch (key)
                        {
                            case Keys.Space:
                                playerPtr.Jump();
                                break;
                            case Keys.OemTilde:
                                gamePtr.toggleDebug();
                                break;
                            case Keys.Insert:
                                playerPtr.testHealing();
                                break;
                            case Keys.Delete:
                                playerPtr.takeDamage();
                                break;
                            case Keys.Escape:
                                gamePtr.pauseGame();
                                break;
                        }
                }
                else
                {
                    switch (key)
                    {
                        /* 
                         * 
                        case Keys.Up:
                            camera.ScrollUp();
                            break;
                        case Keys.Down:
                            camera.ScrollDown();
                            break;
                        case Keys.Left:
                            camera.ScrollLeft();
                            break;
                        case Keys.Right:
                            camera.ScrollRight();
                            break;
                         * 
                         * Disabling keyboard control of the Camera while working on Camera AI
                         */
                        case Keys.W:
                            playerPtr.MoveUp();
                            break;
                        case Keys.S:
                            playerPtr.MoveDown();
                            break;
                        case Keys.A:
                            playerPtr.MoveLeft();
                            break;
                        case Keys.D:
                            playerPtr.MoveRight();
                            break;
                    }//End Switch
                }//End Else
            }//End ForEach key
        }//end kbdUpdate

        public void storeStates()
        {
            oldKeyboardState = currentKeyboardState;
            oldMouseState = currentMouseState;
        }
    }
}
