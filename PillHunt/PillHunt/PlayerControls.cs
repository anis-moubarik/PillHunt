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

            if (player1.isInputEnabled())

                {

                if (player1.isAI()) //player 1 is AI
                    {
                    player1.getAI().moveAIPlayer(player1);
                    }

                else

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


            if (player2.isInputEnabled())
                {

                if (player2.isAI()) //player 2 is AI
                    {
                    player2.getAI().moveAIPlayer(player2);
                    }

                else

                    {

                    if (keyState.IsKeyDown(Keys.Up) && keyState.IsKeyDown(Keys.Left))
                        {
                        player2.changeBothDirections(float.MinValue, float.MinValue);
                        }

                    else if (keyState.IsKeyDown(Keys.Up) && keyState.IsKeyDown(Keys.Right))
                        {
                        player2.changeBothDirections(float.MaxValue, float.MinValue);
                        }

                    else if (keyState.IsKeyDown(Keys.Down) && keyState.IsKeyDown(Keys.Left))
                        {
                        player2.changeBothDirections(float.MinValue, float.MaxValue);
                        }

                    else if (keyState.IsKeyDown(Keys.Down) && keyState.IsKeyDown(Keys.Right))
                        {
                        player2.changeBothDirections(float.MaxValue, float.MaxValue);
                        }

                    else if (keyState.IsKeyDown(Keys.Up))
                        {
                        player2.changeDirectionY(float.MinValue);
                        }

                    else if (keyState.IsKeyDown(Keys.Down))
                        {
                        player2.changeDirectionY(float.MaxValue);
                        }

                    else if (keyState.IsKeyDown(Keys.Left))
                        {
                        player2.changeDirectionX(float.MinValue);
                        }

                    else if (keyState.IsKeyDown(Keys.Right))
                        {
                        player2.changeDirectionX(float.MaxValue);
                        }

                    }

                }

            }
        }
    }
