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
        private Rectangle position;
        int speedX;
        int speedY;

        public Player()
            {
            score = 0;
            position = new Rectangle(0, 0, 32, 32);
            }

        public Rectangle getPosition()
            {
            return position;
            }

        public int getX()
            {
            return position.X;
            }

        public int getY()
            {
            return position.Y;
            }

        public void setPosition(Rectangle newPosition)
            {
            position = newPosition;
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

        public void draw(SpriteBatch spriteBatch, Texture2D texture)
            {
            spriteBatch.Draw(texture, position, Color.White);
            }

        public void changeSpeedX(int change)
        {
            speedX += change;
        }

        public void changeSpeedY(int change)
        {
            speedY += change;
        }

        public void slowDown()
        {
            if (speedX > 0)
            {
                speedX -= 1;
            }
            if (speedX < 0)
            {
                speedX += 1;
            }

            if (speedY > 0)
            {
                speedY -= 1;
            }
            if (speedY < 0)
            {
                speedY += 1;
            }
        }

        internal void move(int maxX, int maxY, int minX, int minY)
        {
            if ((getX() + speedX) < maxX && ((getX() + speedX) > minX))
            {
                setX(getX() + speedX);
            } //bounce
            else
            {
                bounceX();
            }

            if ((getY() + speedY) < maxY && (getY() + speedY) > minY)
            {
                setY(getY() + speedY);
            }
            else
            {
                bounceY();
            }
        }

        internal void bounceX()
        {
            speedX -= speedX * 2;
        }

        internal void bounceY()
        {
            speedY -= speedY * 2;
        }
        }
    }
