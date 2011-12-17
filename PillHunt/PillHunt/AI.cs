using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace PillHunt
    {
    class AI
        {

        private int level;
        private int counter;
        private Random random;
        private bool targetAcquired;
        private Pills pills;
        private bool hardIsUsingMedium;
        
        //creates a new AI of given level
        public AI(int lvl, Pills p)
            {
            level = lvl;
            counter = 0;
            random = new Random();
            targetAcquired = false;
            pills = p;
            hardIsUsingMedium = false;
            }

        //sets a new target for AI player if it doesn't already have one or the previous target has been reached
        public void moveAIPlayer(Player player)
            {

            if (level == 1 && targetAcquired)
                {

                counter++;

                if (counter > 30)
                    {
                    counter = 0;
                    targetAcquired = false;
                    }

                }

            else if (level == 3 && hardIsUsingMedium)
                {

                counter++;

                if (counter > 100)
                    {
                    hardIsUsingMedium = false;
                    counter = 0;
                    targetAcquired = false;
                    }

                }

            else if (level == 3 && !hardIsUsingMedium)
                {

                counter++;

                if (counter > 100)
                    {
                    hardIsUsingMedium = true;
                    counter = 0;
                    }

                }

            if (!targetAcquired)
                {
                player.setDirection(getDirection(player));
                }
            }

        //returns new target for AI, target depends on the level of AI
        public Vector2 getDirection(Player player)
            {

            if (level == 1)
                {
                return easyTarget();
                }

            else if (level == 2)
                {
                return mediumTarget();
                }

            else if (level == 3)
                {
                return hardTarget(player);
                }

            else
                {
                return veryHardTarget(player);
                }

            }

        //level 1 = easy, AI player gets totally random directions
        public Vector2 easyTarget()
            {
            targetAcquired = true;
            return randomDirection();
            }

        //level 2 = medium, AI player gets starting direction and the rest is based on bounces and luck
        public Vector2 mediumTarget()
            {
            targetAcquired = true;
            return new Vector2(float.MinValue, float.MinValue);
            }

        //level 3 = hard, AI player gets in turns medium or very hard targets
        public Vector2 hardTarget(Player player)
            {
            if (hardIsUsingMedium)
                {
                return mediumTarget();
                }
            else
                {
                return nearestPill(player.getPosition(""));
                }
            }

        //level 4 = very hard, AI player gets always the location of the nearest pill and tries to go towards it
        public Vector2 veryHardTarget(Player player)
            {
            targetAcquired = false;
            return nearestPill(player.getPosition(""));
            }

        //returns the nearest pill as a Vector2
        public Vector2 nearestPill(Rectangle playerPosition)
            {

            List<Pill> list = pills.getPills();
            Vector2 playerVector = new Vector2(playerPosition.X, playerPosition.Y);
            Vector2 pillVector = new Vector2(float.MinValue, float.MinValue);
            Vector2 nearest = pillVector;
            float shortestDistance = Vector2.Distance(pillVector, playerVector);

            for (int i = 0; i < list.Count; i++)
                {

                pillVector = new Vector2(list[i].getPosition().X, list[i].getPosition().Y);
                if (Vector2.Distance(pillVector, playerVector) < shortestDistance)
                    {
                    shortestDistance = Vector2.Distance(pillVector, playerVector);
                    nearest = pillVector;
                    }

                }

            return nearest;

            }

        //returns a random direction vector
        public Vector2 randomDirection()
            {

            int randomNumber = random.Next(3);
            Vector2 randomVector;

            if (randomNumber == 0)
                {
                randomVector = new Vector2(float.MinValue, float.MinValue);
                }

            else if (randomNumber == 1)
                {
                randomVector = new Vector2(float.MaxValue, float.MinValue);
                }

            else if (randomNumber == 2)
                {
                randomVector = new Vector2(float.MinValue, float.MaxValue);
                }

            else
                {
                randomVector = new Vector2(float.MaxValue, float.MaxValue);
                }

            return randomVector;

            }

        }

    }

