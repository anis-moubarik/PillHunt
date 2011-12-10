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
        private bool towardsOneWay;

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
            towardsOneWay = false;

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

        public bool movingOnlyTowardsOneWay()
            {
            return towardsOneWay;
            }


        //player movement, if the player hits an edge, a wall or another player, it bounces

        public void moveUpAndLeft()
            {

            towardsOneWay = false;

            if (topEdges())
                {
                bounce(false);
                }
            else if (leftEdges())
                {
                bounce(true);
                }
            else
                {
                up();
                left();
                }

            }

        public void moveUpAndRight()
            {

            towardsOneWay = false;

            if (topEdges())
                {
                bounce(false);
                }
            else if (rightEdges())
                {
                bounce(true);
                }
            else
                {
                up();
                right();
                }

            }

        public void moveDownAndLeft()
            {

            towardsOneWay = false;

            if (bottomEdges())
                {
                bounce(false);
                }
            else if (leftEdges())
                {
                bounce(true);
                }
            else
                {
                down();
                left();
                }

            }

        public void moveDownAndRight()
            {

            towardsOneWay = false;

            if (bottomEdges())
                {
                bounce(false);
                }
            else if (rightEdges())
                {
                bounce(true);
                }
            else
                {
                down();
                right();
                }

            }

        public void moveUp()
            {

            towardsOneWay = true;

            if (topEdges())
                {
                bounce(false);
                }
            else
                {
                up();
                }

            }

        public void moveDown()
            {

            towardsOneWay = true;

            if (bottomEdges())
                {
                bounce(false);
                }
            else
                {
                down();
                }

            }

        public void moveLeft()
            {

            towardsOneWay = true;

            if (leftEdges())
                {
                bounce(true);
                }
            else
                {
                left();
                }

            }

        public void moveRight()
            {

            towardsOneWay = true;

            if (rightEdges())
                {
                bounce(true);
                }
            else
                {
                right();
                }

            }

        public bool topEdges()
            {
            return (position.Y < 5 || wall("bottom"));
            }

        public bool bottomEdges()
            {
            return (position.Y > (screenHeight - 37) || wall("top"));
            }

        public bool leftEdges()
            {
            return (position.X < 5 || wall("right"));
            }

        public bool rightEdges()
            {
            return (position.X > (screenWidth - 37) || wall("left"));
            }

        public void left()
            {
            inputEnabled = true;
            position.X = position.X - speed;
            }

        public void right()
            {
            inputEnabled = true;
            position.X = position.X + speed;
            }

        public void up()
            {
            inputEnabled = true;
            position.Y = position.Y - speed;
            }

        public void down()
            {
            inputEnabled = true;
            position.Y = position.Y + speed;
            }

        //returns true if there is a given edge of a wall in the way
        public bool wall(String edge)
            {
            return map.intersectsWithAWall(position, edge);
            }

        //bounces player towards opposite direction
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

        //moves player towards the direction vector
        public void moveTowardsDirection()
            {

            if (position.X > direction.X && position.Y > direction.Y)
                {
                moveUpAndLeft();
                }

            else if (position.X < direction.X && position.Y > direction.Y)
                {
                moveUpAndRight();
                }

            else if (position.X > direction.X && position.Y < direction.Y)
                {
                moveDownAndLeft();
                }

            else if (position.X < direction.X && position.Y < direction.Y)
                {
                moveDownAndRight();
                }

            else if (position.X > direction.X)
                {
                moveLeft();
                }

            else if (position.X < direction.X)
                {
                moveRight();
                }

            else if (position.Y > direction.Y)
                {
                moveUp();
                }

            else if (position.Y < direction.Y)
                {
                moveDown();
                }

            }

        //draws the player using the given spirtebatch, texture and color
        public void draw(SpriteBatch spriteBatch, Texture2D texture, Color color)
            {
            spriteBatch.Draw(texture, position, color);
            }

        }

    }
