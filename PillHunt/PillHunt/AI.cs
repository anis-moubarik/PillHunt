using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace PillHunt
    {
    class AI
        {

        private int level;
        private bool targetAcquired;
        private Pills pills;

        //creates a new AI of given level
        public AI(int lvl, Pills p)
            {
            level = lvl;
            targetAcquired = false;
            pills = p;
            }

        //sets a new target for AI if it doesn't already have one or the previous target has been reached
        public void moveAIPlayer(Player player)
            {
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

            else
                {
                return hardTarget(player);
                }

            }

        //level 1 = easy, AI sets starting direction and the rest is based on bounces and luck
        public Vector2 easyTarget()
            {
            targetAcquired = true;
            return new Vector2(float.MinValue, float.MinValue);
            }

        //level 2 = medium
        public Vector2 mediumTarget()
            {
            targetAcquired = true;
            return new Vector2(float.MinValue, float.MinValue);
            }

        //level 3 = hard, AI gets always the location of the nearest pill and tries to go towards it
        public Vector2 hardTarget(Player player)
            {
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

        }

    }

