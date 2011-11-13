using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
    {
    class Player
        {

        private int score;
        private int speedX;
        private int speedY;
        private Rectangle position;


        public Player()
            {
            score = 0;
            position = new Rectangle(0, 0, 32, 32);
            }

        public void changeSpeedX(int change)
            {
            speedX += change;
            }

        public void changeSpeedY(int change)
            {
            speedY += change;
            }

        //checks which way the player is currently going and decreases his speed by 1
        public void slowDown()
            {

            if (speedX > 0) { speedX -= 1; }

            if (speedX < 0) { speedX += 1; }

            if (speedY > 0) { speedY -= 1; }

            if (speedY < 0) { speedY += 1; }

            }

        //moves the player, if the player hits a wall he bounces of it
        public void move(int maxX, int maxY, int minX, int minY)
            {

            if ((position.X + speedX) < maxX && (position.X + speedX) > minX)

            { position.X = position.X + speedX; }

            else

            { bounceX(); }

            if ((position.Y + speedY) < maxY && (position.Y + speedY) > minY)

            { position.Y = position.Y + speedY; }

            else

            { bounceY(); }

            }

        public void bounceX()
            {
            speedX -= speedX * 2;
            }

        public void bounceY()
            {
            speedY -= speedY * 2;
            }

        public Rectangle getPosition()
            {
            return position;
            }

        public void setX(int x)
            {
            position.X = x;
            }

        public void setY(int y)
            {
            position.Y = y;
            }

        public int getScore()
            {
            return score;
            }

        public void increaseScore()
            {
            score++;
            }

        //draws the player using the given spirtebatch, texture and color
        public void draw(SpriteBatch spriteBatch, Texture2D texture, Color color)
            {
            spriteBatch.Draw(texture, position, color);
            }

        }
    }
