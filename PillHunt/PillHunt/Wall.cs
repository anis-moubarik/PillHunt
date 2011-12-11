using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
    {
    class Wall
        {

        private Rectangle position;
        private Rectangle originalPosition;
        private Rectangle limitPosition;
        private bool isMoving;
        private bool isVertical;
        private bool isInflating;
        private int inflateLimit;
        private int speed;

        //creates a new wall
        public Wall(Rectangle pos, Rectangle limitPos, bool moving, bool vertical, bool inflating, int iLimit, int s)
            {

            position = pos;
            limitPosition = limitPos;
            isMoving = moving;
            isVertical = vertical;
            isInflating = inflating;
            inflateLimit = iLimit;
            speed = s;

            if (isVertical)
                {
                originalPosition = new Rectangle(pos.X - (pos.Width + 1), pos.Y, pos.Width, pos.Height);
                }
            else
                {
                originalPosition = new Rectangle(pos.X, pos.Y - (pos.Height + 1), pos.Width, pos.Height);
                }

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
            else
                {
                return position;
                }
            }

        //draws the wall using the given spritebatch and texture
        public void draw(SpriteBatch spriteBatch, Texture2D texture, bool gameEnds, bool active, Player player1, Player player2)
            {

            if (!gameEnds && active)
                {

                if (isMoving)
                    {
                    moveWall(player1, player2);
                    }

                if (isInflating)
                    {
                    inflateWall(player1, player2);
                    }

                }

            spriteBatch.Draw(texture, position, Color.White);

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
