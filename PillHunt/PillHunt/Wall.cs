using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
    {
    class Wall
        {

        private Rectangle position;
        private int screenWidth;
        private int screenHeight;
        private bool isMoving;
        private bool isVertical;
        private int movingSpeed;

        //creates a new wall to given position
        public Wall(Rectangle pos, int width, int height, bool moving, bool vertical, int speed)
            {
            position = pos;
            screenWidth = width;
            screenHeight = height;
            isMoving = moving;
            isVertical = vertical;
            movingSpeed = speed;
            }

        //returns the position of a given edge of the wall
        public Rectangle getPosition(String edge)
            {
            if (edge.Equals("left"))
                {
                return new Rectangle(position.X, position.Y, 2, position.Height);
                }
            else if (edge.Equals("right"))
                {
                return new Rectangle(position.X + position.Width, position.Y - 2, 2, position.Height);
                }
            else if (edge.Equals("top"))
                {
                return new Rectangle(position.X, position.Y, position.Width, 2);
                }
            else if (edge.Equals("bottom"))
                {
                return new Rectangle(position.X, position.Y + position.Height - 2, position.Width, 2);
                }
            else {
                return position;
                }
            }

        //draws the wall using the given spritebatch and texture
        public void draw(SpriteBatch spriteBatch, Texture2D texture, bool gameEnds)
            {

            if (isMoving && !gameEnds)
                {
                moveWall();
                }

            spriteBatch.Draw(texture, position, Color.White);

            }

        //moves the wall
        public void moveWall()

            {

            if (isVertical) //vertical wall, moves left and right
                {
                if (position.X < 50 || position.X > (screenWidth - position.Width - 50))
                    {
                    movingSpeed = -1 * movingSpeed;
                    }
                position.X = position.X + movingSpeed;
                }

            else //horizontal wall, moves up and down
                {
                if (position.Y < 50 || position.Y > (screenHeight - position.Height - 50))
                    {
                    movingSpeed = -1 * movingSpeed;
                    }
                position.Y = position.Y + movingSpeed;
                }

            }

        }

    }
