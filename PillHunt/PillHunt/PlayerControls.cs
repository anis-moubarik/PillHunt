using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace PillHunt
    {
    class PlayerControls
        {

        public PlayerControls()
            {
            }

        //changes directions of both players according to keyboard's current state
        public void checkKeyboardStatus(KeyboardState keyState, Player player1, Player player2)
            {

            if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.A))
                {
                player1.changeBothDirections(float.MinValue, float.MinValue);
                }

            else if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.D))
                {
                player1.changeBothDirections(float.MaxValue, float.MinValue);
                }

            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.A))
                {
                player1.changeBothDirections(float.MinValue, float.MaxValue);
                }

            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.D))
                {
                player1.changeBothDirections(float.MaxValue, float.MaxValue);
                }

            else if (keyState.IsKeyDown(Keys.W))
                {
                player1.changeDirectionY(float.MinValue);
                }

            else if (keyState.IsKeyDown(Keys.S))
                {
                player1.changeDirectionY(float.MaxValue);
                }

            else if (keyState.IsKeyDown(Keys.A))
                {
                player1.changeDirectionX(float.MinValue);
                }

            else if (keyState.IsKeyDown(Keys.D))
                {
                player1.changeDirectionX(float.MaxValue);
                }

            }
        }
    }
