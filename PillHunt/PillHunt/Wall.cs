using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
    {
    class Wall
        {

        private Color color;
        private Rectangle position;
        private Rectangle originalPosition;
        private Rectangle limitPosition;
        private Rectangle outSideScreenPosition;
        private bool isMoving;
        private bool isVertical;
        private bool isInflating;
        private bool partTimeVisible;
        private bool isVisible;
        private int visibilityCounter;
        private int blinkRate;
        private int inflateLimit;
        private int speed;

        //creates a new wall
        public Wall(Color c, Rectangle pos, Rectangle limitPos, bool moving, 
            bool vertical, bool inflating, bool ptv, int counter, int bRate, int iLimit, int s)
            {

            color = c;
            position = pos;
            limitPosition = limitPos;
            outSideScreenPosition = new Rectangle(-100, -100, 1, 1);
            isMoving = moving;
            isVertical = vertical;
            isInflating = inflating;
            partTimeVisible = ptv;
            isVisible = true;
            visibilityCounter = counter;
            blinkRate = bRate;
            inflateLimit = iLimit;
            speed = s;

            if (isVertical)

                {

                if (speed < 0)
                    {
                    originalPosition = new Rectangle(pos.X + (pos.Width + 1), pos.Y, pos.Width, pos.Height);
                    }

                else
                    {
                    originalPosition = new Rectangle(pos.X - (pos.Width + 1), pos.Y, pos.Width, pos.Height);
                    }

                }

            else

                if (speed < 0)
                    {
                    originalPosition = new Rectangle(pos.X, pos.Y + (pos.Height + 1), pos.Width, pos.Height);
                    }

                else
                    {
                    originalPosition = new Rectangle(pos.X, pos.Y - (pos.Height + 1), pos.Width, pos.Height);
                    }

            }

        //returns the position of a given edge of the wall
        public Rectangle getPosition(String edge)
            {

            if (!isVisible)
                {
                return outSideScreenPosition;
                }

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
            else
                {
                return position;
                }
            }

        //draws the wall using the given spritebatch and texture
        public void draw(SpriteBatch spriteBatch, Texture2D texture, bool gameEnds, bool active, Player player1, Player player2)
            {

            if (!gameEnds && active) //game doesn't end and it isn't paused
                {

                if (isMoving)
                    {
                    moveWall(player1, player2);
                    }

                if (isInflating)
                    {
                    inflateWall(player1, player2);
                    }

                if (partTimeVisible)
                    {
                    hideOrShowWall();
                    }

                }

            if (isVisible)
                {
                spriteBatch.Draw(texture, position, color);
                }

            }

        //moves the wall
        public void moveWall(Player player1, Player player2)
            {

            checkPlayerHits(player1, player2);

            if (position.Intersects(originalPosition) || position.Intersects(limitPosition))
                {
                speed = -1 * speed;
                }

            if (isVertical) //vertical wall, moves left and right
                {
                position.X = position.X + speed;
                }
            else //horizontal wall, moves up and down
                {
                position.Y = position.Y + speed;
                }

            }

        //inflates the wall
        public void inflateWall(Player player1, Player player2)
            {

            checkPlayerHits(player1, player2);

            if (isMoving)
                {
                if (position.Width < 0 || position.Width > (originalPosition.Width + inflateLimit))
                    {
                    speed = -1 * speed;
                    }
                }

            else
                {
                if (position.Width < originalPosition.Width || position.Width > (originalPosition.Width + inflateLimit))
                    {
                    speed = -1 * speed;
                    }
                }

            position.Width = position.Width + speed;
            position.Height = position.Height + speed;

            }

        //hides or shows a part-time visible wall
        public void hideOrShowWall()
            {

            if (visibilityCounter > blinkRate)
                {
                isVisible = !isVisible;
                visibilityCounter = 0;
                }

            else
                {
                visibilityCounter++;
                }

            }

        //checks if player hits a moving wall while player is moving only towards one way
        public void checkPlayerHits(Player player1, Player player2)
            {

            if (player1.movingOnlyTowardsOneWay())
                {

                if (player1.getPosition("").Intersects(getPosition("top")))
                    {
                    player1.changeDirectionY(float.MinValue);
                    }

                else if (player1.getPosition("").Intersects(getPosition("bottom")))
                    {
                    player1.changeDirectionY(float.MaxValue);
                    }

                else if (player1.getPosition("").Intersects(getPosition("left")))
                    {
                    player1.changeDirectionX(float.MinValue);
                    }

                else if (player1.getPosition("").Intersects(getPosition("right")))
                    {
                    player1.changeDirectionX(float.MaxValue);
                    }

                }

            if (player2.movingOnlyTowardsOneWay())
                {

                if (player2.getPosition("").Intersects(getPosition("top")))
                    {
                    player2.changeDirectionY(float.MinValue);
                    }

                else if (player2.getPosition("").Intersects(getPosition("bottom")))
                    {
                    player2.changeDirectionY(float.MaxValue);
                    }

                else if (player2.getPosition("").Intersects(getPosition("left")))
                    {
                    player2.changeDirectionX(float.MinValue);
                    }

                else if (player2.getPosition("").Intersects(getPosition("right")))
                    {
                    player2.changeDirectionX(float.MaxValue);
                    }

                }

            }

        }

    }
