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
        private int stuckTimer;
        private Rectangle position;
        private Rectangle startPosition;
        private Rectangle lastKnownPosition;
        private Vector2 direction;
        private Map map;
        private AI ai;
        private bool inputEnabled;
        private bool towardsOneWay;
        private bool playerIsAI;

        //creates a new player to a given (x,y) position
        public Player(int x, int y, int width, int height, string n, Map m, bool computer, int aiLevel, Pills pills)
            {

            position = new Rectangle(x, y, 32, 32);
            startPosition = position;
            direction = new Vector2(x, y);
            map = m;
            name = n;
            screenWidth = width;
            screenHeight = height;
            score = 0;
            speed = 8;
            stuckTimer = 0;
            inputEnabled = true;
            towardsOneWay = false;
            playerIsAI = computer;
            ai = new AI(aiLevel, pills);

            }

        public string getName()
            {
            return name;
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

        public bool isAI()
            {
            return playerIsAI;
            }

        public AI getAI()
            {
            return ai;
            }

        //returns the position of a given edge of the player
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


        //player movement, if the player hits an edge, a wall or another player, it bounces:

        public void moveUpAndLeft(Player otherPlayer)
            {

            towardsOneWay = false;

            if (topEdges() || position.Intersects(otherPlayer.getPosition("bottom")))
                {
                bounceFromUpAndLeft(false);
                }
            else if (leftEdges() || position.Intersects(otherPlayer.getPosition("right")))
                {
                bounceFromUpAndLeft(true);
                }
            else
                {
                up();
                left();
                }

            }

        public void moveUpAndRight(Player otherPlayer)
            {

            towardsOneWay = false;

            if (topEdges() || position.Intersects(otherPlayer.getPosition("bottom")))
                {
                bounceFromUpAndRight(false);
                }
            else if (rightEdges() || position.Intersects(otherPlayer.getPosition("left")))
                {
                bounceFromUpAndRight(true);
                }
            else
                {
                up();
                right();
                }

            }

        public void moveDownAndLeft(Player otherPlayer)
            {

            towardsOneWay = false;

            if (bottomEdges() || position.Intersects(otherPlayer.getPosition("top")))
                {
                bounceFromDownAndLeft(false);
                }
            else if (leftEdges() || position.Intersects(otherPlayer.getPosition("right")))
                {
                bounceFromDownAndLeft(true);
                }
            else
                {
                down();
                left();
                }

            }

        public void moveDownAndRight(Player otherPlayer)
            {

            towardsOneWay = false;

            if (bottomEdges() || position.Intersects(otherPlayer.getPosition("top")))
                {
                bounceFromDownAndRight(false);
                }
            else if (rightEdges() || position.Intersects(otherPlayer.getPosition("left")))
                {
                bounceFromDownAndRight(true);
                }
            else
                {
                down();
                right();
                }

            }

        public void moveUp(Player otherPlayer)
            {

            towardsOneWay = true;

            if (topEdges() || position.Intersects(otherPlayer.getPosition("bottom")))
                {
                bounceDown();
                }
            else
                {
                up();
                }

            }

        public void moveDown(Player otherPlayer)
            {

            towardsOneWay = true;

            if (bottomEdges() || position.Intersects(otherPlayer.getPosition("top")))
                {
                bounceUp();
                }
            else
                {
                down();
                }

            }

        public void moveLeft(Player otherPlayer)
            {

            towardsOneWay = true;

            if (leftEdges() || position.Intersects(otherPlayer.getPosition("right")))
                {
                bounceRight();
                }
            else
                {
                left();
                }

            }

        public void moveRight(Player otherPlayer)
            {

            towardsOneWay = true;

            if (rightEdges() || position.Intersects(otherPlayer.getPosition("left")))
                {
                bounceLeft();
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

        //returns true if the players collide
        public bool hitsOtherPlayer(Rectangle p2position)
            {
            return position.Intersects(p2position);
            }


        //bounce when player moving left and up
        public void bounceFromUpAndLeft(bool leftOrRightEdge)
            {

            inputEnabled = false;

            if (leftOrRightEdge)
                {
                changeBothDirections(float.MaxValue, float.MinValue);
                }
            else
                {
                changeBothDirections(float.MinValue, float.MaxValue);
                }

            }

        //bounce when player moving right and up
        public void bounceFromUpAndRight(bool leftOrRightEdge)
            {

            inputEnabled = false;

            if (leftOrRightEdge)
                {
                changeBothDirections(float.MinValue, float.MinValue);
                }
            else
                {
                changeBothDirections(float.MaxValue, float.MaxValue);
                }

            }

        //bounce when player moving left and down
        public void bounceFromDownAndLeft(bool leftOrRightEdge)
            {

            inputEnabled = false;

            if (leftOrRightEdge)
                {
                changeBothDirections(float.MaxValue, float.MaxValue);
                }
            else
                {
                changeBothDirections(float.MinValue, float.MinValue);
                }

            }

        //bounce when player moving right and down
        public void bounceFromDownAndRight(bool leftOrRightEdge)
            {

            inputEnabled = false;

            if (leftOrRightEdge)
                {
                changeBothDirections(float.MinValue, float.MaxValue);
                }
            else
                {
                changeBothDirections(float.MaxValue, float.MinValue);
                }

            }

        //bounce when player moving only left
        public void bounceRight()
            {
            inputEnabled = false;
            direction.X = float.MaxValue;
            }

        //bounce when player moving only right
        public void bounceLeft()
            {
            inputEnabled = false;
            direction.X = float.MinValue;
            }

        //bounce when player moving only up
        public void bounceDown()
            {
            inputEnabled = false;
            direction.Y = float.MaxValue;
            }

        //bounce when player moving only down
        public void bounceUp()
            {
            inputEnabled = false;
            direction.Y = float.MinValue;
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

        //sets AI player's direction
        public void setDirection(Vector2 dir)
            {
            direction = dir;
            }

        //moves player towards the direction vector
        public void moveTowardsDirection(Player otherPlayer)
            {

            if (playerGotStuck())
                {
                position = startPosition;
                }

            if (position.X > direction.X && position.Y > direction.Y)
                {
                moveUpAndLeft(otherPlayer);
                }

            else if (position.X < direction.X && position.Y > direction.Y)
                {
                moveUpAndRight(otherPlayer);
                }

            else if (position.X > direction.X && position.Y < direction.Y)
                {
                moveDownAndLeft(otherPlayer);
                }

            else if (position.X < direction.X && position.Y < direction.Y)
                {
                moveDownAndRight(otherPlayer);
                }

            else if (position.X > direction.X)
                {
                moveLeft(otherPlayer);
                }

            else if (position.X < direction.X)
                {
                moveRight(otherPlayer);
                }

            else if (position.Y > direction.Y)
                {
                moveUp(otherPlayer);
                }

            else if (position.Y < direction.Y)
                {
                moveDown(otherPlayer);
                }

            }

        //returns true if the player doesn't move for a while
        //players sometimes get stuck on walls or each other and this method is used to fix that bug
        public bool playerGotStuck()
            {

            if (position.Equals(lastKnownPosition))
                {
                stuckTimer++;
                if (stuckTimer > 100)
                    {
                    return true;
                    }
                else
                    {
                    return false;
                    }
                }

            else
                {
                lastKnownPosition = position;
                stuckTimer = 0;
                return false;
                }

            }

        //draws the player using the given spirtebatch, texture and color
        public void draw(SpriteBatch spriteBatch, Texture2D texture, Color color)
            {
            spriteBatch.Draw(texture, position, color);
            }

        }

    }
