using System;
using Microsoft.Xna.Framework.Input;

namespace PillHunt
{
    class PlayerMovement
    {

        //creates a new player movement, saves screen and texture sizes
        public PlayerMovement()
        {
        }

        //moves both players according to keyboard's current state
        public void moveBothPlayers(KeyboardState keyState, Player player1, Player player2, Map map)
        {

            //player 1 moving up and left
            if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.A))
            {
                
                player1.moveUp();
                player1.moveLeft();

                if (map.intersectsWithAWall(player1.getPosition()))
                {
                    player1.bounceDown();
                    player1.bounceLeft();
                }

            }

            //player 1 moving up and right
            else if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.D))
            {

                player1.moveUp();
                player1.moveRight();

                if (map.intersectsWithAWall(player1.getPosition()))
                {
                    player1.bounceDown();
                    player1.bounceRight();
                }

            }

            //player 1 moving down and left
            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.A))
            {

                player1.moveDown();
                player1.moveLeft();

                if (map.intersectsWithAWall(player1.getPosition()))
                {
                    player1.bounceDown();
                    player1.bounceLeft();
                }

            }

            //player 1 moving down and right
            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.D))
            {

                player1.moveDown();
                player1.moveRight();

                if (map.intersectsWithAWall(player1.getPosition()))
                {
                    player1.bounceDown();
                    player1.bounceRight();
                }

            }
            

            //player 1 is moving up
            else if (keyState.IsKeyDown(Keys.W))
            {

                player1.moveUp();

                if (map.intersectsWithAWall(player1.getPosition()))
                {
                    player1.bounceDown();
                }

            }

            //player 1 is moving down
            else if (keyState.IsKeyDown(Keys.S))
            {

                player1.moveDown();

                if (map.intersectsWithAWall(player1.getPosition()))
                {
                    player1.bounceUp();
                }

            }


            //player 1 is moving left
            else if (keyState.IsKeyDown(Keys.A))
            {

                player1.moveLeft();

                if (map.intersectsWithAWall(player1.getPosition()))
                {
                    player1.bounceRight();
                }

            }


            //player 1 is moving right
            else if (keyState.IsKeyDown(Keys.D))
            {

                player1.moveRight();

                if (map.intersectsWithAWall(player1.getPosition()))
                {
                    player1.bounceLeft();
                }

            }

        }
    }
}
