using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        private Map map;
        private bool inputEnabled;

        //creates a new player to a given (x,y) position
        public Player(int x, int y, int width, int height, string n, Map m)
            {

            position = new Rectangle(x, y, 32, 32);
            direction = new Vector2(x, y);
            map = m;
            name = n;
            screenWidth = width;
            screenHeight = height;
            score = 0;
            speed = 8;
            inputEnabled = true;

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

        public bool isInputEnabled()
            {
            return inputEnabled;
            }


        //player movement, if the player hits an edge or a wall, it bounces

        public void moveUp()
            {
            if (position.Y > 0 && noWalls("bottom"))
                {
                inputEnabled = true;
                position.Y = position.Y - speed;
                }
            else
                {
                bounce(false);
                }
            }

        public void moveDown()
            {
            if (position.Y < (screenHeight - 32) && noWalls("top"))
                {
                inputEnabled = true;
                position.Y = position.Y + speed;
                }
            else
                {
                bounce(false);
                }
            }

        public void moveLeft()
            {
            if (position.X > 0 && noWalls("right"))
                {
                inputEnabled = true;
                position.X = position.X - speed;
                }
            else
                {
                bounce(true);
                }
            }

        public void moveRight()
            {
            if (position.X < (screenWidth - 32) && noWalls("left"))
                {
                inputEnabled = true;
                position.X = position.X + speed;
                }
            else
                {
                bounce(true);
                }
            }

        //returns true if there are no walls in the way
        public bool noWalls(String edge)
            {
            return !(map.intersectsWithAWall(position, edge));
            }

        //bounces player towards opposite direction(s)
        public void bounce(bool leftOrRightEdge)
            {

            inputEnabled = false;

            //player moving left and up
            if (direction.X == float.MinValue && direction.Y == float.MinValue)
                {
                if (leftOrRightEdge)
                    {
                    changeBothDirections(float.MaxValue, float.MinValue);
                    }
                else
                    {
                    changeBothDirections(float.MinValue, float.MaxValue);
                    }
                }

            //player moving right and up
            else if (direction.X == float.MaxValue && direction.Y == float.MinValue)
                {
                if (leftOrRightEdge)
                    {
                    changeBothDirections(float.MinValue, float.MinValue);
                    }
                else
                    {
                    changeBothDirections(float.MaxValue, float.MaxValue);
                    }
                }

            //player moving left and down
            else if (direction.X == float.MinValue && direction.Y == float.MaxValue)
                {
                if (leftOrRightEdge)
                    {
                    changeBothDirections(float.MaxValue, float.MaxValue);
                    }
                else
                    {
                    changeBothDirections(float.MinValue, float.MinValue);
                    }
                }

            //player moving right and down
            else if (direction.X == float.MaxValue && direction.Y == float.MaxValue)
                {
                if (leftOrRightEdge)
                    {
                    changeBothDirections(float.MinValue, float.MaxValue);
                    }
                else
                    {
                    changeBothDirections(float.MaxValue, float.MinValue);
                    }
                }

            //player moving only left
            else if (direction.X == float.MinValue)
                {
                direction.X = float.MaxValue;
                }

            //player moving only right
            else if (direction.X == float.MaxValue)
                {
                direction.X = float.MinValue;
                }

            //player moving only up
            else if (direction.Y == float.MinValue)
                {
                direction.Y = float.MaxValue;
                }

            //player moving only down
            else if (direction.Y == float.MaxValue)
                {
                direction.Y = float.MinValue;
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

        //moves player towards the direction vector, checks if the players collide
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
