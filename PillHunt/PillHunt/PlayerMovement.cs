using System;
using Microsoft.Xna.Framework.Input;

namespace PillHunt
    {
    class PlayerMovement
        {

        //creates a new player movement
        public PlayerMovement()
            {
            }

        //moves both players according to keyboard's current state
        public void moveBothPlayers(KeyboardState keyState, Player player1, Player player2)
            {

            if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.A))
                {
                player1.moveUpAndLeft();
                }

            else if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.D))
                {
                player1.moveUpAndRight();
                }

            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.A))
                {
                player1.moveDownAndLeft();
                }

            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.D))
                {
                player1.moveDownAndRight();
                }

            else if (keyState.IsKeyDown(Keys.W))
                {
                player1.moveUp();
                }

            else if (keyState.IsKeyDown(Keys.S))
                {
                player1.moveDown();
                }

            else if (keyState.IsKeyDown(Keys.A))
                {
                player1.moveLeft();
                }

            else if (keyState.IsKeyDown(Keys.D))
                {
                player1.moveRight();
                }

            }
        }
    }
