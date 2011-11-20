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

        string name;
        private int score;
        private int speedX;
        private int speedY;
        private int totalSpeed;
        private const int maxSpeed = 30;
        private Rectangle position;

        //creates a new player to a given (x,y) position
        public Player(int x, int y, string nick)
        {
            score = 0;
            position = new Rectangle(x, y, 32, 32);
            name = nick;
        }

        public string getName()
        {
            return name;
        }

        public void changeSpeedX(int change)
        {
            if (Math.Abs((speedX + change)) < maxSpeed)
            {
                speedX += change;
            }
        }

        public void changeSpeedY(int change)
        {

            if (Math.Abs((speedY + change)) < maxSpeed)
            {
                speedY += change;
            }
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
                if (speedX > maxSpeed)
                {
                    speedX = maxSpeed;
                }
                else
                {
                    speedX -= speedX * 2;
                }
        }

        public void bounceY()
        {
            if (speedY > maxSpeed)
            {
                speedY = maxSpeed;
            }
            else
            {
                speedY -= speedY * 2;
            }
        }

        public void bounceXY()
        {
            bounceX();
            bounceY();

        }

        public Rectangle getPosition()
        {
            return position;
        }

        public int getScore()
        {
            return score;
        }

        public void increaseScore(int increase)
        {
            score = score + increase;
        }

        //draws the player using the given spirtebatch, texture and color
        public void draw(SpriteBatch spriteBatch, Texture2D texture, Color color)
        {
            spriteBatch.Draw(texture, position, color);
        }

    }
}
