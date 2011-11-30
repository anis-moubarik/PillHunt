using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
{
    class Player
    {

        private string name;
        private int score;
        private int width;
        private int height;
        private int speed;
        private int bounceRate;
        private Rectangle position;

        //creates a new player to a given (x,y) position
        public Player(int x, int y, int w, int h, string nick)
        {
            score = 0;
            position = new Rectangle(x, y, 32, 32);
            name = nick;
            width = w;
            height = h;
            speed = 8;
            bounceRate = 2;
        }

        public string getName()
        {
            return name;
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


        //player movement

        public void moveLeft() {

            if (position.X > 0)
            {
                position.X = position.X - speed;
            }
            else
            {
                bounceRight();
            }
        }

        public void moveRight()
        {

            if ((position.X) < (width - 32))
            {
                position.X = position.X + speed;
            }
            else
            {
                bounceLeft();
            }
        }

        public void moveUp()
        {
            if (position.Y > 0)
            {
                position.Y = position.Y - speed;
            }
            else
            {
                bounceDown();
            }
        }

        public void moveDown()
        {
            if ((position.Y) < (height - 32))
            {
                position.Y = position.Y + speed;
            }
            else
            {
                bounceUp();
            }
        }

        
        //bounces

        public void bounceLeft()
        {
            position.X = position.X - bounceRate * speed;
        }

        public void bounceRight()
        {
            position.X = position.X + bounceRate * speed;
        }

        public void bounceDown()
        {
            position.Y = position.Y + bounceRate * speed;
        }

        public void bounceUp()
        {
            position.Y = position.Y - bounceRate * speed;
        }


        //draws the player using the given spirtebatch, texture and color
        public void draw(SpriteFont font, SpriteBatch spriteBatch, Texture2D texture, Color color)
        {
            spriteBatch.Draw(texture, position, color);
        }

    }
}
