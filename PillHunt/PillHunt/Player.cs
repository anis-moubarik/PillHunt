using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
    {
    class Player
        {

        private string name;
        private int score;
        private int screenWidth;
        private int screenHeight;
        private int speed;
        private Rectangle position;
        private Vector2 direction;

        //creates a new player to a given (x,y) position
        public Player(int x, int y, int width, int height, string n)
            {

            position = new Rectangle(x, y, 32, 32);
            screenWidth = width;
            screenHeight = height;
            name = n;
            direction = new Vector2(0, screenHeight);
            score = 0;
            speed = 8;

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

        public void moveUp()
            {
            if (position.Y > 0)
                {
                position.Y = position.Y - speed;
                }
            }

        public void moveDown()
            {
            if (position.Y < (screenHeight - 32))
                {
                position.Y = position.Y + speed;
                }
            }

        public void moveLeft()
            {

            if (position.X > 0)
                {
                position.X = position.X - speed;
                }
            }

        public void moveRight()
            {

            if (position.X < (screenWidth - 32))
                {
                position.X = position.X + speed;
                }
            }
        
        public void changeDirectionX(float x)
            {
            direction.X = x;
            direction.Y = position.Y;
            }

        public void changeDirectionY(float y)
            {
            direction.Y = y;
            direction.X = position.X;
            }

        public void changeBothDirections(float x, float y)
            {
            direction.X = x;
            direction.Y = y;
            }

        //moves player towards the direction vector
        public void moveTowardsDirection()
            {

            if (position.Y < direction.Y)
                {
                moveDown();
                }

            else if (position.Y > direction.Y)
                {
                moveUp();
                }

            if (position.X < direction.X)
                {
                moveRight();
                }

            else if (position.X > direction.X)
                {
                moveLeft();
                }

            }


        //draws the player using the given spirtebatch, texture and color
        public void draw(SpriteBatch spriteBatch, Texture2D texture, Color color)
            {
            spriteBatch.Draw(texture, position, color);
            }

        }
    }
