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
        Map map;

        //creates a new player to a given (x,y) position
        public Player(int x, int y, int w, int h, string n, Map m)
            {

            position = new Rectangle(x, y, 32, 32);
            width = w;
            height = h;
            name = n;
            map = m;

            score = 0;
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

        public void moveUpAndLeft()
            {
            if (noLeftEdge() && noTop() && noWall())
                {
                up();
                left();
                }
            else
                {
                bounceDown();
                bounceRight();
                }
            }

        public void moveUpAndRight()
            {
            if (noRightEdge() && noTop() && noWall())
                {
                up();
                right();
                }
            else
                {
                bounceDown();
                bounceLeft();
                }
            }

        public void moveDownAndLeft()
            {
            if (noLeftEdge() && noBottom() && noWall())
                {
                down();
                left();
                }
            else
                {
                bounceUp();
                bounceRight();
                }
            }

        public void moveDownAndRight()
            {
            if (noRightEdge() && noBottom() && noWall())
                {
                down();
                right();
                }
            else
                {
                bounceUp();
                bounceLeft();
                }
            }

        public void moveUp()
            {
            if (noTop() && noWall())
                {
                up();
                }
            else
                {
                bounceDown();
                }
            }

        public void moveDown()
            {
            if (noBottom() && noWall())
                {
                down();
                }
            else
                {
                bounceUp();
                }
            }

        public void moveLeft()
            {

            if (noLeftEdge() && noWall())
                {
                left();
                }
            else
                {
                bounceRight();
                }
            }

        public void moveRight()
            {

            if (noRightEdge() && noWall())
                {
                right();
                }
            else
                {
                bounceLeft();
                }
            }


        //directions

        public void up()
            {
            position.Y = position.Y - speed;
            }

        public void down()
            {
            position.Y = position.Y + speed;
            }

        public void left()
            {
            position.X = position.X - speed;
            }

        public void right()
            {
            position.X = position.X + speed;
            }


        //bounces
        
        public void bounceUp()
            {
            position.Y = position.Y - bounceRate * speed;
            }

        public void bounceDown()
            {
            position.Y = position.Y + bounceRate * speed;
            }

        public void bounceLeft()
            {
            position.X = position.X - bounceRate * speed;
            }

        public void bounceRight()
            {
            position.X = position.X + bounceRate * speed;
            }


        //wall and edge checks

        public bool noWall()
            {
            return !map.intersectsWithAWall(position);
            }

        public bool noLeftEdge()
            {
            return position.X > 0;
            }

        public bool noRightEdge()
            {
            return position.X < (width - 32);
            }

        public bool noTop()
            {
            return position.Y > 0;
            }

        public bool noBottom()
            {
            return position.Y < (height - 32);
            }

        //draws the player using the given spirtebatch, texture and color
        public void draw(SpriteFont font, SpriteBatch spriteBatch, Texture2D texture, Color color)
            {
            spriteBatch.Draw(texture, position, color);
            }

        }
    }
