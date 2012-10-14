using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WhenRobotsAttack
{
    public class kbdController
    {
        /**
         * 
         * TODO: make into a component. Then the player object will add it
         * 
         **/


        private GameManager gamePtr;
        private Hero playerPtr;

        KeyboardState oldKeyboardState, currentKeyboardState;
        MouseState oldMouseState, currentMouseState;
        Keys[] keys;

        public kbdController(GameManager mainGame, Hero inPlayer) // TODO: change hero to player
        {
            gamePtr = mainGame;
            playerPtr = inPlayer;
        }

        public void kbdUpdate()
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            keys = currentKeyboardState.GetPressedKeys();

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
                                //gamePtr.toggleDebug();
                                break;
                            case Keys.Insert:
                                playerPtr.testHealing();
                                break;
                            case Keys.Delete:
                                playerPtr.takeDamage();
                                break;
                            case Keys.Escape:
                                //gamePtr.pauseGame();
                                break;
                        }
                }
                else
                {
                    switch (key)
                    {
                        
                        case Keys.Up:
                            gamePtr.camera.ScrollUp();
                            break;
                        case Keys.Down:
                            gamePtr.camera.ScrollDown();
                            break;
                        case Keys.Left:
                            gamePtr.camera.ScrollLeft();
                            break;
                        case Keys.Right:
                            gamePtr.camera.ScrollRight();
                            break;
                         
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
