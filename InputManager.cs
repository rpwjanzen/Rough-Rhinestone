
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace ParticlesScreenSaver
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public partial class InputManager : Microsoft.Xna.Framework.GameComponent
    {

        #region Class Members

        KeyboardState originalKBState;      //represents the keyboard state at program start
        Keys[] originalKeys;                //represents the keys that were pressed at program start
        MouseState originalMouseState;      //represents the mouse at program stard

        #endregion


        public InputManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here


            originalKBState = Keyboard.GetState();              //get the keyboard state at program start
            originalKeys = originalKBState.GetPressedKeys();      //find the pressed keys at program start

            originalMouseState = Mouse.GetState();       //initialize the mouse state


        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            updateKeyboard();       //checks to see if the keyboard has changed since the program started
            updateMouse();          //checks to see if the mouse has changed since the program started

            base.Update(gameTime);
        }



        /// <summary>
        /// This function compares the keyboard's initial state to its current state
        /// and exits the program if the keyboard state has changed
        /// </summary>
        private void updateKeyboard()
        {

            //get the current keyboard state
            KeyboardState currentKBState = Keyboard.GetState();

            //get the array of the keys that are down now
            Keys[] currentKeys = currentKBState.GetPressedKeys();


            //iterate through every key that's down now, and exit
            //if a key wasn't pressed when the program started
            foreach (Keys newKey in currentKeys)
            {
                if (!originalKBState.IsKeyDown(newKey))
                    Game.Exit();
            }


            //iterate through every key that was down when the program
            //started, and exit if it's not still pressed now
            foreach (Keys originalKey in originalKeys)
            {
                if (!currentKBState.IsKeyDown(originalKey))
                    Game.Exit();
            }

        }


        /// <summary>
        /// this function compares the mouse's initial state to its current state,
        /// and exits the program if the mouse state has changed
        /// </summary>
        private void updateMouse()
        {

            //get the mouse's current state
            MouseState currentMouseState = Mouse.GetState();


            //check to see if the mouse state has changed at all,
            //and exit the game if it has
            if ((originalMouseState.LeftButton != currentMouseState.LeftButton) ||
                 (originalMouseState.MiddleButton != currentMouseState.MiddleButton) ||
                 (originalMouseState.RightButton != currentMouseState.RightButton) ||
                 (originalMouseState.XButton1 != currentMouseState.XButton1) ||
                 (originalMouseState.XButton2 != currentMouseState.XButton2) ||
                 (originalMouseState.ScrollWheelValue != currentMouseState.ScrollWheelValue) ||
                 (originalMouseState.X != currentMouseState.X) ||
                 (originalMouseState.Y != currentMouseState.Y)
               )
            {
                Game.Exit();
            }


        }
    }
}


