using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PillHunt
    {
    class PlayerMovement
        {

        int screenWidth;
        int screenHeight;
        int textureWidth;
        int textureHeight;
        bool playerOneMoved;
        bool playerTwoMoved;

        //creates a new player movement, saves screen and texture sizes
        public PlayerMovement(int swidth, int sheight, int twidth, int theight)
            {
            screenWidth = swidth;
            screenHeight = sheight;
            textureWidth = twidth;
            textureHeight = theight;
            }

        //moves both players according to keyboard's current state
        public void moveBothPlayers(KeyboardState keyState, Player player1, Player player2, Map map)
            {

            //players move faster if keys are pressed, otherwise they slow down
            playerOneMoved = false;
            playerTwoMoved = false;



            if (map.intersectsWithAWall(player1.getPosition()))
            {
                player1.bounceXY();
            }
            else
            {
                //player 1 movement
                if (keyState.IsKeyDown(Keys.W))
                {
                    player1.changeSpeedY(-2);
                    playerOneMoved = true;
                }
                if (keyState.IsKeyDown(Keys.S))
                {
                    player1.changeSpeedY(2);
                    playerOneMoved = true;
                }
                if (keyState.IsKeyDown(Keys.A))
                {
                    player1.changeSpeedX(-2);
                    playerOneMoved = true;
                }
                if (keyState.IsKeyDown(Keys.D))
                {
                    player1.changeSpeedX(2);
                    playerOneMoved = true;
                }

                if (!playerOneMoved)
                {
                    player1.slowDown();
                }
            }

            //player 2 movement
            if (keyState.IsKeyDown(Keys.Up))
                {
                player2.changeSpeedY(-2);
                playerTwoMoved = true;
                }
            if (keyState.IsKeyDown(Keys.Down))
                {
                player2.changeSpeedY(2);
                playerTwoMoved = true;
                }
            if (keyState.IsKeyDown(Keys.Left))
                {
                player2.changeSpeedX(-2);
                playerTwoMoved = true;
                }
            if (keyState.IsKeyDown(Keys.Right))
                {
                player2.changeSpeedX(2);
                playerTwoMoved = true;
                }

            //possible slow downs
            if (!playerTwoMoved)
                {
                player2.slowDown();
                }

            //if players intersect with each other, they bounce away
            if (player1.getPosition().Intersects(player2.getPosition()))
                {
                player1.bounceX();
                player1.bounceY();
                player2.bounceX();
                player2.bounceY();
                }

            player1.move(screenWidth - textureWidth, screenHeight - textureHeight, 0, 0);
            player2.move(screenWidth - textureWidth, screenHeight - textureHeight, 0, 0);

            }

        }
    }
